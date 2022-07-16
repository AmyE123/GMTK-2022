using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameData
{
    [CreateAssetMenu(menuName="Data/Recipe")]
    public class Recipe : ScriptableObject
    {
        [SerializeField]
        private DiceColor[] _requiredColors;

        [SerializeField, Range(1,6)]
        private int[] _requiredNumbers = new int[] { 1 };

        [SerializeField]
        private int _bakeTime;

        public int BakeTime => _bakeTime;

        public IEnumerable<DiceColor> Colors => _requiredColors;

        public IEnumerable<int> Numbers => _requiredNumbers;
    }
}