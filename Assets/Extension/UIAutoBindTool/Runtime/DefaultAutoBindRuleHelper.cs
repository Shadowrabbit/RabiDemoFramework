using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 默认自动绑定规则辅助器
/// </summary>
public class DefaultAutoBindRuleHelper : IAutoBindRuleHelper
{
    /// <summary>
    /// 命名前缀与类型的映射
    /// </summary>
    private readonly Dictionary<string, string> _prefixesDict = new Dictionary<string, string>()
    {
        {"_rectTrans", "RectTransform"},
        {"_btn", "Button"},
        {"_img", "Image"},
        {"_rImg", "RawImage"},
        {"_txt", "Text"},
    };

    public void IsValidBind(Transform target, List<string> filedNames, List<string> componentTypeNames)
    {
        var nodeName = target.name;
        foreach (var compPrefix in _prefixesDict.Keys.Where(compPrefix => nodeName.StartsWith(compPrefix)))
        {
            filedNames.Add(nodeName);
            componentTypeNames.Add(_prefixesDict[compPrefix]);
        }
    }
}