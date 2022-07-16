using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameData
{
    [CreateAssetMenu(menuName="Data/Dice Color Setting")]
    public class DiceColorSetting : ScriptableObject
    {
        [SerializeField]
        private DiceColor _colorName;

        [SerializeField]
        private Material _mainMaterial;

        [SerializeField]
        private Material _pipMaterial;

        public DiceColor ColorName => _colorName;
        public Material MainMaterial => _mainMaterial;
        public Material PipMaterial => _pipMaterial;
    }
}