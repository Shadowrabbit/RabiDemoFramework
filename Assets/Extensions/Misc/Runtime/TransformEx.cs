// ******************************************************************
//       /\ /|       @file       TransformEx.cs
//       \ V/        @brief      
//       | "")       @author     Shadowrabbit, yue.wang04@mihoyo.com
//       /  |                    
//      /  \\        @Modified   2022-06-23 11:55:13
//    *(__\_\        @Copyright  Copyright (c)  2022, Shadowrabbit
// ******************************************************************

using System;
using UnityEngine;

namespace Rabi
{
    public static class TransformEx
    {
        /// <summary>
        /// 重置缩放
        /// </summary>
        /// <param name="component"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ResetLocalScale<T>(this T component) where T : Component
        {
            component.transform.localScale = Vector3.one;
            return component;
        }
        
        /// <summary>
        /// 获取血条挂点
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static Transform GetHpSocket(this Transform unit)
        {
            return unit.Find("HpSocket");
        }
        
        /// <summary>
        /// 获取unitCurHud挂点
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static Transform GetUnitCurSocket(this Transform unit)
        {
            return unit.Find("UnitCurSocket");
        }
    }
}