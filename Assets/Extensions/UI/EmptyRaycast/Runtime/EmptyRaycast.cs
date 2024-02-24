// ******************************************************************
//       /\ /|       @file       EmptyRaycast.cs
//       \ V/        @brief      空射线组件
//       | "")       @author     Shadowrabbit, yue.wang04@mihoyo.com
//       /  |                    
//      /  \\        @Modified   2022-03-21 20:34:45
//    *(__\_\        @Copyright  Copyright (c)  2022, Shadowrabbit
// ******************************************************************

using UnityEngine;
using UnityEngine.UI;

namespace Rabi
{
    [RequireComponent(typeof(CanvasRenderer))]
    public class EmptyRaycast : MaskableGraphic
    {
        public override void SetMaterialDirty()
        {
        }

        public override void SetVerticesDirty()
        {
        }

        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();
        }
    }
}