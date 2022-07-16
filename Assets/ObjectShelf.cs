using System.Collections.Generic;
using UnityEngine;

public class ObjectShelf : MonoBehaviour
{
    [SerializeField]
    private List<Transform> _putDownPoints;

    [SerializeField]
    private PickupObject[] _putDownObjects;

    public List<Transform> PutDownPoints => _putDownPoints;

    private void Start()
    {
        _putDownObjects = new PickupObject[_putDownPoints.Count];
    }

    public bool PutDownObject(PickupObject obj)
    {
        for (int i=0; i<_putDownPoints.Count; i++)
        {
            bool isSpaceFree = _putDownObjects[i] == null;

            if (isSpaceFree == false)
                continue;

            obj.SetToFollow(_putDownPoints[i]);
            obj.GetComponent<BoxCollider>().enabled = true;
            _putDownObjects[i] = obj;
            return true;
        }

        return false;
    }
}
