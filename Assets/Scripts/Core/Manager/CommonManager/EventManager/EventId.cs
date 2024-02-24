// ******************************************************************
//       /\ /|       @file       EventId.cs
//       \ V/        @brief      事件id
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2022-03-29 11:36:18
//    *(__\_\        @Copyright  Copyright (c) 2022, Shadowrabbit
// ******************************************************************

using Sirenix.OdinInspector;

namespace Rabi
{
    public enum EventId
    {
        #region Input

        [LabelText("鼠标左键点击")] OnClickAny,
        [LabelText("鼠标左键长按")] OnHoldAny,

        #endregion

        #region UI

        [LabelText("logo完成事件")] OnLogoComplete,
        [LabelText("语言更变事件")] OnLanguageChanged,
        [LabelText("当前生命值刷新事件")] OnHpRefresh,
        [LabelText("当前攻击力刷新事件")] OnAtkRefresh,
        [LabelText("当前攻击顺序更新事件")] OnAttackOrderChanged,
        [LabelText("剧情对话刷新事件")] OnDialogueRefresh,
        [LabelText("eid可见性刷新")] OnEidVisitableChanged,
        [LabelText("单位入场时")] OnSpawn, //单位入场

        #endregion

        #region Plot

        [LabelText("剧情节点播放执行完毕回调")] OnPlotNodeComplete,
        [LabelText("剧情播放完毕回调")] OnPlotPlayComplete,

        #endregion
    }
}