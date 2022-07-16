using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameData;
using DG.Tweening;

public class OutputShelf : MonoBehaviour
{
    [SerializeField]
    private Transform _customerParent;

    [SerializeField]
    private GameObject _customerPrefab;

    [SerializeField]
    private Transform _meshParent;

    [SerializeField]
    private Customer[] _customerSlots;

    [SerializeField]
    private float _spawnInTime;

    [SerializeField]
    private float _socialDistancing = 1f;

    [SerializeField]
    private OrderList _orderList;

    private int ShelfWidth => _customerSlots == null ? 0 : _customerSlots.Length;

    public void SetShelfWidth(int width)
    {
        _meshParent.localScale = new Vector3(width * _socialDistancing, 1, 1);
        _customerSlots = new Customer[width];

        BoxCollider collider = GetComponent<BoxCollider>();
        collider.center = new Vector3(width * _socialDistancing * -0.5f, 1, 0.5f);
        collider.size = new Vector3(width * _socialDistancing, 2, 1);
    }

    public bool SpawnRandomNewCustomer(LevelConfig levelData)
    {
        for (int i=0; i<ShelfWidth; i++)
        {
            if (_customerSlots[i] != null)
                continue;
            
            Recipe product = levelData.GetRandomRecipe();
            SpawnSingleCustomer(product, i);
            return true;
        }

        return false;
    }

    private void SpawnSingleCustomer(Recipe productRequest, int slotNum)
    {
        GameObject newObj = Instantiate(_customerPrefab, _customerParent);
        Vector3 finalPos = new Vector3(-slotNum * _socialDistancing, 0, 0);

        Customer customer = newObj.GetComponent<Customer>();
        customer.SetRequest(productRequest);
        
        RecipeUI newUI = _orderList.AddOrder(customer);
        customer.SetUI(newUI);

        _customerSlots[slotNum] = customer;

        customer.transform.localPosition = finalPos + new Vector3(0, -3, 0);
        customer.transform.DOLocalMove(finalPos, _spawnInTime).SetEase(Ease.OutBack);
    }
}
