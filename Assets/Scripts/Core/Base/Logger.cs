// ******************************************************************
//       /\ /|       @file       Logger.cs
//       \ V/        @brief      日志工具
//       | "")       @author     Shadowrabbit, yue.wang04@mihoyo.com
//       /  |                    
//      /  \\        @Modified   2022-03-17 22:14:08
//    *(__\_\        @Copyright  Copyright (c)  2022, Shadowrabbit
// ******************************************************************

using UnityEngine;

namespace Rabi
{
    public static class Logger
    {
        public static void Log(string str)
        {
//#if DEBUG
            Debug.Log(str);
//#endif
        }

        public static void Warning(string str)
        {
#if DEBUG
            Debug.LogWarning(str);
#endif
        }

        public static void Error(string str)
        {
            Debug.LogError(str);
        }
    }
}