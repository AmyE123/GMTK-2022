using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickupObjectDetection : MonoBehaviour
{
    private bool _IsFacingDice => _inRangePickups.Count > 0;
    private bool _IsFacingShelf => _inRangeShelves.Count > 0;

    [SerializeField] private List<PickupObject> _inRangePickups;
    [SerializeField] private List<ObjectShelf> _inRangeShelves;

    [SerializeField] private List<PickupObject> _pickedUpObjects;
    [SerializeField] private List<Transform> _pickupPoints;

    private int _NumberOfPickedUpDice => _pickedUpObjects.Count;
    private PickupObject _ClosestPickup => GetClosest(_inRangePickups);
    private ObjectShelf _ClosestShelf => GetClosest(_inRangeShelves);

    [SerializeField]
    private string _contextualAction = "???";
    [SerializeField]
    private Transform _contextualTarget;

    public string ContextualAction => _contextualAction;
    public Transform ContextualTarget => _contextualTarget;

    // Generic function that can be used on any type of GameObject
    private T GetClosest<T>(List<T> inList) where T: MonoBehaviour
    {
        float bestDist = 9999;
        T closest = null;

        foreach (T obj in inList)
        {
            float thisDist = Vector3.Distance(obj.transform.position, transform.position);

            if (thisDist > bestDist)
                continue;

            bestDist = thisDist;
            closest = obj;
        }

        return closest;
    }

    private void Update()
    {
        bool isHoldingStuff = _NumberOfPickedUpDice > 0;
        bool areHandsFull = _NumberOfPickedUpDice >= 3;

        if (_IsFacingDice && areHandsFull == false)
        {
            _contextualAction = "pick up";
            _contextualTarget = _ClosestPickup.transform; // yes this is wasteful but gamejam

            if (Input.GetKeyDown(KeyCode.E))
            {             
                PickUpObject(_ClosestPickup);         
            }
        }
        else if (_IsFacingShelf && isHoldingStuff)
        {
            PickupObject topHeldItem = _pickedUpObjects[_NumberOfPickedUpDice - 1];

            _contextualAction = "put down";
            _contextualTarget = topHeldItem.transform; // yes this is wasteful but gamejam

            if (Input.GetKeyDown(KeyCode.E))
            {
                PutDownObject(topHeldItem);  
            }
        }
        else
        {
            _contextualAction = "";
            _contextualTarget = null;
        }


    }

    private void PickUpObject(PickupObject obj)
    {
        var pickupTransform = _pickupPoints[_NumberOfPickedUpDice].transform;
        _pickedUpObjects.Add(obj);
        obj.GetPickedUp();
        obj.SetToFollow(pickupTransform);
        obj.GetComponent<BoxCollider>().enabled = false;
        _inRangePickups.Remove(obj);
    }

    private void PutDownObject(PickupObject obj)
    {        
        bool didPutDown = _ClosestShelf.PutDownObject(obj);
        if (didPutDown)
        {
            _pickedUpObjects.Remove(obj);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Pickup")
        {
            _inRangePickups.Add(other.GetComponent<PickupObject>());
        }

        if (other.transform.tag == "Shelf")
        {
            _inRangeShelves.Add(other.GetComponent<ObjectShelf>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Pickup")
        {
            _inRangePickups.Remove(other.GetComponent<PickupObject>());
        }

        if (other.transform.tag == "Shelf")
        {
            _inRangeShelves.Remove(other.GetComponent<ObjectShelf>());
        }
    }
}
