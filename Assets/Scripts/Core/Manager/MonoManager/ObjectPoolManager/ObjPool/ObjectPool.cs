// ******************************************************************
//       /\ /|       @file       ObjectPool.cs
//       \ V/        @brief      对象池 定期清理一定数量的过期物体
//       | "")       @author     Shadowrabbit, yue.wang04@mihoyo.com
//       /  |                    
//      /  \\        @Modified   2022-03-18 18:10:34
//    *(__\_\        @Copyright  Copyright (c)  2022, Shadowrabbit
// ******************************************************************

using System.Collections.Generic;
using UnityEngine;

namespace Rabi
{
    public class ObjectPool
    {
        private readonly string _name; //池名字
        private readonly Transform _parent; //被回收物体的挂点
        private readonly List<GameObject> _objList = new List<GameObject>(); //内部回收的物体列表
        private readonly Dictionary<int, float> _insId2RecycledTime = new Dictionary<int, float>(); //实例id对回收时间映射
        private float _expiredTime; //判定obj过期的时间 单位秒
        private int _maxReleaseCount; //每次释放的最大数量
        private int _minCacheNum; //最小缓存数量 小于这个数量时过期obj不会被销毁
        private float _releaseInterval; //释放间隔
        private float _lastReleaseTime; //上次释放的时间

        public bool IsPoolEmpty => _objList == null || _objList.Count <= 0; //池子为空

        public ObjectPool(string name, Transform parent, float expiredTime = 300, int minCacheNum = 0,
            int maxReleaseCount = 4, float releaseInterval = 30)
        {
            _name = name;
            _parent = parent;
            _expiredTime = expiredTime;
            _minCacheNum = minCacheNum;
            _maxReleaseCount = maxReleaseCount;
            _releaseInterval = releaseInterval;
            _lastReleaseTime = Time.realtimeSinceStartup;
        }

        /// <summary>
        /// 设置对象池参数
        /// </summary>
        /// <param name="expiredTime"></param>
        /// <param name="minCacheNum"></param>
        /// <param name="maxReleaseCount"></param>
        /// <param name="releaseInterval"></param>
        public void SetParams(float expiredTime = 300, int minCacheNum = 0,
            int maxReleaseCount = 4, float releaseInterval = 30)
        {
            _expiredTime = expiredTime;
            _minCacheNum = minCacheNum;
            _maxReleaseCount = maxReleaseCount;
            _releaseInterval = releaseInterval;
        }

        /// <summary>
        /// 轮询
        /// </summary>
        public void Tick()
        {
            if (_objList.Count <= _minCacheNum)
            {
                return;
            }

            var realtimeSinceStartup = Time.realtimeSinceStartup;
            //不满足释放间隔
            if (realtimeSinceStartup - _lastReleaseTime < _releaseInterval)
            {
                return;
            }

            _lastReleaseTime = realtimeSinceStartup;
            //已经释放的数量
            var hasReleaseCount = 0;
            for (var i = _objList.Count - 1; i >= 0; i--)
            {
                var obj = _objList[i];
                _insId2RecycledTime.TryGetValue(obj.GetInstanceID(), out var recycledTime);
                //当前物体已过期 释放
                if (realtimeSinceStartup - recycledTime > _expiredTime)
                {
                    Release(obj);
                    hasReleaseCount++;
                }

                //本次释放数量达到上限 小于等于最小缓存数量 不继续释放
                if (hasReleaseCount >= _maxReleaseCount || _objList.Count <= _minCacheNum)
                {
                    return;
                }
            }
        }

        /// <summary>
        /// 生成
        /// </summary>
        /// <returns></returns>
        public GameObject Spawn()
        {
            if (_objList.Count <= 0)
            {
                return default;
            }

            var obj = _objList[0];
            obj.name = _name;
            _objList.RemoveAt(0);
            return obj;
        }

        /// <summary>
        /// 回收
        /// </summary>
        /// <param name="obj">待回收的物体</param>
        /// <param name="parent">被回收后放在哪个节点下</param>
        public void Recycle(GameObject obj, Transform parent)
        {
            var trans = obj.transform;
            trans.SetParent(parent, false);
            _objList.Add(obj);
            if (_insId2RecycledTime.ContainsKey(obj.GetInstanceID()))
            {
                _insId2RecycledTime[obj.GetInstanceID()] = Time.realtimeSinceStartup;
                return;
            }

            _insId2RecycledTime.Add(obj.GetInstanceID(), Time.realtimeSinceStartup);
        }

        /// <summary>
        /// 清理池 销毁全部物体
        /// </summary>
        public void Clear()
        {
            for (var i = _objList.Count - 1; i >= 0; i--)
            {
                Release(_objList[i]);
            }

            _objList.Clear();
        }

        /// <summary>
        /// 释放物体
        /// </summary>
        /// <param name="obj"></param>
        private void Release(GameObject obj)
        {
            if (_insId2RecycledTime.ContainsKey(obj.GetInstanceID()))
            {
                _insId2RecycledTime.RemoveSafe(obj.GetInstanceID());
            }

            _objList.Remove(obj);
            Object.Destroy(obj);
        }
    }
}