// ******************************************************************
//       /\ /|       @file       ImageEx.cs
//       \ V/        @brief      Image扩展
//       | "")       @author     Shadowrabbit, yue.wang04@mihoyo.com
//       /  |                    
//      /  \\        @Modified   2022-03-18 12:50:23
//    *(__\_\        @Copyright  Copyright (c)  2022, Shadowrabbit
// ******************************************************************

using UnityEngine;
using UnityEngine.UI;

namespace Rabi
{
    public static class ImageEx
    {
        /// <summary>
        /// 同步加载sp 通过路径
        /// </summary>
        /// <param name="image"></param>
        /// <param name="spritePath"></param>
        /// <param name="needSetNativeSize"></param>
        public static void SetSpriteSync(this Image image, string spritePath, bool needSetNativeSize = false)
        {
            if (string.IsNullOrEmpty(spritePath))
            {
                Logger.Error($"sprite加载路径异常 spritePath: {spritePath}");
                return;
            }

            image.SetSpriteSyncByPath(spritePath, needSetNativeSize);
        }

        /// <summary>
        /// 填充进度
        /// </summary>
        /// <param name="image"></param>
        /// <param name="fillAmount"></param>
        /// <returns></returns>
        public static Image FillAmount(this Image image, float fillAmount)
        {
            image.fillAmount = fillAmount;
            return image;
        }

        /// <summary>
        /// 异步加载sp 通过完整路径
        /// </summary>
        /// <param name="image"></param>
        /// <param name="spritePath"></param>
        /// <param name="needSetNativeSize"></param>
        private static void SetSpriteSyncByPath(this Image image, string spritePath,
            bool needSetNativeSize = false)
        {
            var sp = AssetManager.Instance.LoadAssetSync<Sprite>(spritePath);
            image.sprite = sp;
            //普通类别 设置原尺寸
            if (needSetNativeSize)
            {
                image.SetNativeSize();
            }
        }
    }
}