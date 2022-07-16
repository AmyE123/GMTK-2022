using UnityEngine;

public class PickupObject : MonoBehaviour
{
    public bool IsPickedup;

    [SerializeField]
    private Transform _toFollow;

    public void SetToFollow(Transform toFollow)
    {
        _toFollow = toFollow;
    }

    protected virtual void Update()
    {
        if (_toFollow != null)
            transform.position = _toFollow.position;
    }
}
