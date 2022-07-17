using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameData;
using DG.Tweening;

public class RecipeUI : MonoBehaviour
{
    [SerializeField]
    private Image _icon;
    
    [SerializeField]
    private GameObject _requirementPrefab;

    [SerializeField]
    private RectTransform _checkmarkRect;

    [SerializeField]
    private RectTransform _requirementParent;

    [SerializeField]
    LayoutElement _layoutElement;
    
    private Recipe _recipe;
    private Customer _customer;
    private OrderList _parentUI;

    public void SetParentUI(OrderList ui)
    {
        _parentUI = ui;
    }

    public void SetCustomer(Customer customer)
    {
        _recipe = customer.RecipeRequest;
        _checkmarkRect.gameObject.SetActive(false);
        _icon.sprite = _recipe.Icon;

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

    public void SetPlayerValidRecipes(List<Recipe> recList)
    {
        bool showIcon = false;

        foreach (Recipe r in recList)
        {
            if (r == _recipe)
            {
                showIcon = true;
                break;
            }
        }

        _checkmarkRect.gameObject.SetActive(showIcon);
    }

    public void OnCompleted()
    {
        float ht = _layoutElement.preferredHeight;
        _layoutElement.DOPreferredSize(new Vector2(0, ht), 1f).SetEase(Ease.InQuad);
        transform.DOScale(0, 1f).SetEase(Ease.InQuad).OnComplete(() => DestroySelf());
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
        _parentUI.UIRemoved(this);
    }
}
