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
        else
        {
            audioSource.Stop();
            mainEngineParticles.Stop();
        }
    }
    
    private void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();
       
        if (rotationInput < 0)
        {
            ApplyRotation(rotationStrength);
            if (!rightThurstParticles.isPlaying)
            {
                leftThurstParticles.Stop();
                rightThurstParticles.Play();
            }
        }
        else if (rotationInput > 0)
        {
            ApplyRotation(-rotationStrength);
            
            if (!leftThurstParticles.isPlaying)
            {
                rightThurstParticles.Stop();
                leftThurstParticles.Play();
            }
        }
        else
        {
            leftThurstParticles.Stop();
            rightThurstParticles.Stop();
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.fixedDeltaTime);
        rb.freezeRotation = false;
    }
}
