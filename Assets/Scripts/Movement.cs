using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thurst;
    [SerializeField] InputAction rotation;
    [SerializeField] float thurstStrength = 100f;
    
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        thurst.Enable();
        rotation.Enable();
    }

    private void FixedUpdate()
    {
        ProcessThurst();
        ProcessRotation();
    }

    private void ProcessThurst()
    {
        if (thurst.IsPressed())
        {
            rb.AddRelativeForce(Vector3.up * thurstStrength * Time.fixedDeltaTime);
        }
    }
    
    private void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();
        Debug.Log("Rotation Input: " + rotationInput);
    }
}
