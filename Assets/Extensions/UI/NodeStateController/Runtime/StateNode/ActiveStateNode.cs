// ******************************************************************
//       /\ /|       @file       ActiveStateNode.cs
//       \ V/        @brief      激活状态节点
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2022-05-15 11:29:11
//    *(__\_\        @Copyright  Copyright (c) 2022, Shadowrabbit
// ******************************************************************

using System;

namespace Rabi
{
    [Serializable]
    public class ActiveStateNode : BaseStateNode
    {
        public bool stateValue; //保存的设置

        /// <summary>
        /// 状态改变回调
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
            if (controller.gameObject.activeSelf == stateValue)
            {
                return;
            }

            controller.gameObject.SetActive(stateValue);
        }
    }
}