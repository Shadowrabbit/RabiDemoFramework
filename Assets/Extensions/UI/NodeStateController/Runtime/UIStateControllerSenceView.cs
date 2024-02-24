// ******************************************************************
//       /\ /|       @file       UIStateControllerSceneView.cs
//       \ V/        @brief      UI状态控制器 场景绘制
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2022-05-17 08:37:02
//    *(__\_\        @Copyright  Copyright (c) 2022, Shadowrabbit
// ******************************************************************

using UnityEditor;
using UnityEngine;

namespace Rabi
{
    public static class UIStateControllerSceneView
    {
#if UNITY_EDITOR
        /// <summary>
        /// UI状态控制器绘制
        /// </summary>
        /// <param name="uiStateController"></param>
        /// <param name="view"></param>
        public static void DrawUIStateController(UIStateController uiStateController, SceneView view)
        {
            if (uiStateController == null)
            {
                return;
            }

            if (Application.isPlaying)
            {
                return;
            }

            Handles.BeginGUI();
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal(GUILayout.MaxWidth(view.size / 3));
            if (GUILayout.Button(uiStateController.ToString()))
            {
                Selection.activeObject = uiStateController;
            }

            if (uiStateController.stateNameList != null && uiStateController.stateNameList.Count > 0)
            {
                for (var index = 0; index < uiStateController.stateNameList.Count; index++)
                {
                    var stateName = uiStateController.stateNameList[index];
                    if (!GUILayout.Button(stateName)) continue;
                    uiStateController.SetSelectedState(index);
                    EditorUtility.SetDirty(uiStateController);
                }
            }

            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
            Handles.EndGUI();
        }
#endif
    }
}