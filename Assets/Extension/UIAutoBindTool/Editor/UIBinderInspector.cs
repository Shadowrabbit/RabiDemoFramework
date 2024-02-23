// ******************************************************************
//       /\ /|       @file       UIBinderInspector
//       \ V/        @brief      
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2024-02-23 18:30
//    *(__\_\        @Copyright  Copyright (c) 2024, Shadowrabbit
// ******************************************************************

using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using Object = UnityEngine.Object;

namespace Rabi
{
    [CustomEditor(typeof(UIBinder))]
    public class UIBinderInspector : Editor
    {
        private UIBinder _target; //自身实例
        private SerializedProperty _bindDataList; //编辑器用到的绑定数据列表 <BindData>结构
        private SerializedProperty _bindCompList; //运行时用的组件实例缓存列表 也是最终需要拿到的数据
        private SerializedProperty _namespace; //生成代码的命名空间
        private SerializedProperty _className; //生成代码的类名
        private SerializedProperty _codePath; //生成代码的路径
        private UIBinderGlobalSetting _setting; //全局默认配置

        protected void OnEnable()
        {
            CheckGlobalSetting();
            _target = (UIBinder) target;
            _bindDataList = serializedObject.FindProperty("bindDataList");
            _bindCompList = serializedObject.FindProperty("bindCompList");
            _namespace = serializedObject.FindProperty("namespace");
            _className = serializedObject.FindProperty("className");
            _codePath = serializedObject.FindProperty("codePath");
            _namespace.stringValue =
                string.IsNullOrEmpty(_namespace.stringValue) ? _setting.Namespace : _namespace.stringValue;
            _className.stringValue = string.IsNullOrEmpty(_className.stringValue)
                ? _target.gameObject.name
                : _className.stringValue;
            _codePath.stringValue =
                string.IsNullOrEmpty(_codePath.stringValue) ? _setting.CodePath : _codePath.stringValue;
            serializedObject.ApplyModifiedProperties();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            DrawTopButton();
            DrawSetting();
            DrawBindMap(); //绘制绑定映射 字段名->组件引用
            serializedObject.ApplyModifiedProperties();
        }

        /// <summary>
        /// 绘制顶部按钮
        /// </summary>
        private void DrawTopButton()
        {
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("全部删除"))
            {
                RemoveAll();
            }

            if (GUILayout.Button("删除空引用"))
            {
                RemoveNull();
            }

            if (GUILayout.Button("自动绑定组件"))
            {
                AutoBindComponent();
            }

            if (GUILayout.Button("生成绑定代码"))
            {
                GenAutoBindCode();
            }

            EditorGUILayout.EndHorizontal();
        }

        /// <summary>
        /// 全部删除
        /// </summary>
        private void RemoveAll()
        {
            _bindDataList.ClearArray();
            SyncBindCompList();
        }

        /// <summary>
        /// 删除空引用
        /// </summary>
        private void RemoveNull()
        {
            if (_bindDataList == null)
            {
                return;
            }

            for (var i = _bindDataList.arraySize - 1; i >= 0; i--)
            {
                var element = _bindDataList.GetArrayElementAtIndex(i)?.FindPropertyRelative("comp");
                if (element?.objectReferenceValue == null)
                {
                    _bindDataList.DeleteArrayElementAtIndex(i);
                }
            }

            SyncBindCompList();
        }

        /// <summary>
        /// 自动绑定组件
        /// </summary>
        private void AutoBindComponent()
        {
            _bindDataList.ClearArray();
            var childArray = _target.gameObject.GetComponentsInChildren<Transform>(true);
            foreach (var child in childArray)
            {
                var nodeName = child.name;
                var type = BinderRuler.CalcNodeType(nodeName);
                if (type == null)
                {
                    continue;
                }

                var comp = child.GetComponent(type);
                if (!comp)
                {
                    Debug.LogError($"找不到组件实例 nodeName:{nodeName} type:{type.Name}");
                    continue;
                }

                AddBindData(nodeName, comp);
            }

            SyncBindCompList();
        }

        /// <summary>
        /// 绘制设置项
        /// </summary>
        private void DrawSetting()
        {
            EditorGUILayout.BeginHorizontal();
            _namespace.stringValue = EditorGUILayout.TextField(new GUIContent("命名空间："), _namespace.stringValue);
            if (GUILayout.Button("默认设置"))
            {
                _namespace.stringValue = _setting.Namespace;
            }

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            _className.stringValue = EditorGUILayout.TextField(new GUIContent("类名："), _className.stringValue);
            if (GUILayout.Button("物体名"))
            {
                _className.stringValue = _target.gameObject.name;
            }

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.LabelField("代码保存路径：");
            EditorGUILayout.LabelField(_codePath.stringValue);
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("选择路径"))
            {
                var temp = _codePath.stringValue;
                _codePath.stringValue = EditorUtility.OpenFolderPanel("选择代码保存路径", Application.dataPath, "");
                if (string.IsNullOrEmpty(_codePath.stringValue))
                {
                    _codePath.stringValue = temp;
                }
            }

            if (GUILayout.Button("默认设置"))
            {
                _codePath.stringValue = _setting.CodePath;
            }

            EditorGUILayout.EndHorizontal();
        }

        /// <summary>
        /// 绘制键值对数据
        /// </summary>
        private void DrawBindMap()
        {
            //绘制key value数据
            var needDeleteIndex = -1;
            EditorGUILayout.BeginVertical();
            for (var i = 0; i < _bindDataList.arraySize; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField($"[{i}]", GUILayout.Width(25));
                var property = _bindDataList.GetArrayElementAtIndex(i).FindPropertyRelative("name");
                property.stringValue = EditorGUILayout.TextField(property.stringValue, GUILayout.Width(150));
                property = _bindDataList.GetArrayElementAtIndex(i).FindPropertyRelative("comp");
                property.objectReferenceValue =
                    EditorGUILayout.ObjectField(property.objectReferenceValue, typeof(Component), true);

                if (GUILayout.Button("X"))
                {
                    //将元素下标添加进删除list
                    needDeleteIndex = i;
                }

                EditorGUILayout.EndHorizontal();
            }

            //删除data
            if (needDeleteIndex != -1)
            {
                _bindDataList.DeleteArrayElementAtIndex(needDeleteIndex);
                SyncBindCompList();
            }

            EditorGUILayout.EndVertical();
        }

        /// <summary>
        /// 添加绑定数据
        /// </summary>
        private void AddBindData(string compName, Object bindCom)
        {
            var index = _bindDataList.arraySize;
            _bindDataList.InsertArrayElementAtIndex(index);
            var element = _bindDataList.GetArrayElementAtIndex(index);
            element.FindPropertyRelative("name").stringValue = compName;
            element.FindPropertyRelative("comp").objectReferenceValue = bindCom;
        }

        /// <summary>
        /// 生成自动绑定代码
        /// </summary>
        private void GenAutoBindCode()
        {
            var go = _target.gameObject;
            var className = !string.IsNullOrEmpty(_target.ClassName) ? _target.ClassName : go.name;
            var codePath = !string.IsNullOrEmpty(_target.CodePath) ? _target.CodePath : _setting.CodePath;
            if (!Directory.Exists(codePath))
            {
                Debug.LogError($"{go.name}的代码保存路径{codePath}无效");
            }

            using (var sw = new StreamWriter($"{codePath}/{className}.BindComponents.cs"))
            {
                sw.WriteLine("// ******************************************************************");
                sw.WriteLine($"//       /\\ /|       @file       {className}");
                sw.WriteLine("//       \\ V/        @brief      ");
                sw.WriteLine("//       | \"\")       @author     Shadowrabbit, yingtu0401@gmail.com");
                sw.WriteLine("//       /  |                    ");
                sw.WriteLine($"//      /  \\        @Modified   {DateTime.Now}");
                sw.WriteLine($"//    *(__\\_\\        @Copyright  Copyright (c) {DateTime.Today.Year}, Shadowrabbit");
                sw.WriteLine("// ******************************************************************");
                sw.WriteLine("");
                sw.WriteLine("using UnityEngine;");
                sw.WriteLine("using UnityEngine.UI;");
                sw.WriteLine("");
                if (!string.IsNullOrEmpty(_target.Namespace))
                {
                    //命名空间
                    sw.WriteLine("namespace " + _target.Namespace);
                    sw.WriteLine("{");
                }

                //类名
                sw.WriteLine($"\tpublic partial class {className}");
                sw.WriteLine("\t{");
                //组件字段
                foreach (var data in _target.bindDataList)
                {
                    sw.WriteLine($"\t\tprivate {data.comp.GetType().Name} {data.name};");
                }

                sw.WriteLine("");
                sw.WriteLine("\t\tprivate void GetBindComponents(GameObject go)");
                sw.WriteLine("\t\t{");
                sw.WriteLine($"\t\t\tvar uiBinder = go.GetComponent<UIBinder>();");
                //根据索引获取
                for (var i = 0; i < _target.bindDataList.Count; i++)
                {
                    var data = _target.bindDataList[i];
                    var filedName = $"{data.name}";
                    sw.WriteLine(
                        $"\t\t\t{filedName} = uiBinder.GetBindComponent<{data.comp.GetType().Name}>({i});");
                }

                sw.WriteLine("\t\t}");
                sw.WriteLine("\t}");
                if (!string.IsNullOrEmpty(_target.Namespace))
                {
                    sw.WriteLine("}");
                }
            }

            AssetDatabase.Refresh();
            EditorUtility.DisplayDialog("提示", "代码生成完毕", "OK");
        }

        /// <summary>
        /// 检查全局设置
        /// </summary>
        private void CheckGlobalSetting()
        {
            var paths = AssetDatabase.FindAssets("t:UIBinderGlobalSetting");
            switch (paths.Length)
            {
                case 0:
                    Debug.LogError("不存在UIBinderGlobalSetting");
                    return;
                case > 1:
                    Debug.LogError("UIBinderGlobalSetting数量大于1");
                    return;
            }

            var path = AssetDatabase.GUIDToAssetPath(paths[0]);
            _setting = AssetDatabase.LoadAssetAtPath<UIBinderGlobalSetting>(path);
        }

        /// <summary>
        /// 同步绑定数据
        /// </summary>
        private void SyncBindCompList()
        {
            _bindCompList.ClearArray();
            if (_bindDataList == null)
            {
                return;
            }

            for (var i = 0; i < _bindDataList.arraySize; i++)
            {
                var arrayElement = _bindDataList.GetArrayElementAtIndex(i);
                if (arrayElement == null)
                {
                    Debug.LogError($"找不到数组元素 i:{i}");
                    continue;
                }

                var property = arrayElement.FindPropertyRelative("comp");
                if (property == null)
                {
                    Debug.LogError("<BindData>结构找不到字段: comp");
                    return;
                }

                _bindCompList.InsertArrayElementAtIndex(i);
                _bindCompList.GetArrayElementAtIndex(i).objectReferenceValue = property.objectReferenceValue;
            }
        }
    }
}