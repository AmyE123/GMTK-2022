using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameData;
using DG.Tweening;

public class GameDice : MonoBehaviour
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
    private DiceColorSetting[] _allColors;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SetNumberHop(1);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SetNumberHop(2);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SetNumberHop(3);
        if (Input.GetKeyDown(KeyCode.Alpha4)) SetNumberHop(4);
        if (Input.GetKeyDown(KeyCode.Alpha5)) SetNumberHop(5);
        if (Input.GetKeyDown(KeyCode.Alpha6)) SetNumberHop(6);
    }

    public void OnMouseDown()
    {
        Destroy(gameObject);
    }

    public void SetNumberHop(int num)
    {
        transform.DOJump(transform.position, _hopHeight, 1, _hopTime);
        transform.DORotate(_rotations[num-1], _hopTime);
    }

    public void SetNumber(int num)
    {
        Quaternion targetRotation = Quaternion.Euler(_rotations[num-1]);
        transform.localRotation = targetRotation;
    }

    public void SetColorAndValue(DiceColor col, int val)
    {
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
