// ******************************************************************
//       /\ /|       @file       SaveManager.cs
//       \ V/        @brief      存档管理器
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2022-05-14 09:45:04
//    *(__\_\        @Copyright  Copyright (c) 2022, Shadowrabbit
// ******************************************************************

namespace Rabi
{
    public class SaveManager : BaseSingleTon<SaveManager>
    {
        public void SavePlayerData()
        {
            //SaveUtil.SaveByJson(StoryPlayerDataCenter.Instance);
        }

        public void LoadPlayerData()
        {
            // //存档检测 不存在的话自动创建个新的
            // SaveUtil.CheckDataFile<StoryPlayerDataCenter>(data =>
            // {
            //     //解锁默认英雄
            //     data.UnlockHero(DefHero.DYoumuL1);
            //     //设置默认英雄
            //     data.storyPlayerData.currentHeroId = DefHero.DYoumuL1;
            //     //默认卡组本就是0号
            // });
            // var storyPlayerDataCenter = SaveUtil.LoadFromJson<StoryPlayerDataCenter>();
            // StoryPlayerDataCenter.Instance.SetData(storyPlayerDataCenter);
        }

        /// <summary>
        /// 保存设置
        /// </summary>
        public void SaveSetting()
        {
            //SaveUtil.SaveByJson(SettingDataCenter.Instance);
        }

        /// <summary>
        /// 加载设置
        /// </summary>
        public void LoadSetting()
        {
            // SaveUtil.CheckDataFile<SettingDataCenter>();
            // var settingDataCenter = SaveUtil.LoadFromJson<SettingDataCenter>();
            // SettingDataCenter.Instance.SetData(settingDataCenter);
        }
    }
}