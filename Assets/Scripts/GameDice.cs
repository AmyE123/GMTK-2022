using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameData;
using DG.Tweening;

public class GameDice : PickupObject
{
    [SerializeField]
    private Vector3[] _rotations;

    [SerializeField]
    private MeshRenderer _mesh;

    [SerializeField]
    private float _hopTime;

    [SerializeField]
    private float _hopHeight;

    [SerializeField]
    private float _vanishTime;

    [SerializeField]
    private DiceColorSetting[] _allColors;

    public bool CanBePickedUp => _isDestroying == false;

    private DiceColor _color;
    private int _currentNum = 1;
    private bool _isDestroying;

    public int Number => _currentNum;
    public DiceColor Color => _color;

    protected override void Update()
    {
        base.Update();
    }


    public void OnMouseDown()
    {
        Destroy(gameObject);
    }

    public bool Decay()
    {
        if (_currentNum == 1)
        {
            BlinkOutOfExistance();
            return true;
        }

        SetNumberHop(_currentNum - 1);
        return false;
    }
    
    private void BlinkOutOfExistance()
    {
        _isDestroying = true;
        transform.DOScale(0, _vanishTime).SetEase(Ease.InBack).OnComplete(() => Destroy(gameObject));
    }

    public void SetNumberHop(int num)
    {
        _currentNum = num;
        transform.DOJump(transform.position, _hopHeight, 1, _hopTime);
        transform.DOLocalRotate(_rotations[num-1], _hopTime);
    }

    public void SetNumber(int num)
    {
        _currentNum = num;
        Quaternion targetRotation = Quaternion.Euler(_rotations[num-1]);
        transform.localRotation = targetRotation;
    }

    public void SetColorAndValue(DiceColor col, int val)
    {
        _color = col;
        SetNumber(val);

        foreach (DiceColorSetting setting in _allColors)
        {
            if (setting.ColorName != col)
                continue;

            _mesh.sharedMaterials = new Material[] { setting.MainMaterial, setting.PipMaterial };
            break;
        }
    }
}
