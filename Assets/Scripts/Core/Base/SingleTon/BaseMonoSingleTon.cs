// ******************************************************************
//       /\ /|       @file       BaseMonoSingleTon.cs
//       \ V/        @brief      Mono单例基类
//       | "")       @author     Shadowrabbit, yue.wang04@mihoyo.com
//       /  |                    
//      /  \\        @Modified   2022-03-17 20:04:02
//    *(__\_\        @Copyright  Copyright (c)  2022, Shadowrabbit
// ******************************************************************

using System;
using UnityEngine;

namespace Rabi
{
    public class BaseMonoSingleTon<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                //实例不存在
                if (_instance != null) return _instance;
                //场景里找
                var obj = FindObjectOfType<T>(true);
                if (!obj)
                {
                    throw new Exception($"场景中找不到单例物体 name:{typeof(T)}");
                }

                //创建个新的
                _instance = obj;
                return _instance;
            }
        }
    }
}