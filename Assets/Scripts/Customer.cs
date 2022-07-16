using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameData;

public class Customer : MonoBehaviour
{
    [SerializeField]
    private Recipe _recipe;

    private RecipeUI _ui;

    public Recipe RecipeRequest => _recipe;

    public void SetUI(RecipeUI ui)
    {
        _ui = ui;
    }

    public void SetRequest(Recipe req)
    {
        _recipe = req;
    }

    public void TakeCompletedGoods()
    {
        _ui?.OnCompleted();
    }
}
