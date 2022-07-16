using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickupObjectDetection : MonoBehaviour
{
    [SerializeField] private bool _isWithinRange = false;
    [SerializeField] private bool _isNearShelf = false;

    [SerializeField] private List<PickupObject> _objectTransforms;
    [SerializeField] private List<PickupObject> _pickedUpDice;
    [SerializeField] private List<Transform> _pickupPoints;
    [SerializeField] private int _numberOfPickedUpDice;
    [SerializeField] private int _shelfIdx;

    [SerializeField] private ObjectShelf _objectShelf;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_isWithinRange && _numberOfPickedUpDice < 3 && !_isNearShelf)
            {                
                PickUpObject(_objectTransforms[0].transform);
                _objectTransforms[0].IsPickedup = true;               
                _objectTransforms.RemoveAt(0);
                _numberOfPickedUpDice ++;
            }

            if (_isNearShelf)
            {
                PutDownObject(_pickedUpDice[_numberOfPickedUpDice - 1].transform);
                _numberOfPickedUpDice--;               
            }
        }
    }

    private void PickUpObject(Transform obj)
    {
        _pickedUpDice.Add(_objectTransforms[0]);
        var pickupTransform = _pickupPoints[_numberOfPickedUpDice].transform;
        obj.parent = pickupTransform;
        obj.transform.position = pickupTransform.position;
        obj.transform.rotation = pickupTransform.rotation;
        obj.GetComponent<BoxCollider>().enabled = false;
    }

    private void PutDownObject(Transform obj)
    {        
        _pickedUpDice.Remove(_pickedUpDice[_numberOfPickedUpDice - 1]);
        var putDownTransform = _objectShelf.PutDownPoints[_shelfIdx].transform;
        obj.parent = putDownTransform;
        obj.transform.position = putDownTransform.position;
        obj.transform.rotation = putDownTransform.rotation;
        obj.GetComponent<BoxCollider>().enabled = false;
        _shelfIdx++;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Pickup")
        {
            _isWithinRange = true;
            _objectTransforms.Add(other.GetComponent<PickupObject>());
        }

        if (other.transform.tag == "Shelf")
        {
            _isNearShelf = true;
            _objectTransforms.Add(other.GetComponent<PickupObject>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Pickup")
        {
            _isWithinRange = false;
            _objectTransforms.Remove(other.GetComponent<PickupObject>());
        }

        if (other.transform.tag == "Shelf")
        {
            _isNearShelf = false;
            _objectTransforms.Remove(other.GetComponent<PickupObject>());
        }
    }
}
