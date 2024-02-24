// ******************************************************************
//       /\ /|       @file       GridInfo
//       \ V/        @brief      
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2024-02-24 20:48
//    *(__\_\        @Copyright  Copyright (c) 2024, Shadowrabbit
// ******************************************************************

using System;
using UnityEngine;

namespace Rabi
{
    [Serializable]
    public class GridInfo
    {
        public int posX;
        public int posY;
        public UnitInfo unitInfoOnGrid = null;

        public GridInfo(int x, int y)
        {
            posX = x;
            posY = y;
        }

        public GridInfo()
        {
        }

        /// <summary>
        /// 获取当前地块的世界坐标
        /// </summary>
        /// <returns></returns>
        public (float, float) GetWorldPosition(int mapWidth, int mapHeight)
        {
            var pivotOffset = new Vector2(0.5f, 0.5f); //一个单位1尺寸的地块 居中的重心偏移量
            var cellToWorldOffsetX = -(float) mapWidth / 2;
            var cellToWorldOffsetY = -(float) mapHeight / 2;
            return (posX + pivotOffset.x + cellToWorldOffsetX, posY + pivotOffset.y + cellToWorldOffsetY);
        }
    }
}