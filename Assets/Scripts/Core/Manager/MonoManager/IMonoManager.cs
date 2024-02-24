// ******************************************************************
//       /\ /|       @file       IMonoManager.cs
//       \ V/        @brief      拥有Mono生命周期的管理器接口
//       | "")       @author     Shadowrabbit, yue.wang04@mihoyo.com
//       /  |                    
//      /  \\        @Modified   2022-03-17 19:49:33
//    *(__\_\        @Copyright  Copyright (c)  2022, Shadowrabbit
// ******************************************************************

namespace Rabi
{
    public interface IMonoManager
    {
        void OnInit();
        void Update();
        void FixedUpdate();
        void LateUpdate();
        void OnClear();
    }
}