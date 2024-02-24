// ******************************************************************
//       /\ /|       @file       DialogScreenPositionFix.cs
//       \ V/        @brief      屏幕坐标修正弹窗（修正弹窗UI超出屏幕）
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |        @tips       使用前确保目标UI和弹窗UI重心为（0,1）
//      /  \\        @Modified   2021-01-12 10:26:28
//    *(__\_\        @Copyright  Copyright (c) 2021, Shadowrabbit
// ******************************************************************

using System;
using UnityEngine;

namespace Rabi
{
    public static class DialogScreenPositionFix
    {
        /// <summary>
        /// 修正位置 将弹窗UI按照规则放置在目标UI附近 X居左其次居右 Y居中
        /// </summary>
        /// <param name="compRectTransformDialog">弹窗UI</param>
        /// <param name="compRectTransformTargetUI">目标UI</param>
        /// <param name="compRectTransformCanvas">画布</param>
        /// <param name="spacingX">X轴间距</param>
        /// <param name="spacingY">Y轴间距</param>
        /// <param name="compCameraUI">UI相机</param>
        public static void FixPosition(RectTransform compRectTransformDialog, RectTransform compRectTransformTargetUI,
            Camera compCameraUI, RectTransform compRectTransformCanvas, float spacingX, float spacingY)
        {
            var compCanvas = compRectTransformCanvas.GetComponent<Canvas>() ?? throw new Exception("找不到canvas组件");
            //左上角视口坐标
            var viewportPosLeftTop = new Vector3(0, 1, -compCameraUI.transform.position.z);
            //左上角屏幕坐标
            var screenPosLeftTop = compCameraUI.ViewportToScreenPoint(viewportPosLeftTop);
            //目标UI的屏幕坐标
            var screenPosTargetUI = compCameraUI.WorldToScreenPoint(compRectTransformTargetUI.transform.position);
            //canvas对于屏幕坐标系的缩放
            var scaleFactor = compCanvas.scaleFactor;
            //目标UI的屏幕坐标系宽高
            var tempRect = compRectTransformTargetUI.rect;
            var screenVhTargetUI = new Vector2(tempRect.width * scaleFactor, tempRect.height * scaleFactor);
            //弹窗UI的屏幕坐标系宽高
            tempRect = compRectTransformDialog.rect;
            var screenVhDialogUI = new Vector2(tempRect.width * scaleFactor, tempRect.height * scaleFactor);
            //屏幕坐标系下X轴间距
            var screenSpacingX = spacingX * scaleFactor;
            //屏幕坐标系下Y轴间距
            var screenSpacingY = spacingY * scaleFactor;
            //屏幕坐标系下的完美X轴坐标 完美X坐标 = 目标UI的X坐标 - 屏幕左侧X坐标 - 弹窗的宽度 - X间距
            var screenPerfectX = screenPosTargetUI.x - screenPosLeftTop.x - screenVhDialogUI.x - screenSpacingX;
            //屏幕坐标系下的最终修正X坐标
            var screenFixPosX = screenPerfectX >= 0
                ? screenPerfectX
                : screenPosTargetUI.x + screenVhTargetUI.x + spacingX;
            //屏幕坐标系下的完美Y轴坐标 完美Y坐标 = 目标UI的Y轴坐标 - 0.5f * 目标UI的高度 + 0.5f * 弹窗的高度
            var screenPerfectY = screenPosTargetUI.y - 0.5f * screenVhTargetUI.y + 0.5f * screenVhDialogUI.y +
                                 screenSpacingY;
            //屏幕坐标系下的最终修正Y坐标
            float screenFixPosY;
            switch (screenPerfectY <= screenPosLeftTop.y - screenSpacingY)
            {
                //没有超出屏幕范围
                case true when screenPerfectY >= screenVhDialogUI.y + screenSpacingY:
                    screenFixPosY = screenPerfectY;
                    break;
                //下方超出屏幕范围
                case true:
                    screenFixPosY = screenVhDialogUI.y + screenSpacingY;
                    break;
                //上方超出屏幕范围
                default:
                    screenFixPosY = screenPosLeftTop.y - screenSpacingY;
                    break;
            }

            //屏幕坐标系最终坐标
            var screenFixPos = new Vector3(screenFixPosX, screenFixPosY);
            //世界坐标系最终坐标
            var worldFixPos = compCameraUI.ScreenToWorldPoint(screenFixPos);
            compRectTransformDialog.transform.position = worldFixPos;
            //ui坐标系z值清空
            var anchoredPosition = compRectTransformDialog.anchoredPosition;
            compRectTransformDialog.anchoredPosition3D = new Vector3(anchoredPosition.x,
                anchoredPosition.y, 0);
        }

        /// <summary>
        /// 修正位置 将弹窗UI按照规则放置在目标UI附近 X居中 Y上
        /// </summary>
        /// <param name="compRectTransformDialog">弹窗UI</param>
        /// <param name="compRectTransformTargetUI">目标UI</param>
        /// <param name="compRectTransformCanvas">画布</param>
        /// <param name="spacingX">X轴间距</param>
        /// <param name="spacingY">Y轴间距</param>
        /// <param name="compCameraUI">UI相机</param>
        public static void FixPositionType2(RectTransform compRectTransformDialog,
            RectTransform compRectTransformTargetUI,
            Camera compCameraUI, RectTransform compRectTransformCanvas, float spacingX, float spacingY)
        {
            var compCanvas = compRectTransformCanvas.GetComponent<Canvas>() ?? throw new Exception("找不到canvas组件");
            //左上角视口坐标
            var position = compCameraUI.transform.position;
            var viewportPosLeftTop = new Vector3(0, 1, -position.z);
            var viewportPosRightBottom = new Vector3(1, 0, -position.z);
            //左上角屏幕坐标
            var screenPosLeftTop = compCameraUI.ViewportToScreenPoint(viewportPosLeftTop);
            var screenPosRightBottom = compCameraUI.ViewportToScreenPoint(viewportPosRightBottom);
            //目标UI的屏幕坐标
            var screenPosTargetUI = compCameraUI.WorldToScreenPoint(compRectTransformTargetUI.transform.position);
            //canvas对于屏幕坐标系的缩放
            var scaleFactor = compCanvas.scaleFactor;
            //目标UI的屏幕坐标系宽高
            var tempRect = compRectTransformTargetUI.rect;
            var screenVhTargetUI = new Vector2(tempRect.width * scaleFactor, tempRect.height * scaleFactor);
            //弹窗UI的屏幕坐标系宽高
            tempRect = compRectTransformDialog.rect;
            var screenVhDialogUI = new Vector2(tempRect.width * scaleFactor, tempRect.height * scaleFactor);
            //屏幕坐标系下X轴间距
            var screenSpacingX = spacingX * scaleFactor;
            //屏幕坐标系下Y轴间距
            var screenSpacingY = spacingY * scaleFactor;
            //屏幕坐标系下的完美X轴坐标 完美X坐标 = 目标UI的X坐标 - 屏幕左侧X坐标 + 目标UI的宽度/2 - 弹窗的宽度/2 + X间距
            var screenPerfectX = screenPosTargetUI.x - screenPosLeftTop.x + screenVhTargetUI.x / 2 -
                screenVhDialogUI.x / 2 + screenSpacingX;
            //屏幕坐标系下的最终修正X坐标
            float screenFixPosX;
            switch (screenPerfectX >= screenPosLeftTop.x)
            {
                //没有超出屏幕范围
                case true when screenPerfectX <= screenPosRightBottom.x:
                    screenFixPosX = screenPerfectX;
                    break;
                //右侧超出范围
                case true:
                    screenFixPosX = screenPosRightBottom.x - spacingX - screenVhTargetUI.x;
                    break;
                //左侧超出范围
                default:
                    screenFixPosX = screenPosLeftTop.x + spacingX;
                    break;
            }

            //屏幕坐标系下的完美Y轴坐标 完美Y坐标 = 目标UI的Y轴坐标 + 弹窗的高度 + 间距
            var screenPerfectY = screenPosTargetUI.y + screenVhDialogUI.y + spacingY;
            //屏幕坐标系下的最终修正Y坐标
            float screenFixPosY;
            switch (screenPerfectY <= screenPosLeftTop.y - screenSpacingY)
            {
                //没有超出屏幕范围
                case true when screenPerfectY >= screenVhDialogUI.y + screenSpacingY:
                    screenFixPosY = screenPerfectY;
                    break;
                //下方超出屏幕范围
                case true:
                    screenFixPosY = screenVhDialogUI.y + screenSpacingY;
                    break;
                //上方超出屏幕范围
                default:
                    screenFixPosY = screenPosLeftTop.y - screenSpacingY;
                    break;
            }

            //屏幕坐标系最终坐标
            var screenFixPos = new Vector3(screenFixPosX, screenFixPosY);
            //世界坐标系最终坐标
            var worldFixPos = compCameraUI.ScreenToWorldPoint(screenFixPos);
            compRectTransformDialog.transform.position = worldFixPos;
            //ui坐标系z值清空
            var anchoredPosition = compRectTransformDialog.anchoredPosition;
            compRectTransformDialog.anchoredPosition3D = new Vector3(anchoredPosition.x,
                anchoredPosition.y, 0);
        }
    }
}