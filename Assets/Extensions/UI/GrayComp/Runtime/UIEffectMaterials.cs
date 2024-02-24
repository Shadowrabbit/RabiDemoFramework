// ******************************************************************
//       /\ /|       @file       UIEffectMaterials.cs
//       \ V/        @brief      
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2022-05-17 11:30:28
//    *(__\_\        @Copyright  Copyright (c) 2022, Shadowrabbit
// ******************************************************************

using System.Collections.Generic;
using UnityEngine;

namespace Rabi
{
    internal static class UIEffectMaterials
    {
        private const int MaterialCacheCount = 16;
        private static readonly Stack<Material> Cachehs = new Stack<Material>();

        private static readonly Dictionary<UIEffectMaterialKey, MaterialRef> Materials =
            new Dictionary<UIEffectMaterialKey, MaterialRef>();

        private static readonly Dictionary<Material, UIEffectMaterialKey> Lookup =
            new Dictionary<Material, UIEffectMaterialKey>();

        public static Material Get(UIEffectMaterialKey key)
        {
            if (Materials.TryGetValue(key, out var materialRef))
            {
                ++materialRef.RefCount;
                return materialRef.Material;
            }

            var material = Cachehs.Count > 0 ? Cachehs.Pop() : null;
            material = key.UpdateMaterial(material);
            if (material == null)
            {
                return null;
            }

            Materials.Add(key, new MaterialRef(material));
            Lookup.Add(material, key);
            return material;
        }

        public static void Free(Material material)
        {
            if (!Lookup.TryGetValue(material, out var key))
            {
                return;
            }

            if (!Materials.TryGetValue(key, out var materialRef))
            {
                return;
            }

            if (--materialRef.RefCount > 0) return;
            if (Cachehs.Count <= MaterialCacheCount)
            {
                Cachehs.Push(material);
            }
            else
            {
#if UNITY_EDITOR
                if (Application.isPlaying)
                {
                    Object.Destroy(materialRef.Material);
                }
                else
                {
                    Object.DestroyImmediate(materialRef.Material);
                }
#else
                Object.Destroy(materialRef.Material);
#endif
            }

            Materials.Remove(key);
            Lookup.Remove(material);
        }
    }
}