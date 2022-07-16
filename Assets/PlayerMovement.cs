using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Vector3 _velocity;

    [SerializeField] private Transform _transform;
    [SerializeField] private float _speed;
    [SerializeField] private CharacterController _characterController;

    void Update()
    {
        bool isGrounded = _characterController.isGrounded;
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (isGrounded && _velocity.y < 0)
        {
            _velocity.y = 0f;
        }

        _characterController.Move(move * Time.deltaTime * _speed);

        if (move != Vector3.zero)
        {           
            _transform.forward = move;           
        }
    }
}
