// ******************************************************************
//       /\ /|       @file       AutoBindTest
//       \ V/        @brief      
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \        @Modified   2024/2/22 23:57:21
//    *(__\_\        @Copyright  Copyright (c) 2024, Shadowrabbit
// ******************************************************************
using UnityEngine;
using UnityEngine.UI;

namespace Rabi
{
	public partial class AutoBindTest
	{
		private Image _imgTest1;
		private Button _btnTest2;
		private Text _txtTest3;

		private void GetBindComponents(GameObject go)
		{
			var autoBindTool = go.GetComponent<ComponentAutoBindTool>();
			_imgTest1 = autoBindTool.GetBindComponent<Image>(0);
			_btnTest2 = autoBindTool.GetBindComponent<Button>(1);
			_txtTest3 = autoBindTool.GetBindComponent<Text>(2);
		}
	}
}
