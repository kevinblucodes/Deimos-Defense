using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    
    [SerializeField] InputAction movement;
    [SerializeField] InputAction fire;

    [Header("General Setup Settings")]
    [Tooltip("How fast ship moves based on player input")][SerializeField] float controlSpeed = 10f;
    [Tooltip("Horizontal ship distance limit from screen center")][SerializeField] float xRange = 4.2f;
    [Tooltip("Vertical ship distance limit from screen center")][SerializeField] float yRange = 3.4f;
    [Tooltip("Controls how much ship pitch changes based on player input")][SerializeField] float controlPitchFactor = -7f;


    [Tooltip("Controls how much ship pitch changes based on screen position")][SerializeField] float positionPitchFactor = -5f;

    [Tooltip("Controls how much ship pitch changes based on player input")][SerializeField] float controlRollFactor = 5f;
    [Tooltip("Controls how much ship yaw changes based on screen position")][SerializeField] float positionYawFactor = -5f;

    [SerializeField] GameObject[] lasers;



    float xThrow;
    float yThrow;
  
    void Start()
    {
        
    }

   void OnEnable()
    {
        movement.Enable();
        fire.Enable();
    }

   void OnDisable()
    {
        movement.Disable();
        fire.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();

    }

    void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controlRollFactor;


        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void ProcessTranslation()
    {
        xThrow = movement.ReadValue<Vector2>().x;
        yThrow = movement.ReadValue<Vector2>().y;

        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float yOffset = yThrow * Time.deltaTime * controlSpeed;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    void ProcessFiring()
    {
        if (fire.ReadValue<float>() > 0.5f)
        {
            SetLasersActive(true);
        }
        else 
        {
            SetLasersActive(false);
        }
    }

    void SetLasersActive(bool isActive)
    {
        foreach (GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }

}
