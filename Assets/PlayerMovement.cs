using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Vector3 _velocity;

    [SerializeField] private Transform _transform;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private CharacterController _characterController;

    void Update()
    {
        bool isGrounded = _characterController.isGrounded;
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        move = Vector3.ClampMagnitude(move, 1);

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
            _characterController.Move(transform.forward * Time.deltaTime * move.magnitude * alignment * _speed);
        }        
    }
}
