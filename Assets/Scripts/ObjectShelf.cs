using System.Collections.Generic;
using UnityEngine;

public class ObjectShelf : PickupHolder
{
    [SerializeField]
    private Transform[] _putDownPoints;

    [SerializeField]
    private PickupObject[] _putDownObjects;

    public Transform[] PutDownPoints => _putDownPoints;

    private void Start()
    {
        //_putDownObjects = new PickupObject[_putDownPoints.Count];
    }

    public void SetWidth(int width)
    {
        transform.localScale = new Vector3(width, 1, 1);
        _putDownObjects = new PickupObject[width];
        _putDownPoints = new Transform[width];

        for (int i = 0; i < width; i++)
        {
            GameObject newObj = new GameObject("PutDownPoint");
            newObj.transform.SetParent(transform);
            Vector3 startPos = transform.position + new Vector3((width - 1) * -0.5f, 0, 0);
            newObj.transform.position = new Vector3(i, 1, 0) + startPos;
            PutDownPoints[i] = newObj.transform;
        }
    }

    public override void RemovePickup(PickupObject obj)
    {
        for (int i=0; i<_putDownObjects.Length; i++)
        {
            if (obj == _putDownObjects[i])
            {
                _putDownObjects[i] = null;
                break;
            }
        }
    }

    public bool PutDownObject(PickupObject obj)
    {
        for (int i=0; i<_putDownPoints.Length; i++)
        {
            bool isSpaceFree = _putDownObjects[i] == null;

            if (isSpaceFree == false)
                continue;

            obj.SetToFollow(_putDownPoints[i]);
            obj.GetComponent<BoxCollider>().enabled = true;
            _putDownObjects[i] = obj;
            obj.SetHolder(this);
            return true;
        }

        return false;
    }
}
