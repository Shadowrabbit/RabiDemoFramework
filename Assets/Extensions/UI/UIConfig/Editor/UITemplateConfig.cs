// ******************************************************************
//       /\ /|       @file       UITemplateConfig
//       \ V/        @brief      UI模板配置
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2022-10-21 18:25
//    *(__\_\        @Copyright  Copyright (c) 2022, Shadowrabbit
// ******************************************************************

using System.Collections.Generic;
using System.IO;
using System.Text;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Rabi
{
    [CreateAssetMenu(fileName = "UITemplateConfig", menuName = "Rabi/Create UITemplateConfig", order = 1)]
    public class UITemplateConfig : ScriptableObject
    {
        public List<UICompConfigData> uiCompConfigDataList = new List<UICompConfigData>(); //UI组件配置

        [Button("生成", ButtonSizes.Medium)]
        private void Generate()
        {
            var sb = new StringBuilder();
            sb.Append(@"
using UnityEngine;
using UnityEditor;

namespace Rabi
{
    public static class UIFactory
    {
        private static void CreateInstance(GameObject ui, MenuCommand menuCommand)
        {
            var parent = menuCommand.context as GameObject;
            if (!parent)
            {
	            return;
            }

            var go = Object.Instantiate(ui, parent.transform);
            go.name = ui.name;
            Selection.activeGameObject = go;
        }
");
            foreach (var item in uiCompConfigDataList)
            {
                sb.Append("\r\n");
                sb.AppendFormat("\t\t[MenuItem(\"GameObject/拉比科技/UI模板/{0}\", false, 0)]\r\n", item.compName);
                sb.AppendFormat("\t\tprivate static void Create{0}(MenuCommand menuCommand)\r\n",
                    item.protoTemplate.name.Replace(" ", ""));
                sb.Append("\t\t{\r\n");
                sb.AppendFormat("\t\t\tvar prefab = AssetDatabase.LoadAssetAtPath<GameObject>(\"{0}\");\r\n",
                    AssetDatabase.GetAssetPath(item.protoTemplate));
                sb.Append("\t\t\tCreateInstance(prefab, menuCommand);\r\n");
                sb.Append("\t\t}\r\n");
            }

            sb.Append("\t}\r\n");
            sb.Append("}");
            using (var textWriter = new StreamWriter(
                Path.Combine(Application.dataPath, "Extensions/UI/UIConfig/Editor/Generator/UITemplateGenerator.cs"), false,
                Encoding.UTF8))
            {
                textWriter.Write(sb.ToString());
                textWriter.Flush();
                textWriter.Close();
            }

            AssetDatabase.Refresh();
        }
    }
}