// ******************************************************************
//       /\ /|       @file       ButtonLongPressHandler
//       \ V/        @brief      长按接收器
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2023-10-21 21:24
//    *(__\_\        @Copyright  Copyright (c) 2023, Shadowrabbit
// ******************************************************************

using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Rabi
{
    public class LongPressReceiver : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private float minDuration = 1f; //最小触发时间 毫秒
        private Action _onLongPress; // 用于通知其他组件长按状态的委托事件
        private float _pressedTime; // 按下的时间记录器
        private bool _isPressed; //是否按下

        protected void Update()
        {
            if (!_isPressed)
            {
                return;
            }

            var timeDelta = Time.time - _pressedTime;
            if (timeDelta < minDuration)
            {
                return;
            }

            _onLongPress?.Invoke();
            _isPressed = false;
        }

        /// <summary>
        /// 设置长按状态改变回调
        /// </summary>
        /// <param name="action"></param>
        public void SetOnPressedStatusChanged(Action action)
        {
            _onLongPress = action;
        }

        /// <summary>
        /// 当鼠标或触摸 pointer 下落时调用
        /// </summary>
        /// Mouse or touch position data.
        public void OnPointerDown(PointerEventData eventData)
        {
            if (!eventData.eligibleForClick)
            {
                return;
            }

            _pressedTime = Time.time;
            _isPressed = true;
        }

        /// <summary>
        /// 当 mouse 或 touch pointer 上升且没有点击发生时调用。如果指针已经上升但发生了点击则不会调用此方法，而是调用 OnPointerClick 方法。
        /// </summary>
        public void OnPointerUp(PointerEventData eventData)
        {
            _isPressed = false;
        }
    }
}