// ******************************************************************
//       /\ /|       @file       UICompConfigData
//       \ V/        @brief      UI配置数据
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2022-10-21 11:43
//    *(__\_\        @Copyright  Copyright (c) 2022, Shadowrabbit
// ******************************************************************


using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Rabi
{
    [Serializable]
    public struct UICompConfigData
    {
        [LabelText("组件名称")] public string compName;
        [LabelText("预制体")] public GameObject protoTemplate;
    }
}