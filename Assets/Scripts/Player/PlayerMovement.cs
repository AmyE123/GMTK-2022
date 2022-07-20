using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private const int DEFAULT_MOVEMENT_SPEED = 5;
    private const int DEFAULT_ROTATION_SPEED = 10;

    [SerializeField] private Transform _transform;
    [SerializeField] private CharacterController _characterController;

    [SerializeField] private float _floorLevel = 0.5f;

    private Vector3 _velocity;
    private float _movementSpeed = DEFAULT_MOVEMENT_SPEED;
    private float _rotationSpeed = DEFAULT_ROTATION_SPEED;

    private bool _canMove = true;
    private bool _paused = false;

    public void SetPaused(bool yesno) => _paused = yesno;

    public void StopMoving()
    {
        _canMove = false;
    }

    void Update()
    {
        if (_canMove == false || _paused)
            return;
            
        bool isGrounded = _characterController.isGrounded;
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        move = Vector3.ClampMagnitude(move, 1);
        
        Vector3 pos = transform.position;
        pos.y = _floorLevel;
        transform.position = pos;

        if (isGrounded && _velocity.y < 0)
        {
            _velocity.y = 0f;
        }    

        if (move != Vector3.zero)
        {
            var targetRot = Quaternion.LookRotation(move, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, _rotationSpeed * Time.deltaTime);
            float alignment = Vector3.Dot(transform.forward, move.normalized);
            alignment = Mathf.Clamp01(alignment);
            _characterController.Move(transform.forward * Time.deltaTime * move.magnitude * alignment * _movementSpeed);
        }
    }
}
