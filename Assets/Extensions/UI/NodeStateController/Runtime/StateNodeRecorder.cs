// ******************************************************************
//       /\ /|       @file       StateNodeRecorder.cs
//       \ V/        @brief      状态节点记录器
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2022-05-15 11:53:04
//    *(__\_\        @Copyright  Copyright (c) 2022, Shadowrabbit
// ******************************************************************

using System.Collections.Generic;
using UnityEngine;

namespace Rabi
{
    public class StateNodeRecorder : MonoBehaviour
    {
        public List<CombinationStateNode> nodeList = new List<CombinationStateNode>(); //当前节点在对应N个状态 对N个组件的操作
        private bool _hasInit; //已经完成初始化
        [HideInInspector] public List<string> stateNameList; //状态名称

        /// <summary>
        /// 设置持有者
        /// </summary>
        /// <param name="stateList"></param>
        public void SetStateNameList(List<string> stateList)
        {
            stateNameList = stateList;
        }

        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="state"></param>
        public void UpdateState(int state)
        {
            TryInit();
            foreach (var stateNode in nodeList)
            {
                stateNode.OnStateChanged(state);
            }
        }

        private void TryInit()
        {
            if (_hasInit)
            {
                return;
            }

            Init();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            foreach (var stateNode in nodeList)
            {
                stateNode.OnInit(gameObject);
            }

            _hasInit = true;
        }

        /// <summary>
        /// 编辑器值改变回调
        /// </summary>
        private void OnValidate()
        {
            _hasInit = false;
        }
    }
}