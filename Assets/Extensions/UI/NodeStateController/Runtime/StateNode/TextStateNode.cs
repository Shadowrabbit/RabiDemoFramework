// ******************************************************************
//       /\ /|       @file       TextStateNode
//       \ V/        @brief      文本内容切换节点
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2023-11-17 19:01
//    *(__\_\        @Copyright  Copyright (c) 2023, Shadowrabbit
// ******************************************************************

using System;
using TMPro;
using UnityEngine;

namespace Rabi
{
    [Serializable]
    public class TextStateNode : BaseStateNode
    {
        private TextMeshProUGUI _textMeshPro; //图形组件
        public string stateValue; //保存的设置

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="obj"></param>
        public override void OnInit(GameObject obj)
        {
            base.OnInit(obj);
            _textMeshPro = controller.gameObject.GetComponent<TextMeshProUGUI>() ??
                           controller.gameObject.AddComponent<TextMeshProUGUI>();
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
            if (_textMeshPro.text == stateValue)
            {
                return;
            }

            _textMeshPro.text = stateValue;
        }
    }
}