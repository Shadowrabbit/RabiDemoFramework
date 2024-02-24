// ******************************************************************
//       /\ /|       @file       BorderPreview
//       \ V/        @brief      九宫格范围预览
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2022-12-16 17:42
//    *(__\_\        @Copyright  Copyright (c) 2022, Shadowrabbit
// ******************************************************************

using UnityEngine;
using UnityEngine.UI;

namespace Rabi
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(Image))]
    public class BorderPreview : MonoBehaviour
    {
        private Image _image;
        private RectTransform _rectTransform;

        private void Awake()
        {
            _image = GetComponent<Image>();
            _rectTransform = GetComponent<RectTransform>();
        }

        protected void OnDrawGizmos()
        {
            if (!_rectTransform)
            {
                return;
            }

            if (!_image)
            {
                return;
            }

            var sprite = _image.sprite;
            if (!sprite)
            {
                return;
            }

            //九宫格空隙百分比
            var leftSpacingPercent = sprite.border.x / sprite.rect.width;
            var bottomSpacingPercent = sprite.border.y / sprite.rect.height;
            var rightSpacingPercent = sprite.border.z / sprite.rect.width;
            var topSpacingPercent = sprite.border.w / sprite.rect.height;
            //四条边中点的世界坐标
            var uiTopCenterPos = new Vector3(0, _rectTransform.rect.height * (1 - _rectTransform.pivot.y), 0);
            var uiBottomCenterPos = new Vector3(0, _rectTransform.rect.height * -_rectTransform.pivot.y, 0);
            var uiLeftCenterPos = new Vector3(_rectTransform.rect.width * -_rectTransform.pivot.x, 0, 0);
            var uiRightCenterPos = new Vector3(_rectTransform.rect.width * (1 - _rectTransform.pivot.x), 0, 0);
            var uiLeftTopPos = new Vector3(Mathf.Lerp(uiLeftCenterPos.x, uiRightCenterPos.x, leftSpacingPercent),
                Mathf.Lerp(uiTopCenterPos.y, uiBottomCenterPos.y, topSpacingPercent));
            var uiRightBottomPos = new Vector3(Mathf.Lerp(uiRightCenterPos.x, uiLeftCenterPos.x, rightSpacingPercent),
                Mathf.Lerp(uiBottomCenterPos.y, uiTopCenterPos.y, bottomSpacingPercent));
            Gizmos.color = Color.yellow;
            //分割后的4个定点
            var leftTop = _rectTransform.TransformPoint(uiLeftTopPos);
            var rightBottom = _rectTransform.TransformPoint(uiRightBottomPos);
            Gizmos.DrawLine(leftTop, rightBottom);
        }
    }
}