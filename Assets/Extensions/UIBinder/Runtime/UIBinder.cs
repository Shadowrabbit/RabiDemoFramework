// ******************************************************************
//       /\ /|       @file       UIBinder
//       \ V/        @brief      UI绑定器
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2024-02-23 18:05
//    *(__\_\        @Copyright  Copyright (c) 2024, Shadowrabbit
// ******************************************************************

using System.Collections.Generic;
using UnityEngine;

namespace Rabi
{
    public class UIBinder : MonoBehaviour
    {
        [SerializeField] public List<Component> bindCompList = new List<Component>(16); //自动绑定的组件实例列表 runtime真正用到的
#if UNITY_EDITOR
        public List<BindData> bindDataList = new List<BindData>(); //绑定的组件名称和实例 数据列表 编辑器临时用下
        [SerializeField] private string className; //生成代码的类名
        [SerializeField] private string @namespace; //生成代码的命名空间
        [SerializeField] private string codePath; //生成代码的路径
        public string ClassName => className;
        public string Namespace => @namespace;
        public string CodePath => codePath;
#endif

        /// <summary>
        /// 获取第index个被绑定的组件实例
        /// </summary>
        /// <param name="index"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetBindComponent<T>(int index) where T : Component
        {
            if (index >= bindCompList.Count)
            {
                Debug.LogError("索引无效");
                return null;
            }

            var bindCom = bindCompList[index] as T;
            if (bindCom != null)
            {
                return bindCom;
            }

            Debug.LogError("类型无效");
            return null;
        }
    }
}