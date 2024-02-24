// ******************************************************************
//       /\ /|       @file       StaticListView.cs
//       \ V/        @brief      静态列表
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2021-02-02 14:38:30
//    *(__\_\        @Copyright  Copyright (c) 2021, Shadowrabbit
// ******************************************************************

using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Rabi
{
    public class StaticListView : MonoBehaviour
    {
        //布局方向
        public enum Direction
        {
            [LabelText("水平")] Horizontal, //水平
            [LabelText("垂直")] Vertical, //垂直
        }

        #region FIELDS

        [LabelText("水平间距")] public float spacingX; //x间距
        [LabelText("垂直间距")] public float spacingY; //y间距
        [LabelText("水平偏移")] public float offsetX; //偏移量x
        [LabelText("垂直偏移")] public float offsetY; //偏移量y
        [LabelText("原型体预设物")] public GameObject protoObj; //item原型体
        [LabelText("布局方向")] public Direction direction = Direction.Vertical; //布局方向
        [LabelText("行或列的数量(用于网格布局)")] public int rowOrCol = 1; //垂直布局表示列的数量 水平布局表示行的数量
        [LabelText("准备绘制的item数量")] public int tryDrawItemNum; //准备绘制的item数量
        private Action<GameObject, int> _onEnable; //启用时回调 
        private Action<GameObject> _onDisable; //禁用时回调 
        private RectTransform _compRectTransformContent; //内容节点tran组件
        private float _protoHeight; //原型体高度
        private float _protoWidth; //原型体宽度
        private Vector2 _protoPivot; //原型体重心
        private Vector2 _protoScale; //原型体缩放
        private int _cacheItemNum = -1; //当前数据中item的总数量
        private List<Vector3> _objPosList = new List<Vector3>(); //obj物体位置列表
        private Stack<GameObject> _objPool = new Stack<GameObject>(); //item物体对象池
        private Vector2 _cachePivot; //缓存 计算前的重心
        private Vector2 _cacheAnchorMin; //缓存 计算前的最小锚点
        private Vector2 _cacheAnchorMax; //缓存 计算前的最大锚点
        private Vector3 _cacheAnchoredPosition3D; //缓存 计算前的UI坐标

        private Dictionary<int, GameObject>
            _mapCurrentObjDict = new Dictionary<int, GameObject>(); //当前使用中的obj物体<索引,物体>

        private bool _hasInit; //已经完成初始化

        #endregion

        #region UNITY LIFE

        protected void Awake()
        {
            if (!_hasInit)
            {
                Init();
            }
        }

        protected void OnDestroy()
        {
            ClearItems();
            Dispose();
        }

#if UNITY_EDITOR
        /// <summary>
        /// inspector值发生改变回调
        /// </summary>
        protected void OnValidate()
        {
            if (Application.isPlaying)
            {
                return;
            }

            if (_mapCurrentObjDict.Count == 0)
            {
                return;
            }

            if (!_hasInit)
            {
                Init();
            }

            Refresh();
        }
#endif

        #endregion

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            //原型体校验
            CheckProtoTransform();
            _compRectTransformContent = GetComponent<RectTransform>() ??
                                        throw new Exception("找不到RectTransform组件:" + _compRectTransformContent.name);
            _hasInit = true;
        }

        /// <summary>
        /// 注册item启用回调
        /// </summary>
        /// <param name="onItemEnable"></param>
        public void AddListenerOnItemEnable(Action<GameObject, int> onItemEnable)
        {
            if (onItemEnable == null)
            {
                return;
            }

            _onEnable = onItemEnable;
        }

        /// <summary>
        /// 注册item禁用回调
        /// </summary>
        /// <param name="onItemDisable"></param>
        public void AddListenerOnItemDisable(Action<GameObject> onItemDisable)
        {
            if (onItemDisable == null)
            {
                return;
            }

            _onDisable = onItemDisable;
        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="itemNum">item绘制数量</param>
        public void Refresh(int itemNum)
        {
            tryDrawItemNum = itemNum;
            Refresh();
        }

        /// <summary>
        /// 清空
        /// </summary>
        public void Clear()
        {
            ClearItems();
        }

        /// <summary>
        /// 撤销监听
        /// </summary>
        public void RemoveAllListeners()
        {
            _onEnable = null;
            _onDisable = null;
        }

        /// <summary>
        /// 刷新
        /// </summary>
        private void Refresh()
        {
            if (!_hasInit)
            {
                Init();
            }

            //缓存计算前的节点transform信息
            CacheTransform();
            //内容节点校验
            CheckContentTransform();
            //尝试回收多余的item
            TryRecycleItems(tryDrawItemNum);
            // 计算content尺寸
            CalcContentSize(tryDrawItemNum);
            //计算储存每个item坐标信息
            CalcEachItemPosition(tryDrawItemNum);
            //尝试放置item
            TrySetItems();
            //记录当前item总数量
            _cacheItemNum = tryDrawItemNum;
            //还原计算前的节点transform信息
            RevertTransform();
        }

        /// <summary>
        /// 清空所有item
        /// </summary>
        private void ClearItems()
        {
            if (!_hasInit)
            {
                Init();
            }

            for (var i = _compRectTransformContent.childCount - 1; i >= 0; i--)
            {
                TryRecycleItem(i);
                var compTransformChild = _compRectTransformContent.GetChild(i);
                Destroy(compTransformChild.gameObject);
            }

            _objPool.Clear();
            _mapCurrentObjDict.Clear();
            tryDrawItemNum = 0;
        }

        /// <summary>
        /// 尝试回收多余item
        /// </summary>
        /// <param name="itemNum"></param>
        private void TryRecycleItems(int itemNum)
        {
            //当前需要绘制的item总数大于等于缓存中的item总数
            if (itemNum >= _cacheItemNum)
            {
                return;
            }

            //回收多余item
            for (var i = itemNum; i < _cacheItemNum; i++)
            {
                TryRecycleItem(i);
            }
        }

        /// <summary>
        /// 尝试回收item
        /// </summary>
        private void TryRecycleItem(int i)
        {
            _mapCurrentObjDict.TryGetValue(i, out var obj);
            //不存在对应索引的物体
            if (obj == null)
            {
                return;
            }

            //回收物体
            _onDisable?.Invoke(obj);
            //Debug.Log("OnDisable" + _i);
            PushObj(obj);
            _mapCurrentObjDict.Remove(i);
        }

        /// <summary>
        /// 尝试放置item
        /// </summary>  
        private void TrySetItems()
        {
            for (var i = 0; i < _objPosList.Count; i++)
            {
                _mapCurrentObjDict.TryGetValue(i, out var obj);
                //物体已经存在
                if (obj != null)
                {
                    //需要刷新数据
                    _onDisable?.Invoke(obj);
                    _onEnable?.Invoke(obj, i);
                    obj.transform.localPosition = _objPosList[i];
                    continue;
                }

                //物体不存在 尝试从对象池中取出
                obj = PopObj();
                _mapCurrentObjDict.Add(i, obj);
                obj.transform.localPosition = _objPosList[i];
                //启用回调
                _onEnable?.Invoke(obj, i);
                //Debug.Log("OnEnable" + i);
            }
        }

        /// <summary>
        /// 计算储存每个item坐标信息
        /// </summary>
        /// <param name="itemNum"></param>
        private void CalcEachItemPosition(int itemNum)
        {
            if (itemNum < 0)
            {
                return;
            }

            //清空数据
            _objPosList.Clear();
            for (var i = 0; i < itemNum; i++)
            {
                float x, y;
                switch (direction)
                {
                    //垂直布局情况
                    case Direction.Vertical:
                        //x = 该元素位于第几列 * （原型体宽度 + X间距) + X偏移量
                        x = (i % rowOrCol) * (_protoWidth + spacingX) + offsetX;
                        //y = 该元素位于第几行 * （原型体高度 + Y间距）+ Y偏移量
                        // ReSharper disable once PossibleLossOfFraction
                        y = (i / rowOrCol) * (_protoHeight + spacingY) + offsetY;
                        break;
                    case Direction.Horizontal:
                        // ReSharper disable once PossibleLossOfFraction
                        x = (_protoWidth + spacingX) * (i / rowOrCol) + offsetX;
                        y = (_protoHeight + spacingY) * (i % rowOrCol) + offsetY;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                var pivotOffsetX = _protoWidth * _protoPivot.x;
                var pivotOffsetY = _protoHeight * -(1 - _protoPivot.y);
                _objPosList.Add(new Vector3(x + pivotOffsetX, -y + pivotOffsetY, 0));
            }
        }

        /// <summary>
        /// obj出栈
        /// </summary>
        /// <returns></returns>
        private GameObject PopObj()
        {
            GameObject obj;
            //尝试从对象池取obj 没有的话创建新实例
            if (_objPool.Count > 0)
            {
                obj = _objPool.Pop();
            }
            else
            {
                obj = Instantiate(protoObj, transform);
                //创建的时候根据顺序命名
                obj.name = protoObj.name + transform.childCount;
            }

            obj.transform.localScale = _protoScale;
            obj.transform.localRotation = Quaternion.identity;
            obj.SetActive(true);
            return obj;
        }

        /// <summary>
        /// obj压入对象池栈
        /// </summary>
        /// <param name="obj"></param>
        private void PushObj(GameObject obj)
        {
            if (obj == null)
            {
                return;
            }

            _objPool.Push(obj);
            obj.SetActive(false);
        }

        /// <summary>
        /// 原型体transform校验
        /// </summary>
        private void CheckProtoTransform()
        {
            //原型体校验
            if (protoObj == null)
            {
                Debug.LogError("找不到原型体");
                return;
            }

            var compProtoRectTransform = protoObj.GetComponent<RectTransform>();
            var rect = compProtoRectTransform.rect;
            _protoScale = compProtoRectTransform.localScale;
            _protoHeight = rect.height * _protoScale.y;
            _protoWidth = rect.width * _protoScale.x;
            _protoPivot = compProtoRectTransform.pivot;
        }

        /// <summary>
        /// 内容节点transform校验
        /// </summary>
        private void CheckContentTransform()
        {
            _compRectTransformContent.pivot = new Vector2(0, 1);
            _compRectTransformContent.anchorMin = new Vector2(0, 1);
            _compRectTransformContent.anchorMax = new Vector2(0, 1);
        }

        /// <summary>
        /// 计算content尺寸
        /// </summary>
        private void CalcContentSize(int itemNum)
        {
            if (itemNum < 0)
            {
                Debug.LogError("itemNum不能小于0");
                return;
            }

            var eachRowOrColNum = Mathf.Ceil((float) itemNum / rowOrCol); //每列/行item的最大数量
            switch (direction)
            {
                //垂直布局
                case Direction.Vertical:
                {
                    var contentHeight =
                        (spacingY + _protoHeight) * eachRowOrColNum + offsetY -
                        spacingY; //内容布局高 = (原型体高度 + 间隔高度) * 每列的最大item数量 + 垂直偏移 - 一份间隔
                    var contentWidth =
                        _protoWidth * rowOrCol + (rowOrCol - 1) * spacingX +
                        offsetX; //内容布局宽 = 原型体宽度 * 列数 + (列数 - 1) * 间距X + 水平偏移
                    _compRectTransformContent.sizeDelta = new Vector2(contentWidth, contentHeight);
                    break;
                }
                //水平布局
                case Direction.Horizontal:
                {
                    var contentWidth = (spacingX + _protoWidth) * eachRowOrColNum + offsetX - spacingX;
                    var contentHeight = _protoHeight * rowOrCol + (rowOrCol - 1) * spacingY + offsetY;
                    _compRectTransformContent.sizeDelta = new Vector2(contentWidth, contentHeight);
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// 缓存内容节点transform信息
        /// </summary>
        private void CacheTransform()
        {
            _cachePivot = _compRectTransformContent.pivot;
            _cacheAnchorMax = _compRectTransformContent.anchorMax;
            _cacheAnchorMin = _compRectTransformContent.anchorMin;
            _cacheAnchoredPosition3D = _compRectTransformContent.anchoredPosition3D;
        }

        /// <summary>
        /// 还原transform 计算的坐标的时候私自修改了参数 算完了改回去
        /// </summary>
        private void RevertTransform()
        {
            _compRectTransformContent.anchoredPosition3D = _cacheAnchoredPosition3D;
            _compRectTransformContent.anchorMin = _cacheAnchorMin;
            _compRectTransformContent.anchorMax = _cacheAnchorMax;
            _compRectTransformContent.pivot = _cachePivot;
        }

        /// <summary>   
        /// 释放内存
        /// </summary>
        private void Dispose()
        {
            _onEnable = null;
            _onDisable = null;
            _objPosList = null;
            _objPool = null;
            _mapCurrentObjDict = null;
            protoObj = null;
            _compRectTransformContent = null;
        }

        /// <summary>
        /// 编辑器展示item
        /// </summary>
        [UsedImplicitly]
        [Button("刷新")]
        private void EditorShowItems()
        {
            Init();
            Refresh();
        }

        /// <summary>
        /// 编辑器模式清空item
        /// </summary>
        [UsedImplicitly]
        [Button("清除")]
        private void EditorClearItems()
        {
            for (var i = _compRectTransformContent.childCount - 1; i >= 0; i--)
            {
                TryRecycleItem(i);
                var compTransformChild = _compRectTransformContent.GetChild(i);
                DestroyImmediate(compTransformChild.gameObject);
            }

            _objPool.Clear();
            _mapCurrentObjDict.Clear();
            tryDrawItemNum = 0;
        }
    }
}