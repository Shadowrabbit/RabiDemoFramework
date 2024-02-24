// ******************************************************************
//       /\ /|       @file       ConfigManager.cs
//       \ V/        @brief      配置表管理器(由python自动生成)
//       | "")       @author     Shadowrabbit, yue.wang04@mihoyo.com
//       /  |
//      /  \\        @Modified   2022-04-23 22:40:15
//    *(__\_\        @Copyright  Copyright (c)  2022, Shadowrabbit
// ******************************************************************

namespace Rabi
{
    public sealed class ConfigManager : BaseSingleTon<ConfigManager>
    {
        public readonly CfgAudio cfgAudio = new CfgAudio();
        public readonly CfgAudioLayer cfgAudioLayer = new CfgAudioLayer();
        public readonly CfgCamp cfgCamp = new CfgCamp();
        public readonly CfgGlobal cfgGlobal = new CfgGlobal();
        public readonly CfgLevel cfgLevel = new CfgLevel();
        public readonly CfgUnitType cfgUnitType = new CfgUnitType();

        public ConfigManager()
        {
            //初始场景有Text的情况 查找翻译文本需要加载资源 因为同为Awake回调 加载顺序可能优于AssetManager 故补充加载
            
            Init();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            cfgAudio.Load();
            cfgAudioLayer.Load();
            cfgCamp.Load();
            cfgGlobal.Load();
            cfgLevel.Load();
            cfgUnitType.Load();
        }
    }}