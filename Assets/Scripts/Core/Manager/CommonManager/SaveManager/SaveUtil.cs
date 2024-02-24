// ******************************************************************
//       /\ /|       @file       SaveUtil.cs
//       \ V/        @brief      存档工具
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2022-05-14 09:45:25
//    *(__\_\        @Copyright  Copyright (c) 2022, Shadowrabbit
// ******************************************************************

using System;
using System.IO;
using CatJson;
using UnityEngine;

namespace Rabi
{
    public static class SaveUtil
    {
        #region PlayerPrefs

        /// <summary>
        /// PlayerPrefs方式保存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        public static void SaveByPlayerPrefs(string key, object data)
        {
            //将数据转为Json格式
            var json = JsonUtility.ToJson(data);
            //保存
            PlayerPrefs.SetString(key, json);
            PlayerPrefs.Save();
#if UNITY_EDITOR
            Debug.Log("存档成功！");
#endif
        }

        /// <summary>
        /// PlayerPrefs方式读取
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T LoadByPlayerPrefs<T>(string key)
        {
            var json = PlayerPrefs.GetString(key, null);
            return JsonUtility.FromJson<T>(json);
        }

        #endregion

        #region JSON

        /// <summary>
        /// 检查数据文件 确保存在
        /// </summary>
        /// <param name="afterCreate">文件创建后的回调</param>
        /// <param name="saveFileName"></param>
        /// <typeparam name="T"></typeparam>
        public static void CheckDataFile<T>(Action<T> afterCreate = null, string saveFileName = null) where T : new()
        {
            saveFileName ??= typeof(T).Name;
            //路径 Combine 合并路径
            var path = Path.Combine(Application.persistentDataPath, saveFileName);
            if (File.Exists(path))
            {
                return;
            }

            var data = new T();
            afterCreate?.Invoke(data);
            SaveByJson(data);
        }

        /// <summary>
        /// Json方式保存
        /// </summary>
        /// <param name="saveFileName"></param>
        /// <param name="data"></param>
        public static void SaveByJson<T>(T data, string saveFileName = null)
        {
            saveFileName ??= data.GetType().Name;
            var json = JsonParser.Default.ToJson(data);
            //路径 Combine 合并路径
            var path = Path.Combine(Application.persistentDataPath, saveFileName);
            try
            {
                File.WriteAllText(path, json);

#if DEBUG
                Debug.Log($"存档成功！存档位置 {path}");
#endif
            }
            catch (Exception exception)
            {
#if DEBUG
                Debug.Log($"存档成功！存档位置 {path}. \n{exception}");
#endif
            }
        }

        /// <summary>
        /// Json方式读取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="saveFileName"></param>
        /// <returns></returns>
        public static T LoadFromJson<T>(string saveFileName = null) where T : new()
        {
            saveFileName ??= typeof(T).Name;
            //路径 Combine 合并路径
            var path = Path.Combine(Application.persistentDataPath, saveFileName);
            if (!File.Exists(path))
            {
                Logger.Error($"数据文件不存在 saveFileName:{saveFileName}");
                return new T();
            }

            try
            {
                var json = File.ReadAllText(path);
                var data = JsonParser.Default.ParseJson<T>(json);
                return data ?? new T();
            }
            catch (Exception e)
            {
#if DEBUG
                Debug.Log($"存档读取失败！存档位置 {path}. \n{e}");
#endif
                return new T();
            }
        }

        #endregion

        /// <summary>
        /// 删除存档
        /// </summary>
        /// <param name="saveFileName"></param>
        public static void DeleteSaveFile(string saveFileName)
        {
            //路径 Combine 合并路径
            var path = Path.Combine(Application.persistentDataPath, saveFileName);
            try
            {
                File.Delete(path);
            }
            catch (Exception e)
            {
#if UNITY_EDITOR
                Debug.Log($"删除存档失败！存档位置 {path}.\n{e}");
#endif
            }
        }
    }
}