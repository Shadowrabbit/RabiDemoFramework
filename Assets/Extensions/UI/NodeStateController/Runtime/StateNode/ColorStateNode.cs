// ******************************************************************
//       /\ /|       @file       ColorStateNode.cs
//       \ V/        @brief      颜色状态节点
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2022-05-16 12:46:03
//    *(__\_\        @Copyright  Copyright (c) 2022, Shadowrabbit
// ******************************************************************

using System;
using UnityEngine;
using UnityEngine.UI;

namespace Rabi
{
    [Serializable]
    public class ColorStateNode : BaseStateNode
    {
        private Graphic _graphicComp; //图形组件
        public Color stateValue; //保存的设置

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="obj"></param>
        public override void OnInit(GameObject obj)
        {
            base.OnInit(obj);
            _graphicComp = controller.gameObject.GetComponent<Graphic>() ??
                           controller.gameObject.AddComponent<Graphic>();
        }

        /// <summary>
        /// 状态改变
        /// </summary>
        /// <param name="state"></param>
        public override void OnStateChanged(int state)
        {
            //不是触发状态 不是默认值
            if (triggerState != state)
            {
                return;
            }

            //触发状态
            if (_graphicComp.color == stateValue)
            {
                return;
            }

            _graphicComp.color = stateValue;
        }
    }
}