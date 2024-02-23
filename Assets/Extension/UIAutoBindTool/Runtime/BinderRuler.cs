// ******************************************************************
//       /\ /|       @file       BinderRuler
//       \ V/        @brief      绑定规则
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2024-02-23 18:56
//    *(__\_\        @Copyright  Copyright (c) 2024, Shadowrabbit
// ******************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Rabi
{
    public static class BinderRuler
    {
        /// <summary>
        /// 命名前缀与类型的映射
        /// </summary>
        private static readonly Dictionary<string, Type> PrefixesDict = new Dictionary<string, Type>()
        {
            {"_rectTrans", typeof(RectTransform)},
            {"_btn", typeof(Button)},
            {"_img", typeof(Image)},
            {"_rImg", typeof(RawImage)},
            {"_txt", typeof(Text)},
        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public static Type CalcNodeType(string nodeName)
        {
            if (nodeName == null)
            {
                Debug.LogError("找不到节点");
                return null;
            }

            foreach (var (prefix, type) in PrefixesDict)
            {
                if (nodeName.StartsWith(prefix))
                {
                    return type;
                }
            }

            return null;
        }
    }
}