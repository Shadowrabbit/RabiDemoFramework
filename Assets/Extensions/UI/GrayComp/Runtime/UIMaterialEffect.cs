// ******************************************************************
//       /\ /|       @file       UIMaterialEffect.cs
//       \ V/        @brief      
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2022-05-17 11:17:25
//    *(__\_\        @Copyright  Copyright (c) 2022, Shadowrabbit
// ******************************************************************

using UnityEngine;
using UnityEngine.UI;

namespace Rabi
{
    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Graphic))]
    public class UIMaterialEffect : MonoBehaviour, IMaterialModifier
    {
        private Graphic _graphic;
        private UIEffectMaterialKey _materialKey;
        private Material _material;

        public Material GetModifiedMaterial(Material baseMaterial)
        {
            var usedMaterial = baseMaterial;
            if (enabled)
            {
                if (_material == null)
                {
                    _material = UIEffectMaterials.Get(_materialKey);
                }

                if (_material)
                {
                    usedMaterial = _material;
                }
            }

            var maskable = _graphic as MaskableGraphic;
            return maskable != null ? maskable.GetModifiedMaterial(usedMaterial) : usedMaterial;
        }

        internal UIEffectMaterialKey MaterialKey
        {
            get => _materialKey;
            set
            {
                if (_materialKey.Equals(value)) return;
                _materialKey = value;
                if (_material == null) return;
                UIEffectMaterials.Free(_material);
                _material = null;
            }
        }

        internal void MarkDirty()
        {
            if (_graphic != null)
            {
                _graphic.SetMaterialDirty();
            }
        }

        private void Awake()
        {
            _graphic = GetComponent<Graphic>();
        }

        private void OnEnable()
        {
            MarkDirty();
        }

        private void OnDisable()
        {
            MarkDirty();
        }

        private void OnDestroy()
        {
            if (_material == null) return;
            UIEffectMaterials.Free(_material);
            _material = null;
        }
    }
}