// ******************************************************************
//       /\ /|       @file       GrayStateNode.cs
//       \ V/        @brief      灰度控制状态节点
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2022-05-18 08:44:10
//    *(__\_\        @Copyright  Copyright (c) 2022, Shadowrabbit
// ******************************************************************

using System;
using UnityEngine;

namespace Rabi
{
    [Serializable]
    public class GrayStateNode : BaseStateNode
    {
        private GrayComp _grayComp; //灰度组件
        public int stateValue; //保存的设置

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="obj"></param>
        public override void OnInit(GameObject obj)
        {
            base.OnInit(obj);
            _grayComp = controller.gameObject.GetComponent<GrayComp>() ??
                        controller.gameObject.AddComponent<GrayComp>();
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
            if (_grayComp.GrayScale == stateValue)
            {
                return;
            }

            _grayComp.GrayScale = stateValue;
        }
    }
}