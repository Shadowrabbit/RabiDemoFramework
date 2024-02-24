// ******************************************************************
//       /\ /|       @file       MaterialRef.cs
//       \ V/        @brief      
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2022-05-17 11:51:08
//    *(__\_\        @Copyright  Copyright (c) 2022, Shadowrabbit
// ******************************************************************

using UnityEngine;

namespace Rabi
{
    public sealed class MaterialRef
    {
        public Material Material { get; }

        public int RefCount { get; set; }

        public MaterialRef(Material material)
        {
            Material = material;
            RefCount = 1;
        }
    }
}