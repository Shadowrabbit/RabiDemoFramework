// ******************************************************************
//       /\ /|       @file       BattleMapDrawer
//       \ V/        @brief      地图光标绘制
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2024-01-02 23:03
//    *(__\_\        @Copyright  Copyright (c) 2024, Shadowrabbit
// ******************************************************************

using System.Collections.Generic;
using UnityEngine;

namespace Rabi
{
    public partial class MapManager
    {
        private GameObject _targetCursor; //目标光标
        private readonly List<GameObject> _attackRangeCursorList = new List<GameObject>(); //放置光标框列表

        /// <summary>
        /// 绘制攻击范围
        /// </summary>
        /// <param name="attackRange"></param>
        public void DrawAttackRangeCursor(List<GridInfo> attackRange)
        {
            if (attackRange == null)
            {
                return;
            }

            //绘制攻击范围光标
            foreach (var grid in attackRange)
            {
                var obj = AssetManager.Instance.InstantiateSyncEx(MapDef.MoveCursorPath,
                    GetCursorContainer());
                var (item1, item2) = grid.GetWorldPosition(_mapInfo.width, _mapInfo.height);
                obj.transform.position = new Vector2(item1, item2);
                _attackRangeCursorList.Add(obj);
            }
        }

        /// <summary>
        /// 清除攻击范围光标
        /// </summary>
        public void ClearAttackRangeCursor()
        {
            if (_attackRangeCursorList.Count <= 0)
            {
                return;
            }

            //销毁所有地块选择光标框
            foreach (var obj in _attackRangeCursorList)
            {
                AssetManager.Instance.Recycle(obj);
            }

            _attackRangeCursorList.Clear();
        }
    }
}