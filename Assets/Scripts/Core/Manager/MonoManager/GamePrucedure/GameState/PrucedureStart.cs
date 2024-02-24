// ******************************************************************
//       /\ /|       @file       PrucedureStart.cs
//       \ V/        @brief      启动游戏流程
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2022-03-25 12:27:05
//    *(__\_\        @Copyright  Copyright (c) 2022, Shadowrabbit
// ******************************************************************

namespace Rabi
{
    public class PrucedureStart : BaseGamePrucedure
    {
        public override void OnEnter(params object[] args)
        {
            base.OnEnter(args);
            //EventManager.Instance.AddListener(EventId.OnLogoComplete, OnLogoComplete);
            //logo展示
            //UIManager.Instance.OpenWindowSync("Logo");
            OnLogoComplete();
        }

        public override void OnExit()
        {
            //EventManager.Instance.RemoveListener(EventId.OnLogoComplete, OnLogoComplete);
            base.OnExit();
        }

        /// <summary>
        /// lua层页面传来
        /// </summary>
        private static void OnLogoComplete()
        {
            //读取设置
            SaveManager.Instance.LoadSetting();
            EventManager.Instance.Dispatch(EventId.OnLanguageChanged);
            //游戏存档读取
            SaveManager.Instance.LoadPlayerData();
            //进入下一个状态
            FsmManager.Instance.ChangeState<PrucedureMainMenu>(FsmDef.GamePrucedure);
        }
    }
}