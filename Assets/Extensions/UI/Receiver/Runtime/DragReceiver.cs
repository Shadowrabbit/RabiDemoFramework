// ******************************************************************
//       /\ /|       @file       DragReceiver
//       \ V/        @brief      拖拽接收器
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2022-08-19 11:17
//    *(__\_\        @Copyright  Copyright (c) 2022, Shadowrabbit
// ******************************************************************

using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Rabi
{
    public class DragReceiver : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private Action<PointerEventData> _onBeginDrag;
        private Action<PointerEventData> _onDrag;
        private Action<PointerEventData> _onEndDrag;

        public void OnBeginDrag(PointerEventData eventData)
        {
            _onBeginDrag?.Invoke(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            _onDrag?.Invoke(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _onEndDrag?.Invoke(eventData);
        }

        /// <summary>
        /// 设置开始拖拽回调函数
        /// </summary>
        /// <param name="onBeginDrag"></param>
        public void SetOnBeginDrag(Action<PointerEventData> onBeginDrag)
        {
            _onBeginDrag = onBeginDrag;
        }

        /// <summary>
        /// 设置拖拽中回调函数
        /// </summary>
        /// <param name="onDrag"></param>
        public void SetOnDrag(Action<PointerEventData> onDrag)
        {
            _onDrag = onDrag;
        }

        /// <summary>
        /// 设置结束拖拽回调函数
        /// </summary>
        /// <param name="onEndDrag"></param>
        public void SetOnEndDrag(Action<PointerEventData> onEndDrag)
        {
            _onEndDrag = onEndDrag;
        }
    }
}