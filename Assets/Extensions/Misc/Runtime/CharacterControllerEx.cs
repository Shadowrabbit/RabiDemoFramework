// ******************************************************************
//       /\ /|       @file       CharacterControllerEx
//       \ V/        @brief      角色控制扩展
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2022-06-19 17:06
//    *(__\_\        @Copyright  Copyright (c) 2022, Shadowrabbit
// ******************************************************************

namespace Rabi
{
    using UnityEngine;

    public static class CharacterControllerEx
    {
        /// <summary>
        /// 判断角色是否在地面 默认判断方式根据上一次move时是否接触碰撞体 这种方法在陡坡时会导致值不稳定
        /// </summary>
        /// <param name="characterController"></param>
        public static bool IsOnGroundEx(this CharacterController characterController)
        {
            if (characterController.isGrounded)
            {
                return true;
            }

            var ray = new Ray(characterController.transform.position + Vector3.up * 0.1f, Vector3.down);
            const float tolerance = 0.3f; //探索距离
            //记得创建对应的层级
            return Physics.Raycast(ray, tolerance, LayerMask.NameToLayer("Default"));
        }
    }
}