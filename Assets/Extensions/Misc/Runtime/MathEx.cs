// ******************************************************************
//       /\ /|       @file       MathEx
//       \ V/        @brief      
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2023-11-13 23:11
//    *(__\_\        @Copyright  Copyright (c) 2023, Shadowrabbit
// ******************************************************************

namespace Rabi
{
    public static class MathEx
    {
        /// <summary>
        /// 判断某概率事件，本次是否发生
        /// </summary>
        /// <param name="percent"></param>
        /// <returns></returns>
        public static bool Percent(int percent)
        {
            return UnityEngine.Random.Range(0, 100) < percent;
        }
    }
}