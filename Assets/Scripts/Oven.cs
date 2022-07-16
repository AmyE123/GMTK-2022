using GameData;
using System.Collections.Generic;
using UnityEngine;

public class Oven : MonoBehaviour
{
    public bool IsCompatible;
    public bool IsOccupied;

    private List<Recipe> _levelRecipes;

    public bool GiveDice(List<GameDice> DiceList)
    {
        return false;
    }

    public void SetLevelConfig(LevelConfig levelConfig)
    {
        foreach (Recipe recipe in levelConfig.AllRecipies)
        {
            _levelRecipes.Add(recipe);
        }
    }
}
