// ******************************************************************
//       /\ /|       @file       GameManager.cs
//       \ V/        @brief      游戏控制器
//       | "")       @author     Shadowrabbit, yue.wang04@mihoyo.com
//       /  |                    
//      /  \\        @Modified   2022-03-17 20:08:55
//    *(__\_\        @Copyright  Copyright (c)  2022, Shadowrabbit
// ******************************************************************

using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Rabi
{
    public class GameManager : BaseMonoSingleTon<GameManager>
    {
        [SerializeField] [LabelText("正式流程")] private bool autoStart;

        private readonly List<IMonoManager> _managerList = new List<IMonoManager>(); //管理器列表
        //private static GamePrucedureController _fsm; //游戏流程状态机

        private void Awake()
        {
            _managerList.Add(AssetManager.Instance);
            //_managerList.Add(SceneManager.Instance);
            _managerList.Add(ObjectPoolManager.Instance);
            _managerList.Add(FsmManager.Instance);
            // _managerList.Add(LuaManager.Instance);
            //_managerList.Add(AudioManager.Instance);
            _managerList.Add(InputManager.Instance);
            // _managerList.Add(ActionManager.Instance);
            foreach (var manager in _managerList)
            {
                manager.OnInit();
            }
        }

        private void Start()
        {
            DontDestroyOnLoad(this);
            UIManager.Instance.Init();
            UIManager.Instance.CloseAllWindow();
            UIManager.Instance.OpenWindow("MainWindow");
            InitPrucedure();
            if (autoStart)
            {
                //_fsm.ChangeState<PrucedureStart>();
            }
        }

        private void Update()
        {
            foreach (var manager in _managerList)
            {
                manager.Update();
            }
        }

        private void FixedUpdate()
        {
            foreach (var manager in _managerList)
            {
                manager.FixedUpdate();
            }
        }

        private void LateUpdate()
        {
            foreach (var manager in _managerList)
            {
                manager.LateUpdate();
            }
        }

        private void OnDestroy()
        {
            for (var i = _managerList.Count - 1; i >= 0; i--)
            {
                _managerList[i].OnClear();
            }
        }

        /// <summary>
        /// 初始化游戏流程
        /// </summary>
        private static void InitPrucedure()
        {
            // _fsm = FsmManager.Instance.GetFsm<GamePrucedureController>(FsmDef.GamePrucedure);
            // var prucedureStart = new PrucedureStart(); //启动游戏流程
            // var prucedureMainMenu = new PrucedureMainMenu(); //主菜单流程
            // var prucedureChangeScene = new PrucedureChangeScene(); //切换场景流程
            // var prucedureBattleSlg = new PrucedureBattleSlg(); //策略类战斗
            // _fsm.ResetStateList(new List<IFsmState>
            // {
            //     prucedureStart, prucedureMainMenu, prucedureChangeScene, prucedureBattleSlg
            // });
        }
    }
}