// ******************************************************************
//       /\ /|       @file       ListEx.cs
//       \ V/        @brief      List结构扩展
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2022-04-05 01:29:41
//    *(__\_\        @Copyright  Copyright (c) 2022, Shadowrabbit
// ******************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace Rabi
{
    public static class ListEx
    {
        /// <summary>
        /// 打乱顺序
        /// </summary>
        /// <param name="list"></param>
        /// <typeparam name="T"></typeparam>
        public static void Shuffle<T>(this IList<T> list)
        {
            if (list == null)
            {
                return;
            }

            var i = list.Count;
            while (i > 0)
            {
                var index = Random.Range(0, i--);
                (list[index], list[i]) = (list[i], list[index]);
            }
        }

        /// <summary>
        /// 获取随机一个元素
        /// </summary>
        /// <param name="list"></param>
        /// <typeparam name="T"></typeparam>
        public static T GetRandomElement<T>(this IList<T> list)
        {
            if (list == null)
            {
                return default;
            }

            if (list.Count <= 0)
            {
                return default;
            }

            var rndIndex = Random.Range(0, list.Count);
            return list[rndIndex];
        }

        /// <summary>
        /// 过滤元素 留下满足filter的
        /// </summary>
        /// <param name="targetList"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static List<string> Pick(this List<string> targetList, Func<string, bool> filter)
        {
            var newTargetList = new List<string>();
            if (targetList == null)
            {
                return newTargetList;
            }

            newTargetList.AddRange(targetList.Where(filter));
            return newTargetList;
        }
    }
}