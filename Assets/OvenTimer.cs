using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OvenTimer : W2C
{
    private Oven _oven;

    [SerializeField]
    private CanvasGroup _grp;

    [SerializeField]
    private Image[] _darkerElements;

    [SerializeField]
    private Image[] _midElements;

    [SerializeField]
    private Image[] _lighterElements;

    [SerializeField]
    private Image _fillImage;

    [SerializeField]
    private Color _orangeDark;

    [SerializeField]
    private Color _orangeMid;

    [SerializeField]
    private Color _orangeLight;

    [SerializeField]
    private Color _greenDark;

    [SerializeField]
    private Color _greenMid;

    [SerializeField]
    private Color _greenLight;

    [SerializeField]
    private Image _foodIcon;

    public void Init(Oven oven)
    {
        _oven = oven;
        SetPosition(_oven.transform, new Vector3(0, 2.5f, 0));
    }

    void Update()
    {
        if (_oven.IsEmpty)
        {
            _grp.alpha = 0;
            return;
        }

        _grp.alpha = 1;
        _foodIcon.sprite = _oven.CurrentItem.Icon;

        if (_oven.IsBaking)
        {
            _fillImage.fillAmount = _oven.BakePercent;

            foreach (Image img in _darkerElements)
                img.color = _orangeDark;
            
            foreach (Image img in _midElements)
                img.color = _orangeMid;

            foreach (Image img in _lighterElements)
                img.color = _orangeLight;
        }
        else
        {
            _fillImage.fillAmount = 1;

            foreach (Image img in _darkerElements)
                img.color = _greenDark;
            
            foreach (Image img in _midElements)
                img.color = _greenMid;

            foreach (Image img in _lighterElements)
                img.color = _greenLight;
        }

    }
}
