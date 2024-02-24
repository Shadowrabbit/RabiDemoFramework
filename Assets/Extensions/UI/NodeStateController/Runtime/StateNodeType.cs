// ******************************************************************
//       /\ /|       @file       StateNodeType.cs
//       \ V/        @brief      状态节点类型
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2022-05-16 03:26:12
//    *(__\_\        @Copyright  Copyright (c) 2022, Shadowrabbit
// ******************************************************************

using UnityEngine;

namespace Rabi
{
    public enum StateNodeType
    {
        [InspectorName("等待选择")] WaitSelect,
        [InspectorName("激活")] Active,
        [InspectorName("图片颜色")] Color,
        [InspectorName("文本颜色")] MeshProColor,
        [InspectorName("灰度")] Gray,
        [InspectorName("Sprite图片")] Sprite,
        [InspectorName("Texture图片")] Texture,
        [InspectorName("文本内容")] Text,
    }
}