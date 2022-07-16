using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameData;

public class OrderList : MonoBehaviour
{
    [SerializeField]
    private GameObject _iconPrefab;

    [SerializeField]
    private RectTransform _parentRect;

    public RecipeUI AddOrder(Customer customer)
    {
        GameObject newObj = Instantiate(_iconPrefab, _parentRect);
        RecipeUI newUI = newObj.GetComponent<RecipeUI>();
        newUI.SetCustomer(customer);
        
        return newUI;
    }
}
