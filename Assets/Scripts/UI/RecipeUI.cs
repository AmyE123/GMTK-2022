using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameData;

public class RecipeUI : MonoBehaviour
{
    [SerializeField]
    private Image _icon;
    
    [SerializeField]
    private GameObject _requirementPrefab;

    [SerializeField]
    private RectTransform _requirementParent;
    
    private Recipe _recipe;
    private Customer _customer;

    public void SetCustomer(Customer customer)
    {
        _recipe = customer.RecipeRequest;

        foreach (DiceColor col in _recipe.Colors)
        {
            GameObject newObj = Instantiate(_requirementPrefab, _requirementParent);
            newObj.GetComponent<RecipeDice>().SetColor(col);
        }

        foreach (int numberValue in _recipe.Numbers)
        {
            GameObject newObj = Instantiate(_requirementPrefab, _requirementParent);
            newObj.GetComponent<RecipeDice>().SetValue(numberValue);
        }
    }

    public void OnCompleted()
    {

    }
}
