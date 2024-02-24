// ******************************************************************
//       /\ /|       @file       DialogScreenPositionFixPreviewEditor.cs
//       \ V/        @brief      DialogScreenPositionFix预览编辑器
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2021-01-12 16:17:53
//    *(__\_\        @Copyright  Copyright (c) 2021, Shadowrabbit
// ******************************************************************

using System.Reflection;
using UnityEditor;
using UnityEngine;
namespace Rabi
{
    [CustomEditor(typeof(DialogScreenPositionFixPreview))]
    public class DialogScreenPositionFixPreviewEditor : Editor
    {
        private MethodInfo _fix; //修正方法
        private MethodInfo _fixPositionTopMiddle; //修正方法
    
        private void OnEnable()
        {
            _fix = target.GetType().GetMethod("FixPosition", BindingFlags.Instance | BindingFlags.NonPublic);
            _fixPositionTopMiddle =
                target.GetType().GetMethod("FixPositionType2", BindingFlags.Instance | BindingFlags.NonPublic);
        }
    
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();
            GUILayout.Label("*****预览*****");
    
            if (GUILayout.Button("X先左后右 Y居中"))
            {
                _fix?.Invoke(target, null);
                EditorUtility.SetDirty(target);
            }
    
            if (GUILayout.Button("X居中 Y居上"))
            {
                _fixPositionTopMiddle?.Invoke(target, null);
                EditorUtility.SetDirty(target);
            }
    
            serializedObject.ApplyModifiedProperties();
        }
    }
}
