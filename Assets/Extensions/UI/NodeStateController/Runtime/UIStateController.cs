// ******************************************************************
//       /\ /|       @file       UIStateController.cs
//       \ V/        @brief      状态控制器
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2022-05-16 12:12:13
//    *(__\_\        @Copyright  Copyright (c) 2022, Shadowrabbit
// ******************************************************************

using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Rabi
{
    [ExecuteAlways]
    public class UIStateController : MonoBehaviour
    {
        [Header("状态名称定义")] [LabelText("状态名称")] public List<string> stateNameList; //状态名称
        [Header("当前控制器管理的记录节点")] public StateNodeRecorder[] stateNodeRecorderList; //当前控制器管理的记录节点
        [LabelText("当前状态")] public int currentStateIndex; //当前状态索引

        /// <summary>
        /// 设置状态
        /// </summary>
        /// <param name="uiState"></param>
        public void SetSelectedState(int uiState)
        {
            currentStateIndex = uiState;
            Refresh();
        }

        /// <summary>
        /// 刷新状态
        /// </summary>
        public void Refresh()
        {
            if (stateNodeRecorderList == null)
            {
                return;
            }

            foreach (var stateNodeRecorder in stateNodeRecorderList)
            {
                stateNodeRecorder.UpdateState(currentStateIndex);
            }
        }

        /// <summary>
        /// 更新记录器状态
        /// </summary>
        public void UpdateNamelist()
        {
            if (stateNodeRecorderList == null)
            {
                return;
            }

            foreach (var stateNodeRecorder in stateNodeRecorderList)
            {
                stateNodeRecorder.SetStateNameList(stateNameList);
            }
        }

        /// <summary>
        /// 在子节点中绑定记录器 编辑器用
        /// </summary>
        [UsedImplicitly]
        private void FindRecorderInChindren()
        {
            stateNodeRecorderList = GetComponentsInChildren<StateNodeRecorder>(true);
            Refresh();
        }

#if UNITY_EDITOR
        private void OnEnable()
        {
            SceneView.duringSceneGui += OnDuringSceneGui;
        }

        /// <summary>
        /// 绘制scene上的UI
        /// </summary>
        /// <param name="view"></param>
        private void OnDuringSceneGui(SceneView view)
        {
            try
            {
                if (this == null)
                {
                    SceneView.duringSceneGui -= OnDuringSceneGui;
                    return;
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.ToString());
                SceneView.duringSceneGui -= OnDuringSceneGui;
            }

            UIStateControllerSceneView.DrawUIStateController(this, view);
        }

        private void OnDisable()
        {
            SceneView.duringSceneGui -= OnDuringSceneGui;
        }

        private void OnValidate()
        {
            UpdateNamelist();
        }
#endif
    }
}