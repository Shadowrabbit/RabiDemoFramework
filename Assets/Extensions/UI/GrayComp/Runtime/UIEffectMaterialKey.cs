// ******************************************************************
//       /\ /|       @file       UIEffectMaterialKey.cs
//       \ V/        @brief      
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2022-05-17 10:47:37
//    *(__\_\        @Copyright  Copyright (c) 2022, Shadowrabbit
// ******************************************************************

using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Rendering;

namespace Rabi
{
    public struct UIEffectMaterialKey : IEquatable<UIEffectMaterialKey>
    {
        [UsedImplicitly] private EffectColorMode _blendColorMode;
        [UsedImplicitly] private byte _blurStrengh;
        [UsedImplicitly] private Texture2D _overlayTexture;
        [UsedImplicitly] private Color _overlayTint;
        [UsedImplicitly] private EffectColorMode _overlayColorMode;
        [UsedImplicitly] private Vector2 _overlaySpeed;
        public byte grayScale;

        public bool Equals(UIEffectMaterialKey other)
        {
            if (_blendColorMode != other._blendColorMode)
            {
                return false;
            }

            if (_blurStrengh != other._blurStrengh)
            {
                return false;
            }

            if (_overlayTexture != other._overlayTexture)
            {
                return false;
            }

            if (_overlayTint != other._overlayTint)
            {
                return false;
            }

            if (_overlayColorMode != other._overlayColorMode)
            {
                return false;
            }

            if (_overlaySpeed != other._overlaySpeed)
            {
                return false;
            }

            return grayScale == other.grayScale;
        }

        public Material UpdateMaterial(Material material)
        {
            EffectPropertyID.Initialize();
            if (_blendColorMode == EffectColorMode.Blend && _blurStrengh == 0 && _overlayTexture == null &&
                grayScale == 0)
            {
                return null;
            }

            var shader = Shader.Find("Rabi/UIEffect");
            if (shader == null)
            {
                Debug.LogError("找不到shader");
                return null;
            }

            if (material == null)
            {
                material = new Material(shader);
            }
            else
            {
                material.shader = shader;
            }

            material.SetInt(EffectPropertyID.SrcBlend, (int)BlendMode.SrcAlpha);
            switch (_blendColorMode)
            {
                case EffectColorMode.Additive:
                    material.SetInt(EffectPropertyID.DstBlend, (int)BlendMode.One);
                    break;
                case EffectColorMode.Blend:
                default:
                    material.SetInt(EffectPropertyID.DstBlend, (int)BlendMode.OneMinusSrcAlpha);
                    break;
            }

            SetProperties(material);
            return material;
        }

        private void SetProperties(Material material)
        {
            if (_blurStrengh > 0)
            {
                material.EnableKeyword("UIEFFECT_BLUR");
                material.SetFloat(EffectPropertyID.BlurDistance, 0.1f * (_blurStrengh / 255.0f));
            }
            else
            {
                material.DisableKeyword("UIEFFECT_BLUR");
            }

            if (_overlayTexture != null)
            {
                material.EnableKeyword("UIEFFECT_OVERLAY");
                material.SetTexture(EffectPropertyID.OverlayTex, _overlayTexture);
                material.SetInt(EffectPropertyID.OverlayColorMode, (int)_overlayColorMode);
                if (_overlayTint != Color.white)
                {
                    material.EnableKeyword("UIEFFECT_OVERLAY_TINT");
                    material.SetColor(EffectPropertyID.OverlayTint, _overlayTint);
                }
                else
                {
                    material.DisableKeyword("UIEFFECT_OVERLAY_TINT");
                }

                if (_overlaySpeed != Vector2.zero)
                {
                    material.EnableKeyword("UIEFFECT_OVERLAY_ANIMATION");
                    material.SetVector(EffectPropertyID.OverlaySpeed, _overlaySpeed);
                }
                else
                {
                    material.DisableKeyword("UIEFFECT_OVERLAY_ANIMATION");
                }
            }
            else
            {
                material.DisableKeyword("UIEFFECT_OVERLAY");
            }

            if (grayScale > 0f)
            {
                material.EnableKeyword("UIEFFECT_GRAYSCALE");
                material.EnableKeyword("UIEFFECT_GRAYSCALE_LERP");
                var v = 1.0f - grayScale / 255.0f;
                material.SetFloat(EffectPropertyID.GrayLerp, v);
            }
            else
            {
                material.DisableKeyword("UIEFFECT_GRAYSCALE");
                material.DisableKeyword("UIEFFECT_GRAYSCALE_LERP");
            }
        }
    }
}