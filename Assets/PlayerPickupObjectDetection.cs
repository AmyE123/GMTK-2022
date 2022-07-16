using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickupObjectDetection : MonoBehaviour
{
    [SerializeField] private bool _isWithinRange = false;

    [SerializeField] private List<PickupObject> _objectTransforms;
    [SerializeField] private List<Transform> _pickupPoints;
    [SerializeField] private int _numberOfPickedUpDice;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_isWithinRange && _numberOfPickedUpDice < 3)
            {                
                PickUpObject(_objectTransforms[0].transform);
                _objectTransforms[0].IsPickedup = true;
                _objectTransforms.RemoveAt(0);
                _numberOfPickedUpDice ++;
            }
        }
    }

    private void PickUpObject(Transform obj)
    {
        var pickupTransform = _pickupPoints[_numberOfPickedUpDice].transform;
        obj.parent = pickupTransform;
        obj.transform.position = pickupTransform.position;
        obj.transform.rotation = pickupTransform.rotation;
        obj.GetComponent<BoxCollider>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Pickup")
        {
            _isWithinRange = true;
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
    }
}
