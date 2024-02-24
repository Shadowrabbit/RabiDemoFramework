// ******************************************************************
//       /\ /|       @file       GrayComp.cs
//       \ V/        @brief      UI灰化组件
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2022-05-18 09:58:19
//    *(__\_\        @Copyright  Copyright (c) 2022, Shadowrabbit
// ******************************************************************

using UnityEngine;

namespace Rabi
{
    public class GrayComp : MonoBehaviour
    {
        [SerializeField] [Range(0, 255)] private int grayScale;
        private UIMaterialEffect _materialEffect;

        public int GrayScale
        {
            get => grayScale;
            set
            {
                value = Mathf.Clamp(value, 0, 255);
                if (grayScale == value) return;
                grayScale = value;
                Refresh();
            }
        }

        private void Awake()
        {
            Refresh();
        }

        private void Update()
        {
            if (_materialEffect.MaterialKey.grayScale != grayScale)
            {
                Refresh();
            }
        }

        private void OnDestroy()
        {
            if (_materialEffect == null) return;
            var key = _materialEffect.MaterialKey;
            key.grayScale = 0;
            _materialEffect.MaterialKey = key;
            _materialEffect.MarkDirty();
        }

        private void Refresh()
        {
            if (_materialEffect == null)
            {
                _materialEffect = this.GetComponent<UIMaterialEffect>();
            }

            var key = _materialEffect.MaterialKey;
            key.grayScale = (byte)grayScale;
            _materialEffect.MaterialKey = key;
            _materialEffect.MarkDirty();
        }

        private void OnDidApplyAnimationProperties()
        {
            Refresh();
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            Refresh();
        }
#endif
    }
}