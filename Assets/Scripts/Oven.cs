using GameData;
using System.Collections.Generic;
using UnityEngine;

public class Oven : MonoBehaviour
{
    private Recipe _currentItem;
    private LevelConfig _levelConfig;
    private float _timeLeft;

    public bool HasItemReady => _currentItem != null && _timeLeft <= 0;

    public bool IsBaking => _currentItem != null && _timeLeft > 0;

    public void Update()
    {
        if (_currentItem != null && _timeLeft > 0)
        {
            _timeLeft -= Time.deltaTime;
            
            if (_timeLeft <= 0)
                OnBakeComplete();
        }
    }

    private void OnBakeComplete()
    {
        Debug.Log($"Finished baking: {_currentItem.name}");
        // play some animation perhaps?
    }

    public Recipe TakeOutItem()
    {
        if (_currentItem == null || _timeLeft > 0)
            return null;

        Recipe item = _currentItem;
        _currentItem = null;
        
        return item;
    }

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
