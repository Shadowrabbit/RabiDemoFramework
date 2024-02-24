// ******************************************************************
//       /\ /|       @file       MapManager
//       \ V/        @brief      
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2024-02-24 20:44
//    *(__\_\        @Copyright  Copyright (c) 2024, Shadowrabbit
// ******************************************************************

using UnityEngine;

namespace Rabi
{
    public partial class MapManager : BaseSingleTon<MapManager>
    {
        private Transform _thingContainer; //单位挂点
        private Transform _cursorContainer; //光标挂点
        private MapInfo _mapInfo; //战斗用的地块数据

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            var thingContainer = GameObject.Find("ThingContainer");
            if (!thingContainer)
            {
                Logger.Error("找不到物体挂点");
            }

            _thingContainer = thingContainer.transform;
            var cursorContainer = GameObject.Find("CursorContainer");
            if (!cursorContainer)
            {
                Logger.Error("找不到物体挂点");
            }

            _cursorContainer = cursorContainer.transform;
        }

        /// <summary>
        /// 创建地图
        /// </summary>
        public void CreateMap(int width, int height)
        {
            _mapInfo ??= new MapInfo(width, height);
            _mapInfo.Clear();
            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    _mapInfo.AddGridInfo(new GridInfo(i, j));
                }
            }
        }

        public void ExitMap()
        {
            _mapInfo.Clear();
        }

        /// <summary>
        /// 查找地块
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public GridInfo FindGridInfo(int x, int y)
        {
            return _mapInfo.FindGridInfo(x, y);
        }

        /// <summary>
        /// 查找卡牌所在地块
        /// </summary>
        /// <param name="unitInfo"></param>
        /// <returns></returns>
        public GridInfo FindGridInfo(UnitInfo unitInfo)
        {
            return _mapInfo.FindGridInfo(unitInfo);
        }

        /// <summary>
        /// 获取物体挂点
        /// </summary>
        /// <returns></returns>
        public Transform GetThingContainer()
        {
            return _thingContainer;
        }

        /// <summary>
        /// 获取光标挂点
        /// </summary>
        /// <returns></returns>
        public Transform GetCursorContainer()
        {
            return _cursorContainer;
        }
    }
}