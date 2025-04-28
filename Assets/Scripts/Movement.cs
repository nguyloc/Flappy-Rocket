using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thurst;
    
    private void OnEnable()
    {
        thurst.Enable();
    }

    private void Update()
    {
        if (thurst.IsPressed())
        {
            Debug.Log("Thurst");
        }
    }
}
