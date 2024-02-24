// ******************************************************************
//       /\ /|       @file       InputManager
//       \ V/        @brief      输入管理器
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2022-06-15 13:30
//    *(__\_\        @Copyright  Copyright (c) 2022, Shadowrabbit
// ******************************************************************

using UnityEngine.InputSystem;

namespace Rabi
{
    public class InputManager : BaseSingleTon<InputManager>, IMonoManager
    {
        private readonly InputControls _inputControl = new InputControls(); //按键控制映射

        public void OnInit()
        {
            _inputControl.Enable();
            _inputControl.NonUI.Click.performed += OnClickAny;
            _inputControl.NonUI.Hold.performed += OnHoldAny;
            Logger.Log("输入管理器初始化");
        }

        public void OnClear()
        {
            _inputControl.NonUI.Click.performed -= OnClickAny;
            _inputControl.NonUI.Hold.performed -= OnHoldAny;
            _inputControl.Disable();
        }

        public void Update()
        {
        }

        public void FixedUpdate()
        {
        }

        public void LateUpdate()
        {
        }

        /// <summary>
        /// 鼠标左键点击
        /// </summary>
        /// <param name="obj"></param>
        private static void OnClickAny(InputAction.CallbackContext obj)
        {
            EventManager.Instance.Dispatch(EventId.OnClickAny);
        }

        /// <summary>
        /// 鼠标左键长按
        /// </summary>
        /// <param name="obj"></param>
        private static void OnHoldAny(InputAction.CallbackContext obj)
        {
            EventManager.Instance.Dispatch(EventId.OnHoldAny);
        }
    }
}