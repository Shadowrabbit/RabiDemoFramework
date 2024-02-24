// ******************************************************************
//       /\ /|       @file       CfgLevel.cs
//       \ V/        @brief      excel数据解析(由python自动生成) ./xlsx/Battle/Level.xlsx
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
    public class RowCfgLevel
    {
        public string key; //key
        public string annotate; //注释
        public string defName; //枚举名称
        public string camp; //阵营
        public string unitType; //单位类型
        public Vector3Int position; //位置
        public int level; //所在关卡
    }

    public class CfgLevel
    {
        private readonly Dictionary<string, RowCfgLevel> _configs = new Dictionary<string, RowCfgLevel>(); //cfgId映射row
        private readonly Dictionary<int, List<RowCfgLevel>> _levelConfigGroup = new Dictionary<int, List<RowCfgLevel>>();
        public RowCfgLevel this[string key] => _configs.ContainsKey(key) ? _configs[key] : throw new Exception($"找不到配置 Cfg:{GetType()} key:{key}");
        public RowCfgLevel this[int id] => _configs.ContainsKey(id.ToString()) ? _configs[id.ToString()] : throw new Exception($"找不到配置 Cfg:{GetType()} key:{id}");
        public List<RowCfgLevel> AllConfigs => _configs.Values.ToList();

        /// <summary>
        /// 获取行数据
        /// </summary>
        public RowCfgLevel Find(int i)
        {
            return this[i];
        }

        /// <summary>
        /// 获取行数据
        /// </summary>
        public RowCfgLevel Find(string i)
        {
            return this[i];
        }

        /// <summary>
        /// 加载表数据
        /// </summary>
        public void Load()
        {
            var reader = new CsvReader();
            reader.LoadText("Assets/Resources/Config/CfgLevel.txt", 3);
            var rows = reader.GetRowCount();
            for (var i = 0; i < rows; ++i)
            {
                var row = reader.GetColValueArray(i);
                var data = ParseRow(row);
                if (!_configs.ContainsKey(data.key))
                {
                    _configs.Add(data.key, data);
                }

                if (!_levelConfigGroup.ContainsKey(data.level))
                {
                     _levelConfigGroup.Add(data.level, new List<RowCfgLevel>());
                }

                _levelConfigGroup[data.level].Add(data);
            }
        }

        /// <summary>
        /// 根据level值获取分组
        /// </summary>
        public List<RowCfgLevel> GetListByLevel(int groupValue)
        {
                return _levelConfigGroup.ContainsKey(groupValue) ? _levelConfigGroup[groupValue] : throw new Exception($"找不到组 Cfg:{GetType()} groupId:{groupValue}");
        }

        /// <summary>
        /// 解析行
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        private RowCfgLevel ParseRow(string[] col)
        {
            //列越界
            if (col.Length < 7)
            {
                Debug.LogError($"配置表字段行数越界:{GetType()}");
                return null;
            }

            var data = new RowCfgLevel();
            var rowHelper = new RowHelper(col);
            data.key = CsvUtility.ToString(rowHelper.ReadNextCol()); //key
            data.annotate = CsvUtility.ToString(rowHelper.ReadNextCol()); //注释
            data.defName = CsvUtility.ToString(rowHelper.ReadNextCol()); //枚举名称
            data.camp = CsvUtility.ToString(rowHelper.ReadNextCol()); //阵营
            data.unitType = CsvUtility.ToString(rowHelper.ReadNextCol()); //单位类型
            data.position = CsvUtility.ToVector3int(rowHelper.ReadNextCol()); //位置
            data.level = CsvUtility.ToInt(rowHelper.ReadNextCol()); //所在关卡
            return data;
        }
    }
}