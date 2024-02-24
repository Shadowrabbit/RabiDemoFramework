// ******************************************************************
//       /\ /|       @file       PrucedureBattleSlg
//       \ V/        @brief      slg战斗流程
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2022-09-24 15:53
//    *(__\_\        @Copyright  Copyright (c) 2022, Shadowrabbit
// ******************************************************************

namespace Rabi
{
    public class PrucedureBattleSlg : BaseGamePrucedure
    {
        // /// <summary>
        // /// 进入战场
        // /// </summary>
        // /// <param name="args"></param>
        // public override void OnEnter(params object[] args)
        // {
        //     base.OnEnter(args);
        //     if (args.Length < 1)
        //     {
        //         Logger.Error($"场景切换参数数量缺失 需要levelId");
        //         return;
        //     }
        //
        //     //要进入的关卡id
        //     if (args[0] is not string levelId)
        //     {
        //         Logger.Error($"levelId参数错误 期望:string 实际:{args[0].GetType()}");
        //         return;
        //     }
        //
        //     //战场UI
        //     UIManager.Instance.OpenWindowSync("Battle");
        //     SlgBattleManager.Instance.OnEnter(levelId, ConfigManager.Instance.cfgLevel[levelId].levelType);
        // }
        //
        // /// <summary>
        // /// 离开战场
        // /// </summary>
        // public override void OnExit()
        // {
        //     //清理战场
        //     SlgBattleManager.Instance.OnExit();
        //     base.OnExit();
        // }
    }
}