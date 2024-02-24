// ******************************************************************
//       /\ /|       @file       GameObjectEx
//       \ V/        @brief      obj扩展
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2022-05-25 12:33
//    *(__\_\        @Copyright  Copyright (c) 2022, Shadowrabbit
// ******************************************************************

using UnityEngine;

namespace Rabi
{
    public static class GameObjectEx
    {
        /// <summary>
        /// 有容错的方式 取得某个组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="go"></param>
        /// <returns></returns>
        public static T GetOrAddComponent<T>(this GameObject go) where T : Component
        {
            var t = go.GetComponent<T>();
            return t ? t : go.AddComponent<T>();
        }
    }
}