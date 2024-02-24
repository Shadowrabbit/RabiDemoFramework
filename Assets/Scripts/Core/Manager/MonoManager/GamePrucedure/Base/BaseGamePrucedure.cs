// ******************************************************************
//       /\ /|       @file       BaseGamePrucedure.cs
//       \ V/        @brief      游戏流程基类
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2022-03-27 01:22:14
//    *(__\_\        @Copyright  Copyright (c) 2022, Shadowrabbit
// ******************************************************************

namespace Rabi
{
    public abstract class BaseGamePrucedure : IFsmState
    {
        public virtual void OnEnter(params object[] args)
        {
            Logger.Log($"Enter GamePrucedure:{GetType()}");
        }

        public virtual void OnExit()
        {
            Logger.Log($"Exit GamePrucedure:{GetType()}");
        }

        public void OnAfterChange()
        {
        }

        public virtual void OnUpdate()
        {
        }

        public virtual void OnLateUpdate()
        {
        }

        public virtual void OnFixedUpdate()
        {
        }

        public virtual void OnPause()
        {
        }

        public virtual void OnResume()
        {
        }
    }
}