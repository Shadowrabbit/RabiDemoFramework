/****************************************************
	文件：DotNetExtensions.cs
	作者：空银子
	邮箱: 1184840945@qq.com
	日期：2022/03/16 13:35:11
	功能：C#相关的扩展
*****************************************************/

using System;
using System.Collections.Generic;
using System.Linq;

namespace Rabi
{
    public static class DotNetEx
    {
        /// <summary>
        /// 获取int型堆叠
        /// </summary>
        /// <param name="valueObjList"></param>
        /// <returns></returns>
        public static int GetIntStackValue(this IEnumerable<object> valueObjList)
        {
            var result = 0;
            foreach (var valueObj in valueObjList)
            {
                result += (int)valueObj;
            }

            return result;
        }

        /// <summary>
        /// 获取float型堆叠
        /// </summary>
        /// <param name="valueObjList"></param>
        /// <returns></returns>
        public static float GetFloatStackValue(this IEnumerable<object> valueObjList)
        {
            var result = 0f;
            foreach (var valueObj in valueObjList)
            {
                result += (float)valueObj;
            }

            return result;
        }

        public static T TryGetValue<TK, T>(this Dictionary<TK, T> dic, TK key)
        {
            return dic.ContainsKey(key) ? dic[key] : default;
        }

        public static void RemoveSafe<T>(this List<T> list, T target)
        {
            if (list.Contains(target))
            {
                list.Remove(target);
            }
        }

        public static void RemoveSafe<TK, TV>(this Dictionary<TK, TV> dic, TK targetKey)
        {
            if (dic.ContainsKey(targetKey))
            {
                dic.Remove(targetKey);
            }
        }

        public static T[] ForEach<T>(this T[] selfArray, Action<T> action)
        {
            Array.ForEach(selfArray, action);
            return selfArray;
        }

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> selfArray, Action<T> action)
        {
            if (action == null) throw new ArgumentException();
            var forEach = selfArray as T[] ?? selfArray.ToArray();
            foreach (var item in forEach)
            {
                action(item);
            }

            return forEach;
        }

        public static List<T> ForEachReverse<T>(this List<T> selfList, Action<T> action)
        {
            if (action == null) throw new ArgumentException();

            for (var i = selfList.Count - 1; i >= 0; --i)
                action(selfList[i]);

            return selfList;
        }

        public static bool ContainsName<T>(this List<T> selfList, string name) where T : UnityEngine.Object
        {
            return selfList.Any(item => item.name == name);
        }

        public static List<T> ForEachReverse<T>(this List<T> selfList, Action<T, int> action)
        {
            if (action == null) throw new ArgumentException();

            for (var i = selfList.Count - 1; i >= 0; --i)
                action(selfList[i], i);

            return selfList;
        }

        public static void ForEach<T>(this List<T> list, Action<int, T> action)
        {
            for (var i = 0; i < list.Count; i++)
            {
                action(i, list[i]);
            }
        }

        public static void CopyTo<T>(this List<T> from, List<T> to, int begin = 0, int end = -1)
        {
            if (begin < 0)
            {
                begin = 0;
            }

            for (var i = begin; i < end; i++)
            {
                to[i] = from[i];
            }
        }

        public static T TryGet<T>(this List<T> selfList, int index)
        {
            return selfList.Count > index ? selfList[index] : default(T);
        }

        public static Dictionary<TKey, TValue> Merge<TKey, TValue>(this Dictionary<TKey, TValue> dictionary,
            params Dictionary<TKey, TValue>[] dictionaries)
        {
            return dictionaries.Aggregate(dictionary,
                (current, dict) => current.Union(dict).ToDictionary(kv => kv.Key, kv => kv.Value));
        }

        /// <summary>
        /// 如果字典里取不到值，就返回此类型的默认值
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            dictionary.TryGetValue(key, out var value);
            return value;
        }

        /// <summary>
        /// 如果字典里取不到值，就返回我们给定的默认值
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key,
            TValue defaultValue)
        {
            return dictionary.TryGetValue(key, out var value) ? value : defaultValue;
        }

        /// <summary>
        /// 如果字典里取不到值，就通过我们给的方法创建一个值返回
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary"></param>
        /// <param name="key"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key,
            Func<TValue> provider)
        {
            return dictionary.TryGetValue(key, out var value) ? value : provider();
        }

        /// <summary>
        /// 如果字典里不包含这个键，就将这对键值添加进去
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void AddGrace<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (!dictionary.ContainsKey(key))
            {
                dictionary.Add(key, value);
            }
        }

        public static void ForEach<TK, TV>(this Dictionary<TK, TV> dict, Action<TK, TV> action)
        {
            var dictE = dict.GetEnumerator();

            while (dictE.MoveNext())
            {
                var current = dictE.Current;
                action(current.Key, current.Value);
            }

            dictE.Dispose();
        }

        public static void AddRange<TK, TV>(this Dictionary<TK, TV> dict, Dictionary<TK, TV> addInDict,
            bool isOverride = false)
        {
            var dictE = addInDict.GetEnumerator();

            while (dictE.MoveNext())
            {
                var current = dictE.Current;
                if (dict.ContainsKey(current.Key))
                {
                    if (isOverride)
                        dict[current.Key] = current.Value;
                    continue;
                }

                dict.Add(current.Key, current.Value);
            }

            dictE.Dispose();
        }
    }
}