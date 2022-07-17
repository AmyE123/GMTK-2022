using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameData;

public class OrderChecker
{
    // TODO move this to recipe match methods
    public static List<Recipe> GetMatchingRecepies(LevelConfig level, List<GameDice> diceList)
    {
        List<Recipe> output = new List<Recipe>();

        foreach (Recipe rec in level.AllRecipies)
        {
            if (DoesThisRecipeMatch(diceList, rec))
                output.Add(rec);
        }

        return output;
    }

    private static bool DoesThisRecipeMatch(List<GameDice> diceList, Recipe rec)
    {
        if (diceList.Count == 0)
            return false;

        DiceRequirement[] basicReq = GetRequirements(rec);

        if (diceList.Count != basicReq.Length)
            return false;

        /* Too complicated for the player I think
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
        */

        bool allMatch = true;

        for (int i=0; i<basicReq.Length; i++)
        {
            if (basicReq[i].DoesMatch(diceList[i]) == false)
            {
                allMatch = false;
                break;
            }
        }

        return allMatch;
    }

    private static DiceRequirement[] GetRequirements(Recipe rec)
    {
        var result = new List<DiceRequirement>();

        foreach (Recipe.DiceColorNumberCombo combo in rec.SpecificDice)
            result.Add(new DiceRequirement(combo));

        foreach (DiceColor col in rec.Colors)
            result.Add(new DiceRequirement(col));

        foreach (int num in rec.Numbers)
            result.Add(new DiceRequirement(num));

        return result.ToArray();
    }

    private static DiceRequirement[][] GetPermutations(DiceRequirement[] inList)
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

        public DiceRequirement(Recipe.DiceColorNumberCombo combo)
        {
            _hasNumber = _hasColor = true;
            _col = combo.color;
            _number = combo.number;
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
