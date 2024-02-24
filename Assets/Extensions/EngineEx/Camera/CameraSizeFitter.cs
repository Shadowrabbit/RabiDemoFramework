// ******************************************************************
//       /\ /|       @file       CameraSizeFitter
//       \ V/        @brief      2D相机全屏适配
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2023-10-01 16:54
//    *(__\_\        @Copyright  Copyright (c) 2023, Shadowrabbit
// ******************************************************************

using Sirenix.OdinInspector;
using UnityEngine;

namespace Rabi
{
    public class CameraSizeFitter : MonoBehaviour
    {
        [SerializeField] private float effectiveWidth = 19.2f; //有效区域的宽度
        [SerializeField] private float effectiveHeight = 10.8f; //有效区域的高度
        private Camera _camera;

        private void Awake()
        {
            Init();
            AutoFit();
        }

        private void Init()
        {
            _camera = GetComponent<Camera>();
            if (_camera)
            {
                return;
            }

            Debug.LogError("CameraSizeFitter需要一个Camera组件");
        }

        private void AutoFit()
        {
            var effectiveAspectRatio = effectiveWidth / effectiveHeight;
#if UNITY_EDITOR
            var vectorGameViewSize = UnityEditor.Handles.GetMainGameViewSize();
            var screenAspectRatio = vectorGameViewSize.x / vectorGameViewSize.y;
#else
            var screenAspectRatio = (float) Screen.width / Screen.height;
#endif
            //实际屏幕宽高比>有效区域宽高比的情况 对相机高度进行适配
            if (screenAspectRatio >= effectiveAspectRatio)
            {
                _camera.orthographicSize = effectiveHeight / 2;
                return;
            }

            //实际屏幕宽高比<有效区域宽高比的情况 对相机宽度进行适配
            var cameraHeight = effectiveWidth / screenAspectRatio;
            _camera.orthographicSize = cameraHeight / 2;
        }

        [Button("适配")]
        private void TestAutoFit()
        {
            Init();
            AutoFit();
        }
    }
}