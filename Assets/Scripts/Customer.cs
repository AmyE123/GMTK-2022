using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameData;
using DG.Tweening;

public class Customer : MonoBehaviour
{
    [SerializeField]
    private Recipe _recipe;

    [SerializeField]
    private Transform _handTransform;

    private RecipeUI _ui;

    public Recipe RecipeRequest => _recipe;

    public Transform HandTransform => _handTransform;

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

        Vector3 endPos = transform.position + new Vector3(0, -0.5f, -4);
        transform.DOJump(endPos, 0.2f, 8, 2).SetEase(Ease.Linear).OnComplete(() => Destroy(gameObject));
    }
}
