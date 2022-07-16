using UnityEngine;

public class PickupObject : MonoBehaviour
{
    public bool IsPickedup;

    [SerializeField]
    private Transform _toFollow;
    
    [SerializeField]
    private PickupHolder _currentHolder;

    public void SetToFollow(Transform toFollow)
    {
        _toFollow = toFollow;
    }

    protected virtual void Update()
    {
        if (_toFollow != null)
            transform.position = _toFollow.position;
    }

    public void GetPickedUp()
    {
        _currentHolder?.RemovePickup(this);
    }

    public void SetHolder(PickupHolder holder)
    {
        _currentHolder = holder;
    }
}
