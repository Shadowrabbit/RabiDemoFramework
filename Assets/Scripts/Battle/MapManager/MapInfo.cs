// ******************************************************************
//       /\ /|       @file       MapInfo
//       \ V/        @brief      地图战场数据
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2024-02-08 11:44
//    *(__\_\        @Copyright  Copyright (c) 2024, Shadowrabbit
// ******************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Rabi
{
    [Serializable]
    public class MapInfo
    {
        public int width;
        public int height;

        public readonly Dictionary<Vector2Int, GridInfo>
            cellPos2GridInfo = new Dictionary<Vector2Int, GridInfo>(); //地图坐标对地块数据映射

        public MapInfo()
        {
        }

        public MapInfo(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        /// <summary>
        /// 添加地块信息
        /// </summary>
        /// <param name="gridInfo"></param>
        public void AddGridInfo(GridInfo gridInfo)
        {
            if (gridInfo == null)
            {
                return;
            }

            var vector2Int = new Vector2Int(gridInfo.posX, gridInfo.posY);
            if (cellPos2GridInfo.ContainsKey(vector2Int))
            {
                return;
            }

            cellPos2GridInfo.Add(vector2Int, gridInfo);
        }

        /// <summary>
        /// 清理地块数据
        /// </summary>
        public void Clear()
        {
            cellPos2GridInfo.Clear();
        }

        /// <summary>
        /// 查找地块
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public GridInfo FindGridInfo(int x, int y)
        {
            var vector = new Vector2Int(x, y);
            return !cellPos2GridInfo.ContainsKey(vector) ? null : cellPos2GridInfo[vector];
        }

        /// <summary>
        /// 查找单位所在地块
        /// </summary>
        /// <param name="unitInfo"></param>
        /// <returns></returns>
        public GridInfo FindGridInfo(UnitInfo unitInfo)
        {
            return (from gridInfo in cellPos2GridInfo.Values
                let unitInfoOnGrid = gridInfo.unitInfoOnGrid
                where unitInfoOnGrid != null
                where unitInfoOnGrid == unitInfo
                select gridInfo).FirstOrDefault();
        }
    }
}