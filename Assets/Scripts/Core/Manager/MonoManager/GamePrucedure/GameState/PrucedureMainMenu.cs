// ******************************************************************
//       /\ /|       @file       PrucedureMainMenu.cs
//       \ V/        @brief      主菜单流程
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2022-03-26 03:05:08
//    *(__\_\        @Copyright  Copyright (c) 2022, Shadowrabbit
// ******************************************************************

namespace Rabi
{
    public class PrucedureMainMenu : BaseGamePrucedure
    {
        public override void OnEnter(params object[] args)
        {
            base.OnEnter(args);
            // UIManager.Instance.OpenWindowAsync("Main");
            // //主菜单背景音乐
            // AudioManager.Instance.PlayAudio(DefAudio.DTestMusic);
        }
    }
}