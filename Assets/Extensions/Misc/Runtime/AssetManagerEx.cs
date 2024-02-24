// ******************************************************************
//       /\ /|       @file       AssetManagerEx.cs
//       \ V/        @brief      资源管理器扩展 实例化
//       | "")       @author     Shadowrabbit, yue.wang04@mihoyo.com
//       /  |                    
//      /  \\        @Modified   2022-03-18 22:52:37
//    *(__\_\        @Copyright  Copyright (c)  2022, Shadowrabbit
// ******************************************************************

using UnityEngine;
using Object = UnityEngine.Object;

namespace Rabi
{
    public static class AssetManagerEx
    {
        /// <summary>
        /// 同步实例化
        /// </summary>
        /// <param name="assetManager"></param>
        /// <param name="assetPath">资源路径</param>
        /// <param name="parent"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static GameObject InstantiateSyncEx(this AssetManager assetManager, string assetPath, Transform parent,
            params object[] args)
        {
            //尝试从池中获取
            var objName = assetPath.GetAssetName();
            var obj = ObjectPoolManager.Instance.Spawn(objName, parent);
            var isNewInstance = false;
            if (obj == null)
            {
                isNewInstance = true;
                //同步加载并实例化
                obj = assetManager.InstantiateSync<GameObject>(assetPath, parent);
                obj.name = objName;
            }

            var thing = obj.GetComponent<Thing>();
            if (!thing)
            {
                return obj;
            }

            if (isNewInstance)
            {
                thing.OnInit();
            }

            thing.OnSpawn(args);
            return obj;
        }

        /// <summary>
        /// 根据原型体同步实例化
        /// </summary>
        /// <param name="assetManager"></param>
        /// <param name="proto">原型体</param>
        /// <param name="parent"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static GameObject InstantiateSyncEx(this AssetManager assetManager, GameObject proto, Transform parent,
            params object[] args)
        {
            //尝试从池中获取
            var obj = ObjectPoolManager.Instance.Spawn(proto.name, parent);
            var isNewInstance = false;
            if (obj == null)
            {
                isNewInstance = true;
                //异步加载并实例化
                obj = Object.Instantiate(proto, parent);
            }

            var thing = obj.GetComponent<Thing>();
            if (!thing)
            {
                return obj;
            }

            if (isNewInstance)
            {
                thing.OnInit();
            }

            thing.OnSpawn(args);
            return obj;
        }

        /// <summary>
        /// 同步获取实例
        /// </summary>
        /// <param name="assetManager"></param>
        /// <param name="assetPath">资源路径</param>
        /// <param name="parent"></param>
        /// <param name="args"></param>
        /// <typeparam name="T">资源类型</typeparam>
        /// <returns></returns>
        public static T InstantiateSyncEx<T>(this AssetManager assetManager, string assetPath, Transform parent,
            params object[] args)
        {
            //尝试从池中获取
            var objName = assetPath.GetAssetName();
            var ins = ObjectPoolManager.Instance.Spawn<T>(objName, parent, args);
            var isNewInstance = false;
            if (ins == null)
            {
                isNewInstance = true;
                //同步加载并实例化
                var obj = assetManager.InstantiateSync<GameObject>(assetPath, parent);
                obj.name = objName;
                ins = obj.GetComponent<T>();
            }

            if (ins is Thing thing)
            {
                if (isNewInstance)
                {
                    thing.OnInit();
                }

                thing.OnSpawn(args);
            }

            if (ins == null) Logger.Error($"找不到组件obj:{objName} comp:{typeof(T)}");
            return ins;
        }

        /// <summary>
        /// 回收实例
        /// </summary>
        /// <param name="assetManager"></param>
        /// <param name="poolObj">待回收对象</param>
        public static void Recycle(this AssetManager assetManager, GameObject poolObj)
        {
            ObjectPoolManager.Instance.Recycle(poolObj);
        }

        /// <summary>
        /// 回收实例
        /// </summary>
        /// <param name="assetManager"></param>
        /// <param name="thing"></param>
        public static void Recycle(this AssetManager assetManager, Thing thing)
        {
            ObjectPoolManager.Instance.Recycle(thing);
        }

        /// <summary>
        /// 同步实例化 优先考虑使用对象池实例化物体 而不是此函数
        /// </summary>
        /// <param name="assetManager"></param>
        /// <param name="path">资源路径</param>
        /// <param name="parent"></param>
        /// <typeparam name="T">资源类型</typeparam>
        /// <returns></returns>
        private static T InstantiateSync<T>(this AssetManager assetManager, string path, Transform parent)
            where T : Object
        {
            var asset = assetManager.LoadAssetSync<T>(path);
            return asset == null ? null : Object.Instantiate(asset, parent);
        }
    }
}