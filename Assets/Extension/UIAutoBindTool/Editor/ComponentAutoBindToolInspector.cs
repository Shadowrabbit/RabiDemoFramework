using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using BindData = ComponentAutoBindTool.BindData;
using System.Reflection;
using System.IO;
using Object = UnityEngine.Object;

[CustomEditor(typeof(ComponentAutoBindTool))]
public class ComponentAutoBindToolInspector : Editor
{
    private ComponentAutoBindTool _target;
    private SerializedProperty _bindDataList;
    private SerializedProperty _bindCompList;
    private readonly List<BindData> _tempBindDataList = new List<BindData>();
    private readonly List<string> _tempFiledNames = new List<string>();
    private readonly List<string> _tempComponentTypeNames = new List<string>();
    private readonly string[] _assemblyNames = {"Assembly-CSharp"};
    private string[] _helperTypeNames;
    private string _helperTypeName;
    private int _helperTypeNameIndex;
    private AutoBindGlobalSetting _setting;
    private SerializedProperty _namespace;
    private SerializedProperty _className;
    private SerializedProperty _codePath;

    private void OnEnable()
    {
        _target = (ComponentAutoBindTool) target;
        _bindDataList = serializedObject.FindProperty("bindDataList");
        _bindCompList = serializedObject.FindProperty("bindCompList");
        _helperTypeNames = GetTypeNames(typeof(IAutoBindRuleHelper), _assemblyNames);
        var paths = AssetDatabase.FindAssets("t:AutoBindGlobalSetting");
        switch (paths.Length)
        {
            case 0:
                Debug.LogError("不存在AutoBindGlobalSetting");
                return;
            case > 1:
                Debug.LogError("AutoBindGlobalSetting数量大于1");
                return;
        }

        var path = AssetDatabase.GUIDToAssetPath(paths[0]);
        _setting = AssetDatabase.LoadAssetAtPath<AutoBindGlobalSetting>(path);
        _namespace = serializedObject.FindProperty("namespace");
        _className = serializedObject.FindProperty("className");
        _codePath = serializedObject.FindProperty("codePath");
        _namespace.stringValue =
            string.IsNullOrEmpty(_namespace.stringValue) ? _setting.Namespace : _namespace.stringValue;
        _className.stringValue = string.IsNullOrEmpty(_className.stringValue)
            ? _target.gameObject.name
            : _className.stringValue;
        _codePath.stringValue = string.IsNullOrEmpty(_codePath.stringValue) ? _setting.CodePath : _codePath.stringValue;
        serializedObject.ApplyModifiedProperties();
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        DrawTopButton();
        DrawHelperSelect();
        DrawSetting();
        DrawKvData();
        serializedObject.ApplyModifiedProperties();
    }

    /// <summary>
    /// 绘制顶部按钮
    /// </summary>
    private void DrawTopButton()
    {
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("排序"))
        {
            Sort();
        }

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
    /// 排序
    /// </summary>
    private void Sort()
    {
        _tempBindDataList.Clear();
        foreach (var data in _target.bindDataList)
        {
            _tempBindDataList.Add(new BindData(data.name, data.comp));
        }

        _tempBindDataList.Sort((x, y) => string.Compare(x.name, y.name, StringComparison.Ordinal));
        _bindDataList.ClearArray();
        foreach (var data in _tempBindDataList)
        {
            AddBindData(data.name, data.comp);
        }

        SyncBindComs();
    }

    /// <summary>
    /// 全部删除
    /// </summary>
    private void RemoveAll()
    {
        _bindDataList.ClearArray();
        SyncBindComs();
    }

    /// <summary>
    /// 删除空引用
    /// </summary>
    private void RemoveNull()
    {
        for (var i = _bindDataList.arraySize - 1; i >= 0; i--)
        {
            var element = _bindDataList.GetArrayElementAtIndex(i).FindPropertyRelative("comp");
            if (element.objectReferenceValue == null)
            {
                _bindDataList.DeleteArrayElementAtIndex(i);
            }
        }

        SyncBindComs();
    }

    /// <summary>
    /// 自动绑定组件
    /// </summary>
    private void AutoBindComponent()
    {
        _bindDataList.ClearArray();
        var childs = _target.gameObject.GetComponentsInChildren<Transform>(true);
        foreach (var child in childs)
        {
            _tempFiledNames.Clear();
            _tempComponentTypeNames.Clear();
            _target.RuleHelper.IsValidBind(child, _tempFiledNames, _tempComponentTypeNames);
            for (var i = 0; i < _tempFiledNames.Count; i++)
            {
                var com = child.GetComponent(_tempComponentTypeNames[i]);
                if (com == null)
                {
                    Debug.LogError($"{child.name}上不存在{_tempComponentTypeNames[i]}的组件");
                    break;
                }

                AddBindData(_tempFiledNames[i], child.GetComponent(_tempComponentTypeNames[i]));
            }
        }

        SyncBindComs();
    }

    /// <summary>
    /// 绘制辅助器选择框
    /// </summary>
    private void DrawHelperSelect()
    {
        _helperTypeName = _helperTypeNames[0];
        if (_target.RuleHelper != null)
        {
            _helperTypeName = _target.RuleHelper.GetType().Name;
            for (var i = 0; i < _helperTypeNames.Length; i++)
            {
                if (_helperTypeName == _helperTypeNames[i])
                {
                    _helperTypeNameIndex = i;
                }
            }
        }
        else
        {
            var helper = (IAutoBindRuleHelper) CreateHelperInstance(_helperTypeName, _assemblyNames);
            _target.RuleHelper = helper;
        }

        foreach (var go in Selection.gameObjects)
        {
            var autoBindTool = go.GetComponent<ComponentAutoBindTool>();
            if (autoBindTool.RuleHelper != null) continue;
            var helper = (IAutoBindRuleHelper) CreateHelperInstance(_helperTypeName, _assemblyNames);
            autoBindTool.RuleHelper = helper;
        }

        var selectedIndex = EditorGUILayout.Popup("AutoBindRuleHelper", _helperTypeNameIndex, _helperTypeNames);
        if (selectedIndex == _helperTypeNameIndex) return;
        {
            _helperTypeNameIndex = selectedIndex;
            _helperTypeName = _helperTypeNames[selectedIndex];
            var helper = (IAutoBindRuleHelper) CreateHelperInstance(_helperTypeName, _assemblyNames);
            _target.RuleHelper = helper;
        }
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
    private void DrawKvData()
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
            SyncBindComs();
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
    /// 同步绑定数据
    /// </summary>
    private void SyncBindComs()
    {
        _bindCompList.ClearArray();

        for (var i = 0; i < _bindDataList.arraySize; i++)
        {
            var property = _bindDataList.GetArrayElementAtIndex(i).FindPropertyRelative("comp");
            _bindCompList.InsertArrayElementAtIndex(i);
            _bindCompList.GetArrayElementAtIndex(i).objectReferenceValue = property.objectReferenceValue;
        }
    }

    /// <summary>
    /// 获取指定基类在指定程序集中的所有子类名称
    /// </summary>
    private static string[] GetTypeNames(Type typeBase, string[] assemblyNames)
    {
        var typeNames = new List<string>();
        foreach (var assemblyName in assemblyNames)
        {
            Assembly assembly;
            try
            {
                assembly = Assembly.Load(assemblyName);
            }
            catch
            {
                continue;
            }

            if (assembly == null)
            {
                continue;
            }

            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                if (type.IsClass && !type.IsAbstract && typeBase.IsAssignableFrom(type))
                {
                    typeNames.Add(type.FullName);
                }
            }
        }

        typeNames.Sort();
        return typeNames.ToArray();
    }

    /// <summary>
    /// 创建辅助器实例
    /// </summary>
    private static object CreateHelperInstance(string helperTypeName, IEnumerable<string> assemblyNames)
    {
        foreach (var assemblyName in assemblyNames)
        {
            var assembly = Assembly.Load(assemblyName);
            var instance = assembly.CreateInstance(helperTypeName);
            if (instance != null)
            {
                return instance;
            }
        }

        return null;
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
            //获取autoBindTool上的Component
            sw.WriteLine($"\t\t\tvar autoBindTool = go.GetComponent<ComponentAutoBindTool>();");
            //根据索引获取
            for (var i = 0; i < _target.bindDataList.Count; i++)
            {
                var data = _target.bindDataList[i];
                var filedName = $"{data.name}";
                sw.WriteLine($"\t\t\t{filedName} = autoBindTool.GetBindComponent<{data.comp.GetType().Name}>({i});");
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
}