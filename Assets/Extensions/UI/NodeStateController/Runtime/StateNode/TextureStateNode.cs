// ******************************************************************
//       /\ /|       @file       TextureStateNode
//       \ V/        @brief      texture图片切换节点
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2023-11-17 18:59
//    *(__\_\        @Copyright  Copyright (c) 2023, Shadowrabbit
// ******************************************************************

using System;
using UnityEngine;
using UnityEngine.UI;

namespace Rabi
{
    [Serializable]
    public class TextureStateNode : BaseStateNode
    {
        private RawImage _graphicComp; //图形组件
        public Texture stateValue; //保存的设置

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="obj"></param>
        public override void OnInit(GameObject obj)
        {
            base.OnInit(obj);
            _graphicComp = controller.gameObject.GetComponent<RawImage>() ??
                           controller.gameObject.AddComponent<RawImage>();
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
            if (_graphicComp.texture == stateValue)
            {
                return;
            }

            _graphicComp.texture = stateValue;
        }
    }
}