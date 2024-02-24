// ******************************************************************
//       /\ /|       @file       CameraEx
//       \ V/        @brief      相机扩展
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2023-01-04 17:28
//    *(__\_\        @Copyright  Copyright (c) 2023, Shadowrabbit
// ******************************************************************

using UnityEngine;

namespace Rabi
{
    public static class CameraEx
    {
        /// <summary>
        /// 截图
        /// </summary>
        public static Texture2D ScreenShot(this Camera camera, Rect rect)
        {
            var renderTexture = new RenderTexture(Screen.width, Screen.height, 0);
            camera.targetTexture = renderTexture;
            camera.Render();
            RenderTexture.active = renderTexture;
            var screenShot = new Texture2D((int) rect.width, (int) rect.height, TextureFormat.RGB24, false);
            screenShot.ReadPixels(rect, 0, 0);
            screenShot.Apply();
            camera.targetTexture = null;
            RenderTexture.active = null;
            Object.Destroy(renderTexture);
            return screenShot;
        }
    }
}