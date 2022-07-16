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

    public void SetRecipe(Recipe recipe)
    {
        _recipe = recipe;

        foreach (DiceColor col in recipe.Colors)
        {
            GameObject newObj = Instantiate(_requirementPrefab, _requirementParent);
            newObj.GetComponent<RecipeDice>().SetColor(col);
        }

        foreach (int numberValue in recipe.Numbers)
        {
            GameObject newObj = Instantiate(_requirementPrefab, _requirementParent);
            newObj.GetComponent<RecipeDice>().SetValue(numberValue);
        }
    }
}
