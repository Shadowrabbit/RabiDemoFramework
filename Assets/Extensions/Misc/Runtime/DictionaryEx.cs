// ******************************************************************
//       /\ /|       @file       DictionaryEx.cs
//       \ V/        @brief      字典扩展
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2022-05-03 05:43:12
//    *(__\_\        @Copyright  Copyright (c) 2022, Shadowrabbit
// ******************************************************************

using System.Collections.Generic;

namespace Rabi
{
    public static class DictionaryEx
    {
        /// <summary>
        /// 获取属性字典中某个属性的值
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int GetPropertyIntValue(this Dictionary<string, int> dict, string key)
        {
            if (dict == null)
            {
                return 0;
            }

            return !dict.ContainsKey(key) ? 0 : dict[key];
        }

        /// <summary>
        /// 获取属性字典中某个属性的值
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetPropertyValue(this Dictionary<string, string> dict, string key)
        {
            if (dict == null)
            {
                return string.Empty;
            }

            return !dict.ContainsKey(key) ? string.Empty : dict[key];
        }
    }
}