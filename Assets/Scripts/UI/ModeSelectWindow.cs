// ******************************************************************
//       /\ /|       @file       ModeSelectWindow
//       \ V/        @brief      
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2024-02-24 19:28
//    *(__\_\        @Copyright  Copyright (c) 2024, Shadowrabbit
// ******************************************************************

using UnityEditor.MPE;
using UnityEngine;

namespace Rabi
{
    public partial class ModeSelectWindow : MonoBehaviour
    {
        private void Awake()
        {
            GetBindComponents(gameObject);
        }

        private void OnEnable()
        {
            _btnAI.onClick.AddListener(OnClickAI);
            _btnPieces.onClick.AddListener(OnClickPieces);
            _btnDark.onClick.AddListener(OnClickDark);
            _btnBack.onClick.AddListener(OnClickBack);
        }

        private void OnDisable()
        {
            _btnAI.onClick.RemoveListener(OnClickAI);
            _btnPieces.onClick.RemoveListener(OnClickPieces);
            _btnDark.onClick.RemoveListener(OnClickDark);
            _btnBack.onClick.RemoveListener(OnClickBack);
        }

        private static void OnClickAI()
        {
            UIManager.Instance.CloseAllWindow();
            var fsm = FsmManager.Instance.GetFsm<GamePrucedureController>(FsmDef.GamePrucedure);
            fsm.ChangeState<PrucedureLevel>(false, "虚假的关卡id");
        }

        private static void OnClickPieces()
        {
        }

        private static void OnClickDark()
        {
        }

        private static void OnClickBack()
        {
            UIManager.Instance.CloseAllWindow();
            UIManager.Instance.OpenWindow("MainWindow");
        }
    }
}