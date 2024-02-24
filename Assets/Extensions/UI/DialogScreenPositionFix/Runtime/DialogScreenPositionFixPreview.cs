// ******************************************************************
//       /\ /|       @file       DialogScreenPositionFixPreview.cs
//       \ V/        @brief      DialogScreenPositionFix预览器
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2021-01-12 16:16:46
//    *(__\_\        @Copyright  Copyright (c) 2021, Shadowrabbit
// ******************************************************************

using JetBrains.Annotations;
using UnityEngine;

namespace Rabi
{
    public class DialogScreenPositionFixPreview : MonoBehaviour
    {
        public RectTransform compRectTransformDialog;
        public RectTransform compRectTransformTargetUI;
        public Camera compCameraUI;
        public RectTransform compRectTransformCanvas;
        public float spacingX;
        public float spacingY;

        /// <summary>
        /// 修正坐标
        /// </summary>
        [UsedImplicitly]
        private void FixPosition()
        {
            DialogScreenPositionFix.FixPosition(compRectTransformDialog, compRectTransformTargetUI, compCameraUI,
                compRectTransformCanvas, spacingX, spacingY);
        }

        /// <summary>
        /// 修正坐标
        /// </summary>
        [UsedImplicitly]
        private void FixPositionType2()
        {
            DialogScreenPositionFix.FixPositionType2(compRectTransformDialog, compRectTransformTargetUI, compCameraUI,
                compRectTransformCanvas, spacingX, spacingY);
        }
    }
}