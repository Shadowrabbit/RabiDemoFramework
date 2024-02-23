// ******************************************************************
//       /\ /|       @file       UIBinderGlobalSetting
//       \ V/        @brief      全局设置
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2024-02-23 18:27
//    *(__\_\        @Copyright  Copyright (c) 2024, Shadowrabbit
// ******************************************************************

using UnityEngine;
using UnityEditor;

namespace Rabi
{
    public class UIBinderGlobalSetting : ScriptableObject
    {
        [SerializeField] private string codePath; //默认代码生成路径
        [SerializeField] private string @namespace; //默认命名空间
        public string CodePath => codePath;
        public string Namespace => @namespace;

        /// <summary>
        /// 创建全局设置数据
        /// </summary>
        [MenuItem("Rabi/UIBinderGlobalSetting")]
        private static void CreateUIBinderGlobalSetting()
        {
            var paths = AssetDatabase.FindAssets("t:UIBinderGlobalSetting");
            if (paths.Length >= 1)
            {
                var path = AssetDatabase.GUIDToAssetPath(paths[0]);
                EditorUtility.DisplayDialog("警告", $"已存在UIBinderGlobalSetting，路径:{path}", "确认");
                return;
            }

            var setting = CreateInstance<UIBinderGlobalSetting>();
            AssetDatabase.CreateAsset(setting, "Assets/UIBinderGlobalSetting.asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}