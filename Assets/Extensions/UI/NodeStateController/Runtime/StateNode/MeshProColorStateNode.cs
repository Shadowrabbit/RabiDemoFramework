// ******************************************************************
//       /\ /|       @file       MeshProColorStateNode
//       \ V/        @brief      文本颜色状态节点
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2022-12-06 10:44
//    *(__\_\        @Copyright  Copyright (c) 2022, Shadowrabbit
// ******************************************************************

using System;
using TMPro;
using UnityEngine;

namespace Rabi
{
    [Serializable]
    public class MeshProColorStateNode : BaseStateNode
    {
        private TextMeshProUGUI _textMeshPro;
        public Color stateValue; //保存的设置

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
            if (_textMeshPro.color == stateValue)
            {
                return;
            }

            _textMeshPro.color = stateValue;
        }
    }
}