// ******************************************************************
//       /\ /|       @file       GridInfo
//       \ V/        @brief      
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2024-02-24 20:48
//    *(__\_\        @Copyright  Copyright (c) 2024, Shadowrabbit
// ******************************************************************

using System;

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
        public (float, float) GetWorldPosition(int width, int height)
        {
            return new(posX - width / 2, posY - height / 2);
        }
    }
}