// ******************************************************************
//       /\ /|       @file       StateNodeRecorderInspector.cs
//       \ V/        @brief      状态节点记录器 窗口
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2022-05-16 03:32:25
//    *(__\_\        @Copyright  Copyright (c) 2022, Shadowrabbit
// ******************************************************************

using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Rabi
{
    [CustomEditor(typeof(StateNodeRecorder))]
    public class StateNodeRecorderInspector : Editor
    {
        [InspectorName("节点类型")] private StateNodeType _stateNodeType = StateNodeType.WaitSelect;
        private ReorderableList _reorderableList; //节点记录器绘制组件
        private SerializedProperty _nodeList; //控制节点列表

        //节点类型对描述映射
        private static readonly Dictionary<int, GUIContent> Type2desc = new Dictionary<int, GUIContent>()
        {
            {StateNodeType.WaitSelect.GetHashCode(), new GUIContent("等待选择")},
            {StateNodeType.Active.GetHashCode(), new GUIContent("激活控制")},
            {StateNodeType.Color.GetHashCode(), new GUIContent("颜色控制")},
            {StateNodeType.MeshProColor.GetHashCode(), new GUIContent("文本颜色控制")},
            {StateNodeType.Gray.GetHashCode(), new GUIContent("灰度控制")},
            {StateNodeType.Sprite.GetHashCode(), new GUIContent("sp图片控制")},
            {StateNodeType.Texture.GetHashCode(), new GUIContent("tex图片控制")},
            {StateNodeType.Text.GetHashCode(), new GUIContent("文本控制")},
        };

        private void OnEnable()
        {
            _nodeList = serializedObject.FindProperty("nodeList");
            _reorderableList = new ReorderableList(serializedObject, _nodeList)
            {
                drawHeaderCallback = OnDrawHeaderCallback,
                drawElementCallback = OnDrawElementCallback,
                onAddCallback = OnAddCallBack,
                elementHeightCallback = OnElementHeightCallback
            };
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.BeginVertical();
            _stateNodeType = (StateNodeType) EditorGUILayout.EnumPopup("选择要添加的控制类型", _stateNodeType);
            _reorderableList?.DoLayoutList();
            EditorGUILayout.EndVertical();
            serializedObject.ApplyModifiedProperties();
        }

        /// <summary>
        /// 元素高度计算回调
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private float OnElementHeightCallback(int index)
        {
            var element = _nodeList.GetArrayElementAtIndex(index);
            var stateNodeType = (StateNodeType) element.FindPropertyRelative("stateNodeType").enumValueIndex;
            switch (stateNodeType)
            {
                case StateNodeType.WaitSelect:
                    return EditorGUIUtility.singleLineHeight + 4f;
                case StateNodeType.Active:
                case StateNodeType.Color:
                case StateNodeType.Gray:
                default:
                    return EditorGUIUtility.singleLineHeight * 3 + 4f;
            }
        }

        /// <summary>
        /// 添加元素回调
        /// </summary>
        /// <param name="list"></param>
        private void OnAddCallBack(ReorderableList list)
        {
            if (!(target is StateNodeRecorder stateNodeRecorder))
            {
                return;
            }

            if (_stateNodeType == StateNodeType.WaitSelect)
            {
                Debug.LogError("没有选择添加的节点类型");
                return;
            }

            var stateNode = new CombinationStateNode(_stateNodeType);
            stateNodeRecorder.nodeList.Add(stateNode);
            stateNode.OnInit(stateNodeRecorder.gameObject);
        }

        /// <summary>
        /// 元素绘制回调
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="index"></param>
        /// <param name="isactive"></param>
        /// <param name="isfocused"></param>
        private void OnDrawElementCallback(Rect rect, int index, bool isactive, bool isfocused)
        {
            var element = _nodeList.GetArrayElementAtIndex(index); //当前元素
            var stateNodeType =
                (StateNodeType) element.FindPropertyRelative("stateNodeType").enumValueIndex; //当前元素的节点类型
            rect.height = EditorGUIUtility.singleLineHeight;
            EditorGUI.LabelField(rect, Type2desc[stateNodeType.GetHashCode()]);
            rect.y += EditorGUIUtility.singleLineHeight;
            var stateNodeRecorder = (StateNodeRecorder) target;
            //强转失败||越界
            if (stateNodeRecorder == null || stateNodeRecorder.nodeList == null ||
                stateNodeRecorder.nodeList.Count <= index)
            {
                return;
            }

            //绘制当前类型相关的值编辑
            var combinationStateNode = stateNodeRecorder.nodeList[index];
            switch (stateNodeType)
            {
                case StateNodeType.WaitSelect:
                    break;
                case StateNodeType.Active:
                    DrawStateNodeActive(rect, combinationStateNode.activeStateNode, stateNodeRecorder.stateNameList);
                    break;
                case StateNodeType.Color:
                    DrawStateNodeColor(rect, combinationStateNode.colorStateNode, stateNodeRecorder.stateNameList);
                    break;
                case StateNodeType.MeshProColor:
                    DrawStateNodeMeshProColor(rect, combinationStateNode.meshProColorStateNode,
                        stateNodeRecorder.stateNameList);
                    break;
                case StateNodeType.Gray:
                    DrawStateNodeGray(rect, combinationStateNode.grayStateNode, stateNodeRecorder.stateNameList);
                    break;
                case StateNodeType.Sprite:
                    DrawStateNodeSprite(rect, combinationStateNode.spriteStateNode, stateNodeRecorder.stateNameList);
                    break;
                case StateNodeType.Texture:
                    DrawStateNodeTexture(rect, combinationStateNode.textureStateNode, stateNodeRecorder.stateNameList);
                    break;
                case StateNodeType.Text:
                    DrawStateNodeText(rect, combinationStateNode.textStateNode, stateNodeRecorder.stateNameList);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// 颜色控制节点绘制
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="colorStateNode"></param>
        /// <param name="nameStateList"></param>
        private static void DrawStateNodeColor(Rect rect, ColorStateNode colorStateNode, List<string> nameStateList)
        {
            if (nameStateList == null)
            {
                return;
            }

            colorStateNode.stateValue =
                EditorGUI.ColorField(rect, "目标颜色", colorStateNode.stateValue);
            rect.y += EditorGUIUtility.singleLineHeight;
            colorStateNode.triggerState =
                EditorGUI.Popup(rect, "触发状态", colorStateNode.triggerState, nameStateList.ToArray());
        }

        /// <summary>
        /// 文本控制节点item绘制
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="meshProColorStateNode"></param>
        /// <param name="nameStateList"></param>
        private static void DrawStateNodeMeshProColor(Rect rect, MeshProColorStateNode meshProColorStateNode,
            List<string> nameStateList)
        {
            if (nameStateList == null)
            {
                return;
            }

            meshProColorStateNode.stateValue =
                EditorGUI.ColorField(rect, "目标颜色", meshProColorStateNode.stateValue);
            rect.y += EditorGUIUtility.singleLineHeight;
            meshProColorStateNode.triggerState =
                EditorGUI.Popup(rect, "触发状态", meshProColorStateNode.triggerState, nameStateList.ToArray());
        }

        
        /// <summary>
        /// 灰度控制节点绘制
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="grayStateNode"></param>
        /// <param name="nameStateList"></param>
        private static void DrawStateNodeGray(Rect rect, GrayStateNode grayStateNode, List<string> nameStateList)
        {
            if (nameStateList == null)
            {
                return;
            }

            grayStateNode.stateValue =
                EditorGUI.IntSlider(rect, "目标灰度", grayStateNode.stateValue, 0, 255);
            rect.y += EditorGUIUtility.singleLineHeight;
            grayStateNode.triggerState =
                EditorGUI.Popup(rect, "触发状态", grayStateNode.triggerState, nameStateList.ToArray());
        }

        /// <summary>
        /// 激活控制节点item绘制
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="activeStateNode"></param>
        /// <param name="nameStateList"></param>
        private static void DrawStateNodeActive(Rect rect, ActiveStateNode activeStateNode, List<string> nameStateList)
        {
            if (nameStateList == null)
            {
                return;
            }

            activeStateNode.stateValue =
                EditorGUI.Toggle(rect, "是否激活", activeStateNode.stateValue);
            rect.y += EditorGUIUtility.singleLineHeight;
            activeStateNode.triggerState =
                EditorGUI.Popup(rect, "触发状态", activeStateNode.triggerState, nameStateList.ToArray());
        }

        /// <summary>
        /// 图片控制节点item绘制
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="spriteStateNode"></param>
        /// <param name="nameStateList"></param>
        private static void DrawStateNodeSprite(Rect rect, SpriteStateNode spriteStateNode, List<string> nameStateList)
        {
            if (nameStateList == null)
            {
                return;
            }

            spriteStateNode.stateValue =
                (Sprite) EditorGUI.ObjectField(rect, spriteStateNode.stateValue, typeof(Sprite), true);
            rect.y += EditorGUIUtility.singleLineHeight;
            spriteStateNode.triggerState =
                EditorGUI.Popup(rect, "触发状态", spriteStateNode.triggerState, nameStateList.ToArray());
        }

        /// <summary>
        /// 图片控制节点item绘制
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="textureStateNode"></param>
        /// <param name="nameStateList"></param>
        private static void DrawStateNodeTexture(Rect rect, TextureStateNode textureStateNode,
            List<string> nameStateList)
        {
            if (nameStateList == null)
            {
                return;
            }

            textureStateNode.stateValue =
                (Texture) EditorGUI.ObjectField(rect, textureStateNode.stateValue, typeof(Texture), true);
            rect.y += EditorGUIUtility.singleLineHeight;
            textureStateNode.triggerState =
                EditorGUI.Popup(rect, "触发状态", textureStateNode.triggerState, nameStateList.ToArray());
        }

        /// <summary>
        /// 文本内容控制节点item绘制
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="textStateNode"></param>
        /// <param name="nameStateList"></param>
        private static void DrawStateNodeText(Rect rect, TextStateNode textStateNode,
            List<string> nameStateList)
        {
            if (nameStateList == null)
            {
                return;
            }

            textStateNode.stateValue = EditorGUI.TextField(rect, textStateNode.stateValue);
            rect.y += EditorGUIUtility.singleLineHeight;
            textStateNode.triggerState =
                EditorGUI.Popup(rect, "触发状态", textStateNode.triggerState, nameStateList.ToArray());
        }

        /// <summary>
        /// 绘制标题回调
        /// </summary>
        /// <param name="rect"></param>
        private static void OnDrawHeaderCallback(Rect rect)
        {
            GUI.Label(rect, "状态控制列表");
        }
    }
}