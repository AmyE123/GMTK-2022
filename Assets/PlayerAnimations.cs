using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField]
    private Animator _anim;

    [SerializeField]
    private float _runSpeedMul = 1f;

    [SerializeField]
    private PlayerPickupObjectDetection _pickupDetect;

    private Vector3 _lastPos;
    private Vector3 _velocityPerSec;
    private float _holdValue = -1;

    // Start is called before the first frame update
    void Start()
    {
        _lastPos = transform.position;    
    }

    // Update is called once per frame
    void Update()
    {
        CalculateVelocity();

        float speed = _velocityPerSec.magnitude;
        float newHoldVal = _pickupDetect.IsHoldingSomething ? 1 : -1;
        
        _holdValue = Mathf.Lerp(_holdValue, newHoldVal, Time.deltaTime * 16);

        _anim.SetBool("isRunning", speed > 0.05f);
        _anim.SetFloat("runSpeed", speed * _runSpeedMul);
        _anim.SetFloat("holdingVal", _holdValue);
    }

    void CalculateVelocity()
    {
        Vector3 vel = (transform.position - _lastPos) / Time.deltaTime;
        _velocityPerSec = Vector3.Lerp(_velocityPerSec, vel, 0.25f);
        _lastPos = transform.position;
    }
}
