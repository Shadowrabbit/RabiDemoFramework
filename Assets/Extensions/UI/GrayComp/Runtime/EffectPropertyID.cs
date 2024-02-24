// ******************************************************************
//       /\ /|       @file       EffectPropertyID.cs
//       \ V/        @brief      
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2022-05-17 10:41:43
//    *(__\_\        @Copyright  Copyright (c) 2022, Shadowrabbit
// ******************************************************************

using UnityEngine;

namespace Rabi
{
    internal static class EffectPropertyID
    {
        private static bool _initialized;
        public static int SrcBlend { get; private set; }
        public static int DstBlend { get; private set; }
        public static int BlurDistance { get; private set; }
        public static int OverlaySpeed { get; private set; }
        public static int OverlayTint { get; private set; }
        public static int OverlayTex { get; private set; }
        public static int OverlayColorMode { get; private set; }
        public static int GrayLerp { get; private set; }

        public static void Initialize()
        {
            if (_initialized)
            {
                return;
            }

            SrcBlend = Shader.PropertyToID("_SrcBlend");
            DstBlend = Shader.PropertyToID("_DstBlend");
            BlurDistance = Shader.PropertyToID("_BlurDistance");
            OverlaySpeed = Shader.PropertyToID("_OverlaySpeed");
            OverlayTint = Shader.PropertyToID("_OverlayTint");
            OverlayTex = Shader.PropertyToID("_OverlayTex");
            OverlayColorMode = Shader.PropertyToID("_OverlayColorMode");
            GrayLerp = Shader.PropertyToID("_GrayLerp");
            _initialized = true;
        }
    }
}