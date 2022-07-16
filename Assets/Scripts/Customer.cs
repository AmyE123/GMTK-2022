using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameData;

public class Customer : MonoBehaviour
{
    [SerializeField]
    private Recipe _recipe;

    public void SetRequest(Recipe req)
    {
        _recipe = req;
    }
}
