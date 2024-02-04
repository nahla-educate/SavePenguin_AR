using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private FixedJoystick _joyStick;
    // [SerializeField] private Animator animator;

    [SerializeField] private float moveSpeed;

    
    private void FixedUpdate()
    {
        rb.velocity = new Vector3(_joyStick.Horizontal * moveSpeed, rb.velocity.y, _joyStick.Vertical * moveSpeed);
        Debug.Log(rb.velocity);
        if (_joyStick.Horizontal != 0 || _joyStick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity);
        }

    }
}
    