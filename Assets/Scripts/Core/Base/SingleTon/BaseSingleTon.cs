// ******************************************************************
//       /\ /|       @file       BaseSingleTon.cs
//       \ V/        @brief      单例模式基类
//       | "")       @author     Shadowrabbit, yue.wang04@mihoyo.com
//       /  |                    
//      /  \\        @Modified   2022-03-15 19:54:56
//    *(__\_\        @Copyright  Copyright (c)  2022, Shadowrabbit
// ******************************************************************

namespace Rabi
{
    public class BaseSingleTon<T> where T : class, new()
    {
        public static T Instance => Inner.InternalInstance;

        private static class Inner
        {
            internal static readonly T InternalInstance = new T();
        }
    }
}