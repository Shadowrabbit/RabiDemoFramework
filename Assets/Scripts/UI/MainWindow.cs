// ******************************************************************
//       /\ /|       @file       MainWindow
//       \ V/        @brief      
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2024-02-24 18:49
//    *(__\_\        @Copyright  Copyright (c) 2024, Shadowrabbit
// ******************************************************************

using UnityEngine;

namespace Rabi
{
    public partial class MainWindow : MonoBehaviour
    {
        private void Awake()
        {
            GetBindComponents(gameObject);
        }

        private void OnEnable()
        {
            _btnInfo.onClick.AddListener(OnClickInfo);
            _btnPlay.onClick.AddListener(OnClickPlay);
            _btnSetting.onClick.AddListener(OnClickSetting);
        }

        private void OnDisable()
        {
            _btnInfo.onClick.RemoveListener(OnClickInfo);
            _btnPlay.onClick.RemoveListener(OnClickPlay);
            _btnSetting.onClick.RemoveListener(OnClickSetting);
        }

        private static void OnClickInfo()
        {
        }

        private static void OnClickPlay()
        {
            UIManager.Instance.CloseAllWindow();
            UIManager.Instance.OpenWindow("ModeSelectWindow");
        }

        private static void OnClickSetting()
        {
        }
    }
}