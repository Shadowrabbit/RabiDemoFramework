// ******************************************************************
//       /\ /|       @file       Thing.cs
//       \ V/        @brief      所有场景中的物体基类
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2022-04-02 08:58:20
//    *(__\_\        @Copyright  Copyright (c) 2022, Shadowrabbit
// ******************************************************************

using System.Collections.Generic;
using UnityEngine;

namespace Rabi
{
    public abstract class Thing : MonoBehaviour
    {
        private Transform _cacheTransform; //自身缓存
        private static readonly Dictionary<string, (Vector3, Quaternion)> DefaultStatusDict =
            new Dictionary<string, (Vector3, Quaternion)>(); //对象的名称,（默认缩放,默认旋转）

        /// <summary>
        /// 尺寸
        /// </summary>
        public virtual Vector2 ColliderSize => Vector2.zero;

        private Transform CacheTransform
        {
            get
            {
                if (_cacheTransform == null)
                {
                    _cacheTransform = transform;
                }

                return _cacheTransform;
            }
        }

        /// <summary>
        /// 初始化回调
        /// </summary>
        public virtual void OnInit()
        {
            //第一次实例化的物体
            if (DefaultStatusDict.ContainsKey(gameObject.name))
            {
                return;
            }

            //记录默认的缩放 旋转
            DefaultStatusDict.Add(gameObject.name, (CacheTransform.localScale, CacheTransform.rotation));
        }

        /// <summary>
        /// 启用回调
        /// </summary>
        /// <param name="args"></param>
        public virtual void OnSpawn(params object[] args)
        {
            gameObject.SetActive(true);
        }

        /// <summary>
        /// 禁用回调
        /// </summary>
        public virtual void OnRecycle()
        {
            if (!DefaultStatusDict.TryGetValue(gameObject.name, out var pair))
            {
                return;
            }

            //有这个物体的记录 还原缩放 旋转
            CacheTransform.localScale = pair.Item1;
            CacheTransform.rotation = pair.Item2;
        }
    }
}