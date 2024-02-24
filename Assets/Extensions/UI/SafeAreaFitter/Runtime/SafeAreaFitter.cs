// ******************************************************************
//       /\ /|       @file       SafeAreaFitter
//       \ V/        @brief      异形屏适配
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2023-11-13 17:23
//    *(__\_\        @Copyright  Copyright (c) 2023, Shadowrabbit
// ******************************************************************

using System;
using UnityEngine;

public class SafeAreaFitter : MonoBehaviour
{
    public bool isHorizontal; //横向适配
    private float _width; //屏幕宽
    private float _height; //屏幕高
    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>() ?? throw new Exception("该组件需要用在UI上");
#if UNITY_EDITOR
        if (Screen.width < Screen.safeArea.width || Screen.height < Screen.safeArea.height)
        {
            _width = Screen.currentResolution.width;
            _height = Screen.currentResolution.height;
        }
        else
#endif
        {
            _width = Screen.width;
            _height = Screen.height;
        }

        var safeArea = Screen.safeArea;
        if (isHorizontal)
        {
            safeArea.y = 0;
            safeArea.height = _height;
        }
        else
        {
            safeArea.x = 0;
            safeArea.width = _width;
        }

        var anchorMin = safeArea.position;
        var anchorMax = safeArea.position + safeArea.size;
        anchorMin.x /= _width;
        anchorMin.y /= _height;
        anchorMax.x /= _width;
        anchorMax.y /= _height;
        _rectTransform.anchorMin = anchorMin;
        _rectTransform.anchorMax = anchorMax;
        _rectTransform.offsetMax = Vector3.zero;
        _rectTransform.offsetMin = Vector3.zero;
    }
}