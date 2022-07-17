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
    private float _freshness = 3;

    [SerializeField]
    private Image _image;

    public void SetRecipe(Recipe rec)
    {
        _image.sprite = rec.Icon;
    }

    protected override void Update()
    {
        base.Update();
    }
}
