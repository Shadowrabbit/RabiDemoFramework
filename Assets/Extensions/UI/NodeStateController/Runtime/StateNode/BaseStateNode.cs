// ******************************************************************
//       /\ /|       @file       BaseStateNode.cs
//       \ V/        @brief      基础状态节点
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2022-05-15 11:13:20
//    *(__\_\        @Copyright  Copyright (c) 2022, Shadowrabbit
// ******************************************************************

using System;
using UnityEngine;

namespace Rabi
{
    [Serializable]
    public abstract class BaseStateNode
    {
        public int triggerState; //触发状态
        protected GameObject controller; //所属的UI状态控制器

        /// <summary>
        /// 初始化回调
        /// </summary>
        public virtual void OnInit(GameObject obj)
        {
            controller = obj;
        }

        /// <summary>
        /// UI状态改变回调
        /// </summary>
        public virtual void OnStateChanged(int state)
        {
        }
    }
}