// ******************************************************************
//       /\ /|       @file       CfgAudioLayer.cs
//       \ V/        @brief      excel数据解析(由python自动生成) ./xlsx/Audio.xlsx
//       | "")       @author     Shadowrabbit, yue.wang04@mihoyo.com
//       /  |
//      /  \\        @Modified   2022-04-25 13:25:11
//    *(__\_\        @Copyright  Copyright (c)  2022, Shadowrabbit
// ******************************************************************

using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Rabi
{
    public class RowCfgAudioLayer
    {
        public string key; //key
        public string annotate; //注释
        public string defName; //枚举名称
        public bool needOverlay; //需要叠加
        public bool needLoop; //需要循环
    }

    public class CfgAudioLayer
    {
        private readonly Dictionary<string, RowCfgAudioLayer> _configs = new Dictionary<string, RowCfgAudioLayer>(); //cfgId映射row
        public RowCfgAudioLayer this[string key] => _configs.ContainsKey(key) ? _configs[key] : throw new Exception($"找不到配置 Cfg:{GetType()} key:{key}");
        public RowCfgAudioLayer this[int id] => _configs.ContainsKey(id.ToString()) ? _configs[id.ToString()] : throw new Exception($"找不到配置 Cfg:{GetType()} key:{id}");
        public List<RowCfgAudioLayer> AllConfigs => _configs.Values.ToList();

        /// <summary>
        /// 获取行数据
        /// </summary>
        public RowCfgAudioLayer Find(int i)
        {
            return this[i];
        }

        /// <summary>
        /// 获取行数据
        /// </summary>
        public RowCfgAudioLayer Find(string i)
        {
            return this[i];
        }

        /// <summary>
        /// 加载表数据
        /// </summary>
        public void Load()
        {
            var reader = new CsvReader();
            reader.LoadText("Assets/Resources/Config/CfgAudioLayer.txt", 3);
            var rows = reader.GetRowCount();
            for (var i = 0; i < rows; ++i)
            {
                var row = reader.GetColValueArray(i);
                var data = ParseRow(row);
                if (!_configs.ContainsKey(data.key))
                {
                    _configs.Add(data.key, data);
                }
            }
        }

        /// <summary>
        /// 解析行
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        private RowCfgAudioLayer ParseRow(string[] col)
        {
            //列越界
            if (col.Length < 5)
            {
                Debug.LogError($"配置表字段行数越界:{GetType()}");
                return null;
            }

            var data = new RowCfgAudioLayer();
            var rowHelper = new RowHelper(col);
            data.key = CsvUtility.ToString(rowHelper.ReadNextCol()); //key
            data.annotate = CsvUtility.ToString(rowHelper.ReadNextCol()); //注释
            data.defName = CsvUtility.ToString(rowHelper.ReadNextCol()); //枚举名称
            data.needOverlay = CsvUtility.ToBool(rowHelper.ReadNextCol()); //需要叠加
            data.needLoop = CsvUtility.ToBool(rowHelper.ReadNextCol()); //需要循环
            return data;
        }
    }
}