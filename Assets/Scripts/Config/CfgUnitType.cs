// ******************************************************************
//       /\ /|       @file       CfgUnitType.cs
//       \ V/        @brief      excel数据解析(由python自动生成) ./xlsx/Battle/Unit.xlsx
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
    public class RowCfgUnitType
    {
        public string key; //key
        public string annotate; //注释
        public string defName; //枚举名称
        public string attackRangeSearcher; //攻击范围搜索器
    }

    public class CfgUnitType
    {
        private readonly Dictionary<string, RowCfgUnitType> _configs = new Dictionary<string, RowCfgUnitType>(); //cfgId映射row
        public RowCfgUnitType this[string key] => _configs.ContainsKey(key) ? _configs[key] : throw new Exception($"找不到配置 Cfg:{GetType()} key:{key}");
        public RowCfgUnitType this[int id] => _configs.ContainsKey(id.ToString()) ? _configs[id.ToString()] : throw new Exception($"找不到配置 Cfg:{GetType()} key:{id}");
        public List<RowCfgUnitType> AllConfigs => _configs.Values.ToList();

        /// <summary>
        /// 获取行数据
        /// </summary>
        public RowCfgUnitType Find(int i)
        {
            return this[i];
        }

        /// <summary>
        /// 获取行数据
        /// </summary>
        public RowCfgUnitType Find(string i)
        {
            return this[i];
        }

        /// <summary>
        /// 加载表数据
        /// </summary>
        public void Load()
        {
            var reader = new CsvReader();
            reader.LoadText("Assets/Resources/Config/CfgUnitType.txt", 3);
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
        private RowCfgUnitType ParseRow(string[] col)
        {
            //列越界
            if (col.Length < 4)
            {
                Debug.LogError($"配置表字段行数越界:{GetType()}");
                return null;
            }

            var data = new RowCfgUnitType();
            var rowHelper = new RowHelper(col);
            data.key = CsvUtility.ToString(rowHelper.ReadNextCol()); //key
            data.annotate = CsvUtility.ToString(rowHelper.ReadNextCol()); //注释
            data.defName = CsvUtility.ToString(rowHelper.ReadNextCol()); //枚举名称
            data.attackRangeSearcher = CsvUtility.ToString(rowHelper.ReadNextCol()); //攻击范围搜索器
            return data;
        }
    }
}