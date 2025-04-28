using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thurst;
    [SerializeField] float thurstStrength = 100f;
    
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        thurst.Enable();
    }

    private void FixedUpdate()
    {
        if (thurst.IsPressed())
        {
            rb.AddRelativeForce(Vector3.up * thurstStrength * Time.fixedDeltaTime);
        }
    }
}
