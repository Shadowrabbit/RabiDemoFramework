using UnityEngine;
using UnityEditor;

/// <summary>
/// 自动绑定全局设置
/// </summary>
public class AutoBindGlobalSetting : ScriptableObject
{
    [SerializeField] private string codePath;
    [SerializeField] private string @namespace;

    public string CodePath => codePath;

    public string Namespace => @namespace;

    [MenuItem("Rabi/CreateAutoBindGlobalSetting")]
    private static void CreateAutoBindGlobalSetting()
    {
        var paths = AssetDatabase.FindAssets("t:AutoBindGlobalSetting");
        if (paths.Length >= 1)
        {
            var path = AssetDatabase.GUIDToAssetPath(paths[0]);
            EditorUtility.DisplayDialog("警告", $"已存在AutoBindGlobalSetting，路径:{path}", "确认");
            return;
        }

        var setting = CreateInstance<AutoBindGlobalSetting>();
        AssetDatabase.CreateAsset(setting, "Assets/AutoBindGlobalSetting.asset");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}