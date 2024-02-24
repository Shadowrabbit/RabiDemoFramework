// ******************************************************************
//       /\ /|       @file       IFsmState
//       \ V/        @brief      状态接口
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2022-05-25 21:04
//    *(__\_\        @Copyright  Copyright (c) 2022, Shadowrabbit
// ******************************************************************

namespace Rabi
{
    /// <summary>
    /// 有限状态机状态接口
    /// </summary>
    public interface IFsmState
    {
        /// <summary>
        /// 进入状态
        /// </summary>
        /// <param name="args">切换状态时传入的参数</param>
        void OnEnter(params object[] args);

        /// <summary>
        /// 退出状态
        /// </summary>
        void OnExit();

        /// <summary>
        /// 此状态在Update里要执行的操作
        /// </summary>
        void OnUpdate();

        /// <summary>
        /// 此状态在LateUpdate里要执行的操作
        /// </summary>
        void OnLateUpdate();

        /// <summary>
        /// 此状态在FixedUpdate里要执行的操作
        /// </summary>
        void OnFixedUpdate();

        /// <summary>
        /// 暂停状态机更新
        /// </summary>
        void OnPause();

        /// <summary>
        /// 恢复状态机更新
        /// </summary>
        void OnResume();
    }
}