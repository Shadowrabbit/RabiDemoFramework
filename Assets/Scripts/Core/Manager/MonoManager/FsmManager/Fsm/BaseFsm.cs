// ******************************************************************
//       /\ /|       @file       BaseFsm
//       \ V/        @brief      基础状态机
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2022-05-25 20:50
//    *(__\_\        @Copyright  Copyright (c) 2022, Shadowrabbit
// ******************************************************************

using System;
using System.Collections.Generic;

namespace Rabi
{
    public abstract class BaseFsm
    {
        protected bool isPause; //暂停

        protected readonly Dictionary<Type, IFsmState>
            stateMap = new Dictionary<Type, IFsmState>(); //当前储存的全部状态key：类型v：状态

        public string name; //状态机名称
        public IFsmState previousState; //上一个状态
        public IFsmState currentState; //当前状态

        protected BaseFsm()
        {
        }

        /// <summary>
        /// 有参构造，传递一组想要的状态创建一个状态机
        /// </summary>
        /// <param name="name">状态机名称</param>
        /// <param name="states">状态列表</param>
        protected BaseFsm(string name, IEnumerable<IFsmState> states)
        {
            this.name = name;
            foreach (var state in states)
            {
                stateMap.Add(state.GetType(), state);
            }
        }

        /// <summary>
        /// 切换状态
        /// </summary>
        /// <param name="fsmType">要进入的状态</param>
        /// <param name="canRepeat">是否是允许重复进入的状态</param>
        /// <param name="args">进入状态时所需的参数</param>
        public virtual void ChangeState(Type fsmType, bool canRepeat = false, params object[] args)
        {
            if (!stateMap.ContainsKey(fsmType))
            {
                Logger.Error($"当前状态机不存在状态name:{name} type:{fsmType}");
                return;
            }

            if (isPause)
            {
                return;
            }

            //不允许重复进入
            if (!canRepeat && currentState == stateMap[fsmType])
            {
                return;
            }

            currentState?.OnExit();
            previousState = currentState;
            currentState = stateMap[fsmType];
            currentState.OnEnter(args);
        }

        /// <summary>
        /// 切换状态
        /// </summary>
        /// <param name="canRepeat">是否是允许重复进入的状态</param>
        /// <param name="args">进入状态时所需的参数</param>
        public virtual void ChangeState<T>(bool canRepeat = false, params object[] args) where T : IFsmState
        {
            if (!stateMap.ContainsKey(typeof(T)))
            {
                Logger.Error($"当前状态机不存在状态name:{name} type:{typeof(T)}");
                return;
            }

            if (isPause)
            {
                return;
            }

            //不允许重复进入
            if (!canRepeat && currentState == stateMap[typeof(T)])
            {
                return;
            }

            currentState?.OnExit();
            previousState = currentState;
            currentState = stateMap[typeof(T)];
            currentState.OnEnter(args);
        }

        /// <summary>
        /// 重置状态机
        /// </summary>
        /// <param name="states"></param>
        public void ResetStateList(IEnumerable<IFsmState> states)
        {
            stateMap.Clear();
            foreach (var state in states)
            {
                stateMap.Add(state.GetType(), state);
            }

            currentState = null;
        }

        public void OnUpdate()
        {
            if (isPause)
            {
                return;
            }

            currentState?.OnUpdate();
        }

        public void OnLateUpdate()
        {
            if (isPause)
            {
                return;
            }

            currentState?.OnLateUpdate();
        }

        public void OnFixedUpdate()
        {
            if (isPause)
            {
                return;
            }

            currentState?.OnFixedUpdate();
        }

        /// <summary>
        /// 暂停
        /// </summary>
        public void Pause()
        {
            isPause = true;
            currentState?.OnPause();
        }

        /// <summary>
        /// 恢复
        /// </summary>
        public void Resume()
        {
            isPause = false;
            currentState?.OnResume();
        }

        /// <summary>
        /// 清空状态
        /// </summary>
        public virtual void Clear()
        {
            currentState = null;
            stateMap.Clear();
        }

        /// <summary>
        /// 终止状态机
        /// </summary>
        public virtual void Stop()
        {
            currentState?.OnExit();
            currentState = null;
        }

        /// <summary>
        /// 当前是否处于某个状态
        /// </summary>
        /// <param name="fsmType"></param>
        /// <returns></returns>
        public bool IsInState(Type fsmType)
        {
            return currentState?.GetType() == fsmType;
        }

        /// <summary>
        /// 当前是否处于某个状态
        /// </summary>
        /// <returns></returns>
        public bool IsInState<T>() where T : IFsmState
        {
            return currentState?.GetType() == typeof(T);
        }
    }
}