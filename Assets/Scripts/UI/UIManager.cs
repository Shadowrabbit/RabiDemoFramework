// ******************************************************************
//       /\ /|       @file       UIManager
//       \ V/        @brief      UI管理器
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2024-02-24 16:41
//    *(__\_\        @Copyright  Copyright (c) 2024, Shadowrabbit
// ******************************************************************

using System.Collections.Generic;
using UnityEngine;

namespace Rabi
{
    public class UIManager : BaseSingleTon<UIManager>
    {
        private readonly Dictionary<string, Transform> _windowName2Transform = new Dictionary<string, Transform>();

        public void Init()
        {
            _windowName2Transform.Add("MainWindow",
                GameManager.Instance.transform.Find("UI/UIRoot/NormalLayer/MainWindow"));
            _windowName2Transform.Add("ModeSelectWindow",
                GameManager.Instance.transform.Find("UI/UIRoot/NormalLayer/ModeSelectWindow"));
        }

        public void OpenWindow(string name)
        {
            if (!_windowName2Transform.ContainsKey(name))
            {
                Logger.Error($"找不到window:{name}");
                return;
            }

            _windowName2Transform[name].gameObject.SetActive(true);
        }

        public void CloseWindow(string name)
        {
            if (!_windowName2Transform.ContainsKey(name))
            {
                Logger.Error($"找不到window:{name}");
                return;
            }

            _windowName2Transform[name].gameObject.SetActive(false);
        }

        public void CloseAllWindow()
        {
            foreach (var t in _windowName2Transform.Values)
            {
                t.gameObject.SetActive(false);
            }
        }
    }
}