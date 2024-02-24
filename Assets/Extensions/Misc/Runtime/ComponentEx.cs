// ******************************************************************
//       /\ /|       @file       ComponentEx.cs
//       \ V/        @brief      组件扩展
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2022-05-18 10:04:27
//    *(__\_\        @Copyright  Copyright (c) 2022, Shadowrabbit
// ******************************************************************

using UnityEngine;

namespace Rabi
{
    public static class ComponentEx
    {
        public static T GetOrAddComponentDontSave<T>(this Component obj) where T : Component
        {
            var comp = obj.GetComponent<T>();
            if (comp == null)
            {
                comp = obj.GetComponentInChildren<T>();
            }

            if (comp != null)
            {
                return comp;
            }

            comp = obj.gameObject.AddComponent<T>();
            comp.hideFlags = HideFlags.DontSave;
            return comp;
        }
    }
}