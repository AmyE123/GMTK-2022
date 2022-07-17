using DG.Tweening;
using GameData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageShelf : PickupHolder
{
    [SerializeField]
    private Transform _meshParent;

    [SerializeField]
    private float _socialDistancing = 1f;

    public void SetShelfWidth(int width)
    {
        _meshParent.localScale = new Vector3(width * _socialDistancing, 1, 1);

        BoxCollider collider = GetComponent<BoxCollider>();
        collider.center = new Vector3(width * _socialDistancing * -0.5f, 1, 0.5f);
        collider.size = new Vector3(width * _socialDistancing, 2, 1);
    }

}
