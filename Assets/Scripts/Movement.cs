using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thurst;
    [SerializeField] InputAction rotation;
    [SerializeField] float thurstStrength = 100f;
    [SerializeField] float rotationStrength = 100f;
    [SerializeField] AudioClip mainEngineSFX;
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftThurstParticles;
    [SerializeField] ParticleSystem rightThurstParticles;
    
    Rigidbody rb;
    AudioSource audioSource;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
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
            StartThursting();
        }
        else
        {
            StopThursting();
        }
    }

    private void StartThursting()
    {
        rb.AddRelativeForce(Vector3.up * thurstStrength * Time.fixedDeltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngineSFX);
        }
        if (!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play();
        }
    }
    
    private void StopThursting()
    {
        audioSource.Stop();
        mainEngineParticles.Stop();
    }
    
    private void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();
       
        if (rotationInput < 0)
        {
            RotateRight();
        }
        else if (rotationInput > 0)
        {
            RotateLeft();
        }
        else
        {
            StopRotating();
        }
    }

    private void RotateRight()
    {
        ApplyRotation(rotationStrength);
        if (!rightThurstParticles.isPlaying)
        {
            leftThurstParticles.Stop();
            rightThurstParticles.Play();
        }
    }
    
    private void RotateLeft()
    {
        ApplyRotation(-rotationStrength);
        if (!leftThurstParticles.isPlaying)
        {
            rightThurstParticles.Stop();
            leftThurstParticles.Play();
        }
    }
    
    private void StopRotating()
    {
        leftThurstParticles.Stop();
        rightThurstParticles.Stop();
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.fixedDeltaTime);
        rb.freezeRotation = false;
    }
}
