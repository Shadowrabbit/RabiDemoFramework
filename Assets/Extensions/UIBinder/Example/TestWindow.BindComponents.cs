// ******************************************************************
//       /\ /|       @file       TestWindow
//       \ V/        @brief      
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \        @Modified   2024/2/23 19:54:59
//    *(__\_\        @Copyright  Copyright (c) 2024, Shadowrabbit
// ******************************************************************

using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rabi
{
    public partial class TestWindow
    {
        private Image _imgBg;
        private TextMeshProUGUI _mpContent;

        private void GetBindComponents(GameObject go)
        {
            var uiBinder = go.GetComponent<UIBinder>();
            _imgBg = uiBinder.GetBindComponent<Image>(0);
            _mpContent = uiBinder.GetBindComponent<TextMeshProUGUI>(1);
        }
    }
}