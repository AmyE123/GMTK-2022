using GameData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickupObjectDetection : MonoBehaviour
{
    private bool _IsFacingDice => _inRangePickups.Count > 0;
    private bool _IsFacingShelf => _inRangeShelves.Count > 0;
    private bool _IsFacingOven => _inRangeOvens.Count > 0;
    private bool _IsFacingOutput => _inRangeOutputs.Count > 0;

    [SerializeField] private List<PickupObject> _inRangePickups;
    [SerializeField] private List<ObjectShelf> _inRangeShelves;
    [SerializeField] private List<OutputShelf> _inRangeOutputs;
    [SerializeField] private List<Oven> _inRangeOvens;

    [SerializeField] private List<PickupObject> _pickedUpObjects;
    [SerializeField] private List<Transform> _pickupPoints;

    [SerializeField] private LevelController _level;

    private int _NumberOfPickedUpDice => _pickedUpObjects.Count;
    private PickupObject _ClosestPickup => GetClosest(_inRangePickups);
    private ObjectShelf _ClosestShelf => GetClosest(_inRangeShelves);
    private Oven _ClosestOven => GetClosest(_inRangeOvens);
    private OutputShelf _ClosestOutput => GetClosest(_inRangeOutputs);

    [SerializeField]
    private string _contextualAction = "???";
    [SerializeField]
    private Transform _contextualTarget;

    public string ContextualAction => _contextualAction;
    public Transform ContextualTarget => _contextualTarget;

    public bool IsHoldingSomething => _pickedUpObjects.Count > 0;

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
                _level.OnPlayerHoldChanged(_pickedUpObjects);
            }
        }
        else if (_IsFacingShelf && isHoldingStuff)
        {
            PickupObject topHeldItem = _pickedUpObjects[_NumberOfPickedUpDice - 1];

            _contextualAction = "put down";
            _contextualTarget = topHeldItem.transform;

            if (Input.GetKeyDown(KeyCode.E))
            {
                PutDownObject(topHeldItem);
                _level.OnPlayerHoldChanged(_pickedUpObjects);
            }
        }
        else if (_IsFacingOven && isHoldingStuff)
        {
            PickupObject topHeldItem = _pickedUpObjects[_NumberOfPickedUpDice - 1];

            _contextualAction = "bake";
            _contextualTarget = topHeldItem.transform;

            if (Input.GetKeyDown(KeyCode.E))
            {
                var ovn = _ClosestOven;

                List<GameDice> gameDice = new List<GameDice>();

                foreach (PickupObject obj in _pickedUpObjects)
                {
                    if (obj is GameDice)
                    {
                        gameDice.Add(obj as GameDice);
                    }
                }

                bool didSucceed = ovn.GiveDice(gameDice);

                if (didSucceed)
                {
                    foreach(GameDice dice in gameDice)
                    {
                        Destroy(dice.gameObject);
                        _pickedUpObjects.Remove(dice);
                        _level.OnPlayerHoldChanged(_pickedUpObjects);
                    }
                }
            }
        }
        else if (_IsFacingOven && areHandsFull == false && _ClosestOven.HasItemReady)
        {
            _contextualAction = "take out food";
            _contextualTarget = _ClosestOven.transform;

            if (Input.GetKeyDown(KeyCode.E))
            {
                FinishedFood result = _ClosestOven.TakeOutItem();

                if (result != null)
                {
                    PickUpObject(result);
                    _level.OnPlayerHoldChanged(_pickedUpObjects);
                }
            }
        }
        else if (_IsFacingOutput && isHoldingStuff)
        {
            PickupObject topHeldItem = _pickedUpObjects[_NumberOfPickedUpDice - 1];

            if (topHeldItem is FinishedFood)
            {
                _contextualAction = "deliver";
                _contextualTarget = topHeldItem.transform;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    bool success = _ClosestOutput.TryPutDownFood(topHeldItem as FinishedFood);

                    if (success)
                    {
                        _pickedUpObjects.Remove(topHeldItem);
                        _level.OnPlayerHoldChanged(_pickedUpObjects);
                    }
                }
            }
            else
            {
                _contextualAction = "";
                _contextualTarget = null;
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

        if (other.transform.tag == "Oven")
        {
            _inRangeOvens.Add(other.GetComponent<Oven>());
        }

        if (other.transform.tag == "Output")
        {
            _inRangeOutputs.Add(other.GetComponent<OutputShelf>());
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

        if (other.transform.tag == "Oven")
        {
            _inRangeOvens.Remove(other.GetComponent<Oven>());
        }

        if (other.transform.tag == "Output")
        {
            _inRangeOutputs.Remove(other.GetComponent<OutputShelf>());
        }
    }
}
