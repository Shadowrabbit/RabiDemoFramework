// ******************************************************************
//       /\ /|       @file       ObjectPoolManager.cs
//       \ V/        @brief      对象池管理器 管理UI以外的物体生成
//       | "")       @author     Shadowrabbit, yue.wang04@mihoyo.com
//       /  |                    
//      /  \\        @Modified   2022-03-15 20:00:50
//    *(__\_\        @Copyright  Copyright (c)  2022, Shadowrabbit
// ******************************************************************

using System.Collections.Generic;
using UnityEngine;

namespace Rabi
{
    public sealed class ObjectPoolManager : BaseSingleTon<ObjectPoolManager>, IMonoManager
    {
        private GameObject _root; //根节点
        private readonly Dictionary<string, ObjectPool> _objectPoolDict = new Dictionary<string, ObjectPool>(); //对象池
        private readonly Dictionary<string, Transform> _transformDict = new Dictionary<string, Transform>(); //对象池的挂点

        public void OnInit()
        {
            _root = new GameObject("ObjectPoolManager");
            _root.transform.SetParent(GameManager.Instance.transform, false);
            Logger.Log("对象池管理器初始化");
        }

        public void Update()
        {
            foreach (var objectPool in _objectPoolDict.Values)
            {
                objectPool.Tick();
            }
        }

        public void FixedUpdate()
        {
        }

        public void LateUpdate()
        {
        }

        public void OnClear()
        {
            foreach (var objectPool in _objectPoolDict.Values)
            {
                objectPool.Clear();
            }
        }

        /// <summary>
        /// 生成
        /// </summary>
        /// <param name="objName"></param>
        /// <param name="args"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Spawn<T>(string objName, Transform parent, params object[] args)
        {
            var obj = Spawn(objName, parent);
            if (!obj)
            {
                return default;
            }

            var comp = obj.GetComponent<T>();
            if (comp != null)
            {
                //框架内物体 执行生成回调
                if (comp is Thing thing)
                {
                    thing.OnSpawn(args);
                }

                return comp;
            }

            Logger.Error($"找不到组件 obj:{obj.name} comp:{typeof(T)}");
            return default;
        }

        /// <summary>
        /// 生成
        /// </summary>
        /// <param name="objName"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public GameObject Spawn(string objName, Transform parent)
        {
            var pool = GetPool(objName);
            if (pool == null || pool.IsPoolEmpty)
            {
                return null;
            }

            var obj = pool.Spawn();
            obj.transform.SetParent(parent, false);
            //默认生成处理
            obj.SetActive(true);
            return obj;
        }

        /// <summary>
        /// 回收
        /// </summary>
        /// <param name="poolObj"></param>
        public void Recycle(GameObject poolObj)
        {
            if (!poolObj)
            {
                return;
            }

            //该物体已经隐藏了 不可重复回收 会导致池子里对同一个物体多份引用
            if (!poolObj.activeSelf)
            {
                return;
            }

            //框架物体
            var thing = poolObj.GetComponent<Thing>();
            if (thing)
            {
                thing.OnRecycle();
            }

            //默认回收处理
            poolObj.SetActive(false);
            var trans = poolObj.transform;
            trans.localPosition = Vector3.zero;
            //池子不存在
            var pool = GetPool(poolObj.name) ?? CreatePool(poolObj.name);
            pool.Recycle(poolObj.gameObject, _transformDict[poolObj.name]);
        }

        /// <summary>
        /// 回收
        /// </summary>
        /// <param name="thing"></param>
        public void Recycle(Thing thing)
        {
            Recycle(thing.gameObject);
        }

        /// <summary>
        /// 设置对象池参数
        /// </summary>
        /// <param name="protoObj"></param>
        /// <param name="expiredTime"></param>
        /// <param name="minCacheNum"></param>
        /// <param name="maxReleaseCount"></param>
        /// <param name="releaseInterval"></param>
        public void SetPoolParams(GameObject protoObj, float expiredTime = 300, int minCacheNum = 0,
            int maxReleaseCount = 4, float releaseInterval = 30)
        {
            //池子不存在
            var pool = GetPool(protoObj.name) ?? CreatePool(protoObj.name);
            pool.SetParams(expiredTime, minCacheNum, maxReleaseCount, releaseInterval);
        }

        /// <summary>
        /// 预加载
        /// </summary>
        /// <param name="protoObj"></param>
        /// <param name="preloadNum"></param>
        public void Preload(GameObject protoObj, int preloadNum)
        {
            if (preloadNum <= 0)
            {
                return;
            }

            for (var i = 0; i < preloadNum; i++)
            {
                var obj = Object.Instantiate(protoObj);
                var thing = obj.GetComponent<Thing>();
                if (thing)
                {
                    thing.OnInit();
                    thing.OnSpawn();
                }

                obj.name = protoObj.name;
                Recycle(obj);
            }
        }

        /// <summary>
        /// 创建对象池
        /// </summary>
        /// <param name="objName"></param>
        /// <returns></returns>
        private ObjectPool CreatePool(string objName)
        {
            //节点存在
            if (_transformDict.ContainsKey(objName))
            {
                return new ObjectPool(objName, _transformDict[objName]);
            }

            //节点不存在 创建节点
            var nodeObj = new GameObject(objName);
            var nodeTrans = nodeObj.transform;
            nodeTrans.SetParent(_root.transform, false);
            _transformDict.Add(objName, nodeTrans);
            //创建池
            var pool = new ObjectPool(objName, nodeTrans);
            _objectPoolDict.Add(objName, pool);
            return pool;
        }

        /// <summary>
        /// 获取池
        /// </summary>
        /// <param name="objName"></param>
        /// <returns></returns>
        private ObjectPool GetPool(string objName)
        {
            return _objectPoolDict.ContainsKey(objName) ? _objectPoolDict[objName] : null;
        }
    }
}