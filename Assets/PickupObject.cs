using UnityEngine;

public class PickupObject : MonoBehaviour
{
    [SerializeField] private GameObject _pickupUI;
    [SerializeField] private Transform _objectTransform;
    [SerializeField] private BoxCollider _objectCollider;
    [SerializeField] private float _placeDownDistance;

    public PlayerProperties PlayerProperties;

    [SerializeField] private bool _isPickedUp = false;
    [SerializeField] private bool _isWithinRange = false;

    private void Awake()
    {
        InitializeObject();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_isWithinRange)
            {
                _isPickedUp = true;
                PickUpObjectToggle(_isPickedUp);
            }
            else if (_isPickedUp)
            {
                _isPickedUp = false;
                PickUpObjectToggle(_isPickedUp);
            }
        }
    }

    private void InitializeObject()
    {
        _pickupUI.SetActive(false);
    }

    private void PickUpObjectToggle(bool isPickingUp)
    {
        if (isPickingUp)
        {
            Transform PickupTransform = PlayerProperties.PickupTransform;
            SetObjectTransform(PickupTransform, PickupTransform.position, PickupTransform.rotation, false, false);
        }
        else
        {
            Vector3 playerDirection = PlayerProperties.PlayerTransform.forward;
            Vector3 playerPosition = PlayerProperties.PlayerTransform.position;
            Vector3 placeObjectPosition = playerPosition + playerDirection * _placeDownDistance;

            SetObjectTransform(null, placeObjectPosition, Quaternion.Euler(Vector3.zero), true, true);
        }
    }

    /// <summary>
    /// Setting the object down
    /// </summary>
    /// <param name="parent">The parent which the object is set to</param>
    /// <param name="position">The position which the object is set to</param>
    /// <param name="rotation">The rotation which the object is set to</param>
    /// <param name="objectHasCollision">If the object has collision enabled when position is set</param>
    /// <param name="objectPickupUIIsActive">If the object has the pickup UI enabled when position is set</param>
    private void SetObjectTransform(Transform parent, Vector3 position, Quaternion rotation, bool objectHasCollision, bool objectPickupUIIsActive)
    {
        _objectTransform.parent = parent;
        _objectTransform.position = position;
        _objectTransform.rotation = rotation;
        _objectCollider.gameObject.SetActive(objectHasCollision);
        _pickupUI.SetActive(objectPickupUIIsActive);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            _pickupUI.SetActive(true);
            _isWithinRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            _pickupUI.SetActive(false);
            _isWithinRange = false;
        }
    }
}
