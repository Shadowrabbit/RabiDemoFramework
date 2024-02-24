// ******************************************************************
//       /\ /|       @file       SpriteStateNode
//       \ V/        @brief      sprite图片切换节点
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2023-11-17 18:13
//    *(__\_\        @Copyright  Copyright (c) 2023, Shadowrabbit
// ******************************************************************

using System;
using UnityEngine;
using UnityEngine.UI;

namespace Rabi
{
    [Serializable]
    public class SpriteStateNode : BaseStateNode
    {
        private Image _graphicComp; //图形组件
        public Sprite stateValue; //保存的设置

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="obj"></param>
        public override void OnInit(GameObject obj)
        {
            base.OnInit(obj);
            _graphicComp = controller.gameObject.GetComponent<Image>() ??
                           controller.gameObject.AddComponent<Image>();
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
            if (_graphicComp.sprite == stateValue)
            {
                return;
            }

            _graphicComp.sprite = stateValue;
        }
    }
}