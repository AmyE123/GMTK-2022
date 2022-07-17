using System.Collections;
using System.Collections.Generic;
using GameData;
using UnityEngine;
using UnityEngine.UI;

public class FinishedFood : PickupObject
{
    [SerializeField]
    private float _freshDecayRate = 0.1f;

    [SerializeField]
    private float _freshness = 2;

    [SerializeField]
    private Image _image;

    [SerializeField]
    private Recipe _recipe;

    public Recipe Recipe => _recipe;

    public float Freshness => _freshness;

    public void SetRecipe(Recipe rec)
    {
        _recipe = rec;
        _image.sprite = rec.Icon;
    }

    protected override void Update()
    {
        base.Update();
        _freshness -= Time.deltaTime * _freshDecayRate; // doesn't accoutn for paused game
    }
}
