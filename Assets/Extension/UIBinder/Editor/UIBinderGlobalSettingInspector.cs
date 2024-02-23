// ******************************************************************
//       /\ /|       @file       UIBinderGlobalSettingInspector
//       \ V/        @brief      
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2024-02-23 18:29
//    *(__\_\        @Copyright  Copyright (c) 2024, Shadowrabbit
// ******************************************************************

using UnityEditor;
using UnityEngine;

namespace Rabi
{
    [CustomEditor(typeof(UIBinderGlobalSetting))]
    public class UIBinderGlobalSettingInspector : Editor
    {
        private SerializedProperty _namespace;
        private SerializedProperty _codePath;

        private void OnEnable()
        {
            _namespace = serializedObject.FindProperty("namespace");
            _codePath = serializedObject.FindProperty("codePath");
        }

        public override void OnInspectorGUI()
        {
            _namespace.stringValue = EditorGUILayout.TextField(new GUIContent("默认命名空间"), _namespace.stringValue);
            EditorGUILayout.LabelField("默认代码保存路径：");
            EditorGUILayout.LabelField(_codePath.stringValue);
            if (GUILayout.Button("选择路径", GUILayout.Width(140f)))
            {
                _codePath.stringValue = EditorUtility.OpenFolderPanel("选择代码保存路径", Application.dataPath, "");
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}