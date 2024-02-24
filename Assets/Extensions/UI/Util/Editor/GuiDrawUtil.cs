// ******************************************************************
//       /\ /|       @file       GuiDrawUtil
//       \ V/        @brief      绘制工具
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2022-09-28 15:04
//    *(__\_\        @Copyright  Copyright (c) 2022, Shadowrabbit
// ******************************************************************

using UnityEditor;
using UnityEngine;

namespace Rabi
{
    public static class GuiDrawUtil
    {
        /// <summary>
        /// Helper function that draws a serialized property.
        /// </summary>
        public static SerializedProperty DrawProperty(string label, SerializedObject serializedObject, string property,
            bool padding = true, params GUILayoutOption[] options)
        {
            var sp = serializedObject.FindProperty(property);
            if (sp != null)
            {
                if (padding) EditorGUILayout.BeginHorizontal();
                if (sp.isArray && sp.type != "string") DrawArray(serializedObject, property, label ?? property);
                else if (label != null) EditorGUILayout.PropertyField(sp, new GUIContent(label), options);
                else EditorGUILayout.PropertyField(sp, options);
                if (!padding)
                {
                    return sp;
                }

                GUILayout.Space(18f);
                EditorGUILayout.EndHorizontal();
            }
            else
            {
                Debug.LogWarning("Unable to find property " + property);
            }

            return sp;
        }

        /// <summary>
        /// Helper function that draws a serialized property.
        /// </summary>
        public static void DrawProperty(string label, SerializedProperty sp, bool padding,
            params GUILayoutOption[] options)
        {
            if (sp == null)
            {
                return;
            }

            if (padding) EditorGUILayout.BeginHorizontal();
            if (label != null) EditorGUILayout.PropertyField(sp, new GUIContent(label), options);
            else EditorGUILayout.PropertyField(sp, options);
            if (!padding)
            {
                return;
            }

            GUILayout.Space(18f);
            EditorGUILayout.EndHorizontal();
        }

        /// <summary>
        /// Helper function that draws an array property.
        /// </summary>
        public static void DrawArray(this SerializedObject obj, string property, string title)
        {
            var sp = obj.FindProperty(property + ".Array.size");
            if (sp == null || !DrawHeader(title))
            {
                return;
            }

            BeginContents();
            var size = sp.intValue;
            var newSize = EditorGUILayout.IntField("Size", size);
            if (newSize != size) obj.FindProperty(property + ".Array.size").intValue = newSize;

            EditorGUI.indentLevel = 1;

            for (var i = 0; i < newSize; i++)
            {
                var p = obj.FindProperty($"{property}.Array.data[{i}]");
                if (p != null) EditorGUILayout.PropertyField(p);
            }

            EditorGUI.indentLevel = 0;
            EndContents();
        }

        /// <summary>
        /// Draw a distinctly different looking header label
        /// </summary>
        public static bool DrawHeader(string text)
        {
            return DrawHeader(text, text, false);
        }

        /// <summary>
        /// Draw a distinctly different looking header label
        /// </summary>
        public static bool DrawHeader(string text, string key, bool forceOn)
        {
            var state = EditorPrefs.GetBool(key, true);
            GUILayout.Space(3f);
            if (!forceOn && !state) GUI.backgroundColor = new Color(0.8f, 0.8f, 0.8f);
            GUILayout.BeginHorizontal();
            GUI.changed = false;
            text = "<b><size=11>" + text + "</size></b>";
            if (state) text = "\u25BC " + text;
            else text = "\u25BA " + text;
            if (!GUILayout.Toggle(true, text, "dragtab", GUILayout.MinWidth(20f))) state = !state;
            if (GUI.changed) EditorPrefs.SetBool(key, state);
            GUILayout.Space(2f);
            GUILayout.EndHorizontal();
            GUI.backgroundColor = Color.white;
            if (!forceOn && !state)
            {
                GUILayout.Space(3f);
            }

            return state;
        }

        /// <summary>
        /// Begin drawing the content area.
        /// </summary>
        public static void BeginContents()
        {
            GUILayout.BeginHorizontal();
            EditorGUILayout.BeginHorizontal("TextArea", GUILayout.MinHeight(10f));
            GUILayout.BeginVertical();
            GUILayout.Space(2f);
        }

        /// <summary>
        /// End drawing the content area.
        /// </summary>
        public static void EndContents()
        {
            GUILayout.Space(3f);
            GUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(3f);
            GUILayout.EndHorizontal();
            GUILayout.Space(3f);
        }
    }
}