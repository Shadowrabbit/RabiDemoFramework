// ******************************************************************
//       /\ /|       @file       PrucedureChangeScene.cs
//       \ V/        @brief      切换场景流程 Loading
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2022-03-27 01:31:01
//    *(__\_\        @Copyright  Copyright (c) 2022, Shadowrabbit
// ******************************************************************

using System.Threading.Tasks;
using UnityEngine;

namespace Rabi
{
    public class PrucedureChangeScene : BaseGamePrucedure
    {
        private const float MinLoadingTime = 2f; //最小加载时间

        public override async void OnEnter(params object[] args)
        {
            base.OnEnter(args);
            if (args.Length < 1)
            {
                Logger.Error($"场景切换参数数量缺失 需要levelId");
                return;
            }

            //要进入的关卡id
            if (args[0] is not string levelId)
            {
                Logger.Error($"levelId参数错误 期望:string 实际:{args[0].GetType()}");
                return;
            }

            // //存档
            // SaveManager.Instance.SavePlayerData();
            // AudioManager.Instance.OnClear();
            // UIManager.Instance.CloseAllWindows();
            // UIManager.Instance.OpenWindowSync("Loading");
            // //最低加载时间
            // var startTime = Time.realtimeSinceStartup;
            // //切换场景
            // await SceneManager.Instance.ChangeSceneAsync(ConfigManager.Instance.cfgLevel[levelId].scenePath);
            // //关卡场景的情况 需要预加载
            // await BattleUtil.Preload(ConfigManager.Instance.cfgLevel[levelId].preloadGroupId);
            // //清理bundle asset断链
            // AssetManager.ClearUnused();
            // var endTime = Time.realtimeSinceStartup;
            // var costTime = endTime - startTime;
            // //低于最小耗时 补一点时间
            // if (costTime < MinLoadingTime)
            // {
            //     await Task.Delay((int) (MinLoadingTime - costTime) * 1000);
            // }
            //
            // OnSceneLoadComplete(levelId);
        }

        /// <summary>
        /// 场景加载完成
        /// </summary>
        /// <param name="levelId"></param>
        private static void OnSceneLoadComplete(string levelId)
        {
            // Logger.Log("场景加载完成");
            // var levelType = ConfigManager.Instance.cfgLevel[levelId].levelType;
            // EventManager.Instance.DispatchLuaEvent(LuaEventDef.OnLoadingComplete);
            // if (levelType == DefLevelType.DSlgBattle)
            // {
            //     FsmManager.Instance.ChangeState(FsmDef.GamePrucedure, typeof(PrucedureBattleSlg), false, levelId);
            //     return;
            // }
            //
            // FsmManager.Instance.ChangeState(FsmDef.GamePrucedure, typeof(PrucedureMainMenu));
        }
    }
}