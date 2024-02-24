// ******************************************************************
//       /\ /|       @file       AssetManager.cs
//       \ V/        @brief      资源管理器 只提供加载 卸载
//       | "")       @author     Shadowrabbit, yue.wang04@mihoyo.com
//       /  |                    
//      /  \\        @Modified   2022-03-15 19:53:45
//    *(__\_\        @Copyright  Copyright (c)  2022, Shadowrabbit
// ******************************************************************

using System;
using System.IO;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Rabi
{
    public sealed class AssetManager : BaseSingleTon<AssetManager>, IMonoManager
    {
        public void OnInit()
        {
            //提前初始化Addressable
            Logger.Log("资源管理器初始化");
        }

        public void Update()
        {
        }

        public void FixedUpdate()
        {
        }

        public void LateUpdate()
        {
        }

        public void OnClear()
        {
        }

        /// <summary>
        /// 同步加载(单个资源)
        /// </summary>
        /// <typeparam name="T">资源类型</typeparam>
        /// <param name="path">资源路径，如果为空则不会加载</param>
        public T LoadAssetSync<T>(string path) where T : Object
        {
            if (string.IsNullOrEmpty(path))
            {
                Logger.Error($"加载资源路径为空 path:{path}");
                return null;
            }

            var index = path.IndexOf('.');
            var newPath = path.Substring(0, index);
            return Resources.Load<T>(newPath.Replace("Assets/Resources/", ""));
        }

        /// <summary>
        /// 释放未在使用的资源
        /// </summary>
        public static void ClearUnused()
        {
            Resources.UnloadUnusedAssets();
            GC.Collect();
        }
    }
}