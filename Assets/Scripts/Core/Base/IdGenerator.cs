// ******************************************************************
//       /\ /|       @file       IdGenerator
//       \ V/        @brief      ID生成器
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2023-10-05 15:36
//    *(__\_\        @Copyright  Copyright (c) 2023, Shadowrabbit
// ******************************************************************

namespace Rabi
{
    public class IdGenerator
    {
        private static int _eid;

        /// <summary>
        /// 获取eid
        /// </summary>
        /// <returns></returns>
        public static int GetEid()
        {
            return --_eid;
        }
    }
}