using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameData;

public class RecipeDice : MonoBehaviour
{
    [SerializeField]
    private Image _pipsImage;

    [SerializeField]
    private Image _bgImage;

    [SerializeField]
    private DiceColorSetting[] _allColors;

    [SerializeField]
    private Sprite[] _pipSprites;

    public void SetColor(DiceColor col)
    {
        _bgImage.color = GetSetting(col).MainMaterial.color;
        _pipsImage.sprite = _pipSprites[0];
    }

    public void SetValue(int value)
    {
        value = Mathf.Clamp(value, 1, 6);
        _bgImage.color = Color.white;
        _pipsImage.sprite = _pipSprites[value];
    }

    public void SetColorAndValue(DiceColor col, int value)
    {
        value = Mathf.Clamp(value, 1, 6);
        _bgImage.color = GetSetting(col).MainMaterial.color;
        _pipsImage.color = GetSetting(col).PipMaterial.color;
        _pipsImage.sprite = _pipSprites[value];
    }

    private DiceColorSetting GetSetting(DiceColor col)
    {
        foreach (DiceColorSetting setting in _allColors)
        {
            if (setting.ColorName == col)
                return setting;
        }

        return null;
    }
}
