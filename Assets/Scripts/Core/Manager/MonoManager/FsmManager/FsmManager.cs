// ******************************************************************
//       /\ /|       @file       FsmManager.cs
//       \ V/        @brief      状态机管理器
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2022-03-24 11:51:41
//    *(__\_\        @Copyright  Copyright (c) 2022, Shadowrabbit
// ******************************************************************

using System;
using System.Collections.Generic;

namespace Rabi
{
    public sealed class FsmManager : BaseSingleTon<FsmManager>, IMonoManager
    {
        private readonly Dictionary<string, BaseFsm> _fsmDict = new Dictionary<string, BaseFsm>(); //key：状态机名字 v：状态机

        public void OnInit()
        {
            Logger.Log("状态机管理器初始化");
        }

        public void Update()
        {
            foreach (var fsm in _fsmDict.Values)
            {
                fsm.OnUpdate();
            }
        }

        public void FixedUpdate()
        {
            foreach (var fsm in _fsmDict.Values)
            {
                fsm.OnFixedUpdate();
            }
        }

        public void LateUpdate()
        {
            foreach (var fsm in _fsmDict.Values)
            {
                fsm.OnLateUpdate();
            }
        }

        public void OnClear()
        {
            _fsmDict.Clear();
        }

        /// <summary>
        /// 获取状态机
        /// </summary>
        /// <param name="name">状态机名字</param>
        /// <typeparam name="T">状态机类</typeparam>
        /// <returns></returns>
        public T GetFsm<T>(string name) where T : BaseFsm, new()
        {
            if (_fsmDict.ContainsKey(name))
            {
                return _fsmDict[name] as T;
            }

            var fsm = new T { name = name };
            _fsmDict.Add(name, fsm);
            return fsm;
        }

        /// <summary>
        /// 重置状态机
        /// </summary>
        /// <param name="name"></param>
        /// <param name="states"></param>
        public void ResetStateList(string name, IEnumerable<IFsmState> states)
        {
            if (!_fsmDict.ContainsKey(name))
            {
                Logger.Error($"状态机不存在 name:{name}");
                return;
            }

            _fsmDict[name].ResetStateList(states);
        }

        /// <summary>
        /// 切换状态
        /// </summary>
        /// <param name="name">状态机的名字</param>
        /// <param name="fsmType">要进入的状态</param>
        /// <param name="canRepeatEntry">是否是允许重复进入的状态</param>
        /// <param name="args">进入状态时所需的参数</param>
        public void ChangeState(string name, Type fsmType, bool canRepeatEntry = false, params object[] args)
        {
            if (!_fsmDict.ContainsKey(name))
            {
                Logger.Error($"状态机不存在 name:{name}");
                return;
            }

            _fsmDict[name].ChangeState(fsmType, canRepeatEntry, args);
        }

        /// <summary>
        /// 切换状态
        /// </summary>
        /// <param name="name"></param>
        /// <param name="canRepeatEntry">是否是允许重复进入的状态</param>
        /// <param name="args">进入状态时所需的参数</param>
        public void ChangeState<T>(string name, bool canRepeatEntry = false, params object[] args)
            where T : IFsmState
        {
            if (!_fsmDict.ContainsKey(name))
            {
                Logger.Error($"状态机不存在 name:{name}");
                return;
            }

            _fsmDict[name].ChangeState<T>(canRepeatEntry, args);
        }

        /// <summary>
        /// 销毁状态机
        /// </summary>
        /// <param name="name"></param>
        public void DestroyFsm(string name)
        {
            if (!_fsmDict.ContainsKey(name))
            {
                return;
            }

            _fsmDict[name].Clear();
            _fsmDict.Remove(name);
        }
    }
}