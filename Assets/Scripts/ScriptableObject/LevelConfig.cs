using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameData
{
    [CreateAssetMenu(menuName="Data/Level")]
    public class LevelConfig : ScriptableObject
    {
        [SerializeField]
        private string _levelName = "day ???";

        [SerializeField, TextArea(minLines:1, maxLines:10)]
        private string _description;

        [SerializeField]
        private ColorOdds[] _potentialColors;

        [SerializeField]
        private NumberOdds[] _potentialNumbers;

        [SerializeField]
        private RecipeOdds[] _potentialRecipes;

        [Header("The input dice shelf")]
        [SerializeField]
        private int _maxDice;

        [SerializeField]
        private int _diceSpawnTime;

        [Header("People coming in to make orders")]
        [SerializeField]
        private int _orderSpawnTime;

        [SerializeField]
        private int _maxOrders;

        [Header("Oven information")]
        [SerializeField]
        private int _ovenCount;

        [Header("For temporary storage")]
        [SerializeField]
        private int _shelfSpace;

        [Header("Win/Lose conditions")]
        [SerializeField]
        private int _totalLevelTime;

        [SerializeField]
        private int _happinessRequirement;

        [Header("Tutorial Stuff")]
        [SerializeField]
        private Recipe.DiceColorNumberCombo[] _tutorialDiceQueue;

        [SerializeField]
        private Recipe[] _tutorialRequestQueue;

        public string LevelName => _levelName;
        public string Description => _description;
        public int RefillTime => _diceSpawnTime;
        public int MaxDice => _maxDice;
        public int OvenCount => _ovenCount;
        public int ShelfSpace => _shelfSpace;
        public int TotalLevelTime => _totalLevelTime;
        public int HappinessRequirement => _happinessRequirement;
        public int OrderSpawnTime => _orderSpawnTime;
        public int MaxOrders => _maxOrders;

        public IEnumerable<Recipe.DiceColorNumberCombo> TutorialDiceQueue => _tutorialDiceQueue;
        public IEnumerable<Recipe> TutorialRequestQueue => _tutorialRequestQueue;

        public IEnumerable<Recipe> AllRecipies
        {
            get
            {
                foreach (RecipeOdds odds in _potentialRecipes)
                {
                    yield return odds.Value;
                }
            }
        }

        public DiceColor GetRandomColor() => GetRandomValue(_potentialColors);
        public Recipe GetRandomRecipe() => GetRandomValue(_potentialRecipes);
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