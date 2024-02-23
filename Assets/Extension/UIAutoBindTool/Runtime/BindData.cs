// ******************************************************************
//       /\ /|       @file       BindData
//       \ V/        @brief      绑定数据
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2024-02-23 18:07
//    *(__\_\        @Copyright  Copyright (c) 2024, Shadowrabbit
// ******************************************************************

using System;
using UnityEngine;

namespace Rabi
{
    [Serializable]
    public class BindData
    {
        public string name;
        public Component comp;

        public BindData()
        {
        }

        public BindData(string name, Component bindCom)
        {
            this.name = name;
            comp = bindCom;
        }
    }
}