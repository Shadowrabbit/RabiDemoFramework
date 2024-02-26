// ******************************************************************
//       /\ /|       @file       PrucedureLevel
//       \ V/        @brief      
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2024-02-26 13:30
//    *(__\_\        @Copyright  Copyright (c) 2024, Shadowrabbit
// ******************************************************************

using System.Threading.Tasks;
using Rabi;
using UnityEngine.SceneManagement;

public class PrucedureLevel : BaseGamePrucedure
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

        var scenePath = "Battle1"; //todo level表里配置场景路径
        // //存档
        // SaveManager.Instance.SavePlayerData();
        // AudioManager.Instance.OnClear();
        // UIManager.Instance.CloseAllWindows();
        // UIManager.Instance.OpenWindowSync("Loading");
        // //最低加载时间
        // var startTime = Time.realtimeSinceStartup;
        // //切换场景
        SceneManager.LoadScene("Battle1");
        await Task.Delay(2000);
        //SceneManager.SetActiveScene(scene);
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
        OnSceneLoadComplete(levelId);
    }

    /// <summary>
    /// 场景加载完成
    /// </summary>
    /// <param name="levelId"></param>
    private static void OnSceneLoadComplete(string levelId)
    {
        Logger.Log("场景加载完成");
        BattleManager.Instance.Enter(1);
        //todo 战斗记得开战斗UI
    }
}