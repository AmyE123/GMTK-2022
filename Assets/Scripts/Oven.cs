using GameData;
using System.Collections.Generic;
using UnityEngine;

public class Oven : MonoBehaviour
{
    private Recipe _currentItem;
    private LevelConfig _levelConfig;
    private float _timeLeft;

    public bool GiveDice(List<GameDice> diceList)
    {
        if (_currentItem != null)
        {
            Debug.Log("Oven is full!");
            return false;
        }

        List<Recipe> matchingRecipies = GetMatchingRecepies(diceList);

        if (matchingRecipies.Count == 0)
        {
            Debug.Log("No matching recipes");
            return false;
        }

        if (matchingRecipies.Count == 1)
        {
            BeginBaking(matchingRecipies[0]);
        }
        else
        {
            foreach (Customer customer in FindObjectOfType<OutputShelf>().CustomersInOrderTheyCame) // yes I know but gamejam
            {
                bool foundCustomer = false;

                foreach (Recipe rec in matchingRecipies)
                {
                    if (rec == customer.RecipeRequest)
                        foundCustomer = true; 
                }

                if (foundCustomer)
                {
                    BeginBaking(customer.RecipeRequest);
                    break;
                }
            }
        }

        return true;
    }

    private void BeginBaking(Recipe rec)
    {
        Debug.Log($"Started baking: {rec.name}");
        _currentItem = rec;
        _timeLeft = rec.BakeTime;
    }

    public void SetLevelConfig(LevelConfig levelConfig)
    {
        _levelConfig = levelConfig;
    }

    // TODO move this to recipe match methods
    private List<Recipe> GetMatchingRecepies(List<GameDice> diceList)
    {
        List<Recipe> output = new List<Recipe>();

        foreach (Recipe rec in _levelConfig.AllRecipies)
        {
            if (DoesThisRecipeMatch(diceList, rec))
                output.Add(rec);
        }

        return output;
    }

    private bool DoesThisRecipeMatch(List<GameDice> diceList, Recipe rec)
    {
        if (diceList.Count == 0)
            return false;

        DiceRequirement[] basicReq = GetRequirements(rec);

        if (diceList.Count != basicReq.Length)
            return false;

        foreach (DiceRequirement[] reqList in GetPermutations(basicReq))
        {
            bool allMatch = true;

            for (int i=0; i<reqList.Length; i++)
            {
                if (reqList[i].DoesMatch(diceList[i]) == false)
                {
                    allMatch = false;
                    break;
                }
            }

            if (allMatch)
                return true;
        }

        return false;
    }

    private DiceRequirement[] GetRequirements(Recipe rec)
    {
        var result = new List<DiceRequirement>();

        foreach (DiceColor col in rec.Colors)
            result.Add(new DiceRequirement(col));

        foreach (int num in rec.Numbers)
            result.Add(new DiceRequirement(num));

        return result.ToArray();
    }

    // TODO could be ienumerable
    private DiceRequirement[][] GetPermutations(DiceRequirement[] inList)
    {
        var result = new List<DiceRequirement[]>();

        if (inList.Length == 1)
        {
            result.Add(inList);
        }
        if (inList.Length == 2)
        {
            result.Add(new DiceRequirement[] { inList[0], inList[1] });
            result.Add(new DiceRequirement[] { inList[1], inList[0] });
        }
        if (inList.Length == 3)
        {
            result.Add(new DiceRequirement[] { inList[0], inList[1], inList[2] });
            result.Add(new DiceRequirement[] { inList[0], inList[2], inList[1] });
            result.Add(new DiceRequirement[] { inList[1], inList[0], inList[2] });
            result.Add(new DiceRequirement[] { inList[1], inList[2], inList[0] });
            result.Add(new DiceRequirement[] { inList[2], inList[0], inList[1] });
            result.Add(new DiceRequirement[] { inList[2], inList[1], inList[0] });
        }

        return result.ToArray();
    }

    private class DiceRequirement
    {
        private DiceColor _col;
        private bool _hasColor;

        private int _number;
        private bool _hasNumber;

        public DiceRequirement(DiceColor dcol)
        {
            _col = dcol;
            _hasColor = true;
        }

        public DiceRequirement(int nval)
        {
            _number = nval;
            _hasNumber = true;;
        }

        public bool DoesMatch(GameDice dice)
        {
            if (_hasColor && dice.Color != _col)
                return false;

            if (_hasNumber && dice.Number != _number)
                return false;

            return true;
        }
    }
}
