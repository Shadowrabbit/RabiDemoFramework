// ******************************************************************
//       /\ /|       @file       UIConfigWindow
//       \ V/        @brief      UI配置页面
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2022-10-17 11:33
//    *(__\_\        @Copyright  Copyright (c) 2022, Shadowrabbit
// ******************************************************************

using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;

namespace Rabi
{
    public class UIConfigWindow : OdinEditorWindow
    {
        [FoldoutGroup("程序预设", Order = 100)] [InlineEditor]
        public UICompConfig uiCompConfig;

        [FoldoutGroup("美术预设", Order = 100)] [InlineEditor]
        public UITemplateConfig uiTemplateConfig;

        protected override void Initialize()
        {
            base.Initialize();
            uiCompConfig =
                AssetDatabase.LoadAssetAtPath<UICompConfig>("Assets/Extensions/UI/UIConfig/Asset/UICompConfig.asset");
            uiTemplateConfig =
                AssetDatabase.LoadAssetAtPath<UITemplateConfig>(
                    "Assets/Extensions/UI/UIConfig/Asset/UITemplateConfig.asset");
        }

        [MenuItem("拉比科技/UI配置")]
        public static void OpenWindow()
        {
            GetWindow<UIConfigWindow>("UI编辑器");
        }
    }
}