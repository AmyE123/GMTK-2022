using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameData
{
    [CreateAssetMenu(menuName="Data/Level")]
    public class LevelConfig : ScriptableObject
    {
        [SerializeField, TextArea(minLines:1, maxLines:10)]
        private string _description;

        [SerializeField]
        private ColorOdds[] _potentialColors;

        [SerializeField]
        private NumberOdds[] _potentialNumbers;

        [SerializeField]
        private RecipeOdds[] _potentialRecipes;

        [SerializeField]
        private int _maxDice;

        [SerializeField]
        private int _diceSpawnTime;

        [SerializeField]
        private int _ovenCount;

        [SerializeField]
        private int _shelfSpace;

        public string Description => _description;
        public int DiceSpawnTime => _diceSpawnTime;
        public int MaxDice => _maxDice;
        public int OvenCount => _ovenCount;
        public int ShelfSpace => _shelfSpace;

        public DiceColor GetRandomColor() => GetRandomValue(_potentialColors);
        public Recipe GetRandomReceipe() => GetRandomValue(_potentialRecipes);
        public int GetRandomNumber() => GetRandomValue(_potentialNumbers);

        private T GetRandomValue<T>(GenericOdds<T>[] oddsList)
        {
            int maxValue = 0;

            foreach (GenericOdds<T> odds in oddsList)
                maxValue += odds.Weight;

            int randValue = Random.Range(0, maxValue);

            foreach (GenericOdds<T> odds in oddsList)
            {
                if (randValue < odds.Weight)
                    return odds.Value;

                randValue -= odds.Weight;
            }

            return default;
        }
    }

    [System.Serializable]
    public class GenericOdds<T>
    {
        [SerializeField]
        private T _value;

        [SerializeField]
        private int _weight;

        public T Value => _value;
        public int Weight => _weight;
    }

    [System.Serializable]
    public class ColorOdds : GenericOdds<DiceColor> {}

    [System.Serializable]
    public class NumberOdds : GenericOdds<int> {}

    [System.Serializable]
    public class RecipeOdds : GenericOdds<Recipe> {}

}