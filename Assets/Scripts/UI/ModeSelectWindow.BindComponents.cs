// ******************************************************************
//       /\ /|       @file       ModeSelectWindow
//       \ V/        @brief      auto generated by UIBinder
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \         @Modified   2024/2/24 20:04:59
//    *(__\_\        @Copyright  Copyright (c) 2024, Shadowrabbit
// ******************************************************************

using UnityEngine;
using UnityEngine.UI;

namespace Rabi
{
	public partial class ModeSelectWindow
	{
		private Button _btnAI;
		private Text _txtAI;
		private Button _btnPieces;
		private Text _txtPieces;
		private Button _btnDark;
		private Text _txtDark;
		private Button _btnBack;
		private Text _txtBack;

		private void GetBindComponents(GameObject go)
		{
			var uiBinder = go.GetComponent<UIBinder>();
			_btnAI = uiBinder.GetBindComponent<Button>(0);
			_txtAI = uiBinder.GetBindComponent<Text>(1);
			_btnPieces = uiBinder.GetBindComponent<Button>(2);
			_txtPieces = uiBinder.GetBindComponent<Text>(3);
			_btnDark = uiBinder.GetBindComponent<Button>(4);
			_txtDark = uiBinder.GetBindComponent<Text>(5);
			_btnBack = uiBinder.GetBindComponent<Button>(6);
			_txtBack = uiBinder.GetBindComponent<Text>(7);
		}
	}
}