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

        List<Recipe> matchingRecipies = OrderChecker.GetMatchingRecepies(_levelConfig, diceList);

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
}
