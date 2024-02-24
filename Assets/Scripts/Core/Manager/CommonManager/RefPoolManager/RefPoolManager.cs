// ******************************************************************
//       /\ /|       @file       RefPoolManager.cs
//       \ V/        @brief      引用池管理器
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2022-03-20 05:21:34
//    *(__\_\        @Copyright  Copyright (c) 2022, Shadowrabbit
// ******************************************************************

using System;
using System.Collections.Generic;

namespace Rabi
{
    public sealed class RefPoolManager : BaseSingleTon<RefPoolManager>
    {
        private readonly Dictionary<Type, RefPool> _refPoolDict = new Dictionary<Type, RefPool>();

        /// <summary>
        /// 入栈
        /// </summary>
        /// <param name="poolRef"></param>
        public void Push(IPoolRef poolRef)
        {
            var className = poolRef.GetType();
            if (!_refPoolDict.ContainsKey(className))
            {
                _refPoolDict[className] = new RefPool();
            }

            _refPoolDict[className].Push(poolRef);
        }

        /// <summary>
        /// 出栈
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Pop<T>() where T : class, IPoolRef
        {
            var className = typeof(T);
            if (!_refPoolDict.ContainsKey(className))
            {
                return default;
            }

            var poolRef = _refPoolDict[className].Pop();
            poolRef.OnReset();
            return poolRef as T;
        }

        /// <summary>
        /// 释放池
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void ReleasePool<T>() where T : class, IPoolRef
        {
            var className = typeof(T);
            if (!_refPoolDict.ContainsKey(className))
            {
                return;
            }

            _refPoolDict[className].ReleaseAll();
        }

        /// <summary>
        /// 释放全部池
        /// </summary>
        public void ReleaseAllPools()
        {
            foreach (var refPool in _refPoolDict.Values)
            {
                refPool.ReleaseAll();
            }

            _refPoolDict.Clear();
        }
    }
}