// ******************************************************************
//       /\ /|       @file       BattleManager
//       \ V/        @brief      
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2024-02-24 20:41
//    *(__\_\        @Copyright  Copyright (c) 2024, Shadowrabbit
// ******************************************************************

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Rabi
{
    public class BattleManager : BaseSingleTon<BattleManager>
    {
        public string currentLevelId; //当前关卡id
        private readonly Dictionary<int, Unit> _eid2Unit = new Dictionary<int, Unit>(); //eid对棋子映
        private string _curTurnCamp = DefCamp.DRed;
        private string _winnerCamp;
        private Unit _selectedUnit;

        public void Enter(int levelId)
        {
            _winnerCamp = null;
            _curTurnCamp = DefCamp.DRed;
            MapManager.Instance.Init();
            MapManager.Instance.CreateMap(MapDef.MapWidth, MapDef.MapHeright);
            var rowCfgLevelList = ConfigManager.Instance.cfgLevel.GetListByLevel(levelId);
            foreach (var rowCfgLevel in rowCfgLevelList)
            {
                var unitType = rowCfgLevel.unitType;
                var rowCfgUnitType = ConfigManager.Instance.cfgUnitType[unitType];
                var eid = IdGenerator.GetEid();
                var unit = AssetManager.Instance.InstantiateSyncEx<Unit>(rowCfgUnitType.modelPath,
                    MapManager.Instance.GetThingContainer(), eid, unitType, rowCfgLevel.camp);
                var gridInfo = MapManager.Instance.FindGridInfo(rowCfgLevel.position.x, rowCfgLevel.position.y);
                gridInfo.unitInfoOnGrid = unit.unitInfo;
                var (x, y) = gridInfo.GetWorldPosition(MapDef.MapWidth, MapDef.MapHeright);
                unit.transform.position = new Vector3(x, y, 0);
                _eid2Unit.Add(eid, unit);
            }

            EventManager.Instance.AddListener(EventId.OnClickAny, OnClickAny);
        }

        public void Exit()
        {
            EventManager.Instance.RemoveListener(EventId.OnClickAny, OnClickAny);
            foreach (var unit in _eid2Unit.Values)
            {
                AssetManager.Instance.Recycle(unit);
            }

            _eid2Unit.Clear();
            MapManager.Instance.ExitMap();
        }

        private void OnClickAny()
        {
            if (_winnerCamp != null)
            {
                return;
            }

#if UNITY_STANDALONE
            var mouse = Mouse.current;
            if (mouse == null)
            {
                return;
            }

            var mousePos = mouse.position.ReadValue();

#elif UNITY_ANDROID
            var touch = Touchscreen.current; //获取当前运行环境的触控组件
            if (touch == null) //设备可能是空 -- 例如在手机端获取pc键盘
            {
                return;
            }

            if (touch.touches.Count <= 1)
            {
                return;
            }

            var tc = touch.touches[0]; //获取手指点击信息
            var mousePos = tc.position.ReadValue();
#endif
            var mainCamera = GameManager.Instance.GetMainCamera();
            var ray = mainCamera.ScreenPointToRay(mousePos);
            Debug.DrawRay(ray.origin, ray.direction, Color.green); //绘制一条绿色的射线  起点-方向
            var hit2d = Physics.Raycast(ray, out var hitInfo);
            if (hit2d)
            {
                Debug.Log(hitInfo.transform.name);
                OnClickObject(hitInfo.transform.gameObject);
                return;
            }

            OnClickNothing();
        }

        void OnClickObject(GameObject gameObject)
        {
            // if (clickedPieceObject == null)
            // {
            //     var piece = gameObject.GetPieceBehavior()?.Piece;
            //     if (piece != null)
            //     {
            //         Debug.Log(string.Format("Clicked piece at ({0},{1}).", piece.Position.X, piece.Position.Y));
            //         availableTargetPoints = game.GetTargetPositions(piece);
            //         if (availableTargetPoints.Count > 0)
            //         {
            //             clickedPieceObject = gameObject;
            //             UpdateTargetPoints();
            //         }
            //
            //         return;
            //     }
            // }
            // else
            // {
            //     var targetPosition = gameObject.GetTargetPointBehavior()?.Position;
            //     if (targetPosition != null)
            //     {
            //         var position = (Position) targetPosition;
            //         Debug.Log(string.Format("Clicked target point at ({0},{1}).", position.X, position.Y));
            //         game.Move(clickedPieceObject.GetPieceBehavior().Piece, position);
            //         UpdatePieces();
            //         return;
            //     }
            // }

            //OnClickNothing();
        }

        private void OnClickNothing()
        {
            _selectedUnit = null;
            MapManager.Instance.ClearAttackRangeCursor();
            Debug.Log("Clicked nothing.");
        }
    }
}