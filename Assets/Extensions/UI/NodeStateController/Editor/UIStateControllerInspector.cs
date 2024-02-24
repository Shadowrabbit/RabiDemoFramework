// ******************************************************************
//       /\ /|       @file       UIStateControllerInspector.cs
//       \ V/        @brief      UI状态控制器窗口
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2022-05-17 01:45:04
//    *(__\_\        @Copyright  Copyright (c) 2022, Shadowrabbit
// ******************************************************************

using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Rabi
{
    [CustomEditor(typeof(UIStateController))]
    public class UIStateControllerInspector : Editor
    {
        private MethodInfo _findRecorderInChildren; //自动绑定全部子节点的记录器
        private SerializedProperty _stateNodeRecorderList; //记录器列表
        private SerializedProperty _stateNameList; //状态名称列表

        private void OnEnable()
        {
            _findRecorderInChildren = target.GetType()
                .GetMethod("FindRecorderInChindren", BindingFlags.Instance | BindingFlags.NonPublic);
            _stateNameList = serializedObject.FindProperty("stateNameList");
            _stateNodeRecorderList = serializedObject.FindProperty("stateNodeRecorderList");
        }

        public override void OnInspectorGUI()
        {
            var uiStateController = (UIStateController) target;
            if (uiStateController == null)
            {
                return;
            }

            serializedObject.Update();
            EditorGUILayout.BeginVertical();
            EditorGUILayout.PropertyField(_stateNameList);
            EditorGUILayout.PropertyField(_stateNodeRecorderList);
            if (GUILayout.Button("自动绑定所有子节点记录器"))
            {
                _findRecorderInChildren?.Invoke(target, null);
                uiStateController.UpdateNamelist();
                EditorUtility.SetDirty(target);
            }

            var tempIndex = uiStateController.currentStateIndex;
            EditorGUILayout.LabelField("当前状态");
            if (uiStateController.stateNameList != null && uiStateController.stateNameList.Count > 0)
            {
                var tempStateIndex = uiStateController.currentStateIndex;
                uiStateController.currentStateIndex =
                    EditorGUILayout.Popup(uiStateController.currentStateIndex,
                        uiStateController.stateNameList.ToArray());
                if (tempStateIndex != uiStateController.currentStateIndex)
                {
                    EditorUtility.SetDirty(target);
                }
            }

            //值变了 刷新
            if (tempIndex != uiStateController.currentStateIndex)
            {
                uiStateController.Refresh();
            }

            EditorGUILayout.EndVertical();
            serializedObject.ApplyModifiedProperties();
        }
    }
}