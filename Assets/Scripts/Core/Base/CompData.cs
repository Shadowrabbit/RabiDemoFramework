// ******************************************************************
//       /\ /|       @file       CompData.cs
//       \ V/        @brief      组件数据
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2022-04-02 10:39:03
//    *(__\_\        @Copyright  Copyright (c) 2022, Shadowrabbit
// ******************************************************************

namespace Rabi
{
    public abstract class CompData
    {
        /// <summary>
        /// 用于复用时还原组件数据
        /// </summary>
        public virtual void Reset()
        {
        }
    }
}