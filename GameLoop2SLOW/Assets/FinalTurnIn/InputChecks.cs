using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class InputChecks : MonoBehaviour
{
    private InputDevice targetDevice;
    public GameObject laserPrefab;
    public Transform mainCamera;
    public float maxRange = 10f;
    public float cooldownTime = 1f;
    public float laserDuration = 2f;
    public float spawnDistance = 1.5f;
    public float laserForce = 10f;
    private bool canShoot = true;

    void Start()
    {
        TryInitialize();
    }

    void TryInitialize()
    {
        targetDevice = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
    }

    void Update()
    {
        if (!targetDevice.isValid)
        {
            TryInitialize();
            return;
        }

        AimLaser();
        CheckInput();
    }

    void CheckInput()
    {
        // Check for trigger input
        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue) && triggerValue > 0.1f)
        {
            Debug.Log("Trigger Held");

            if (canShoot)
            {
                StartCoroutine(FireLaser());
                StartCoroutine(Cooldown());
            }
        }

        // Check for button input
        if (targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool buttonPressed) && buttonPressed)
        {
            Debug.Log("Primary Button Pressed");
        }
    }

    void AimLaser()
    {
        // Get the direction the player is looking
        Vector3 direction = mainCamera.forward;

        // Rotate laser to aim in the direction
        laserPrefab.transform.rotation = Quaternion.LookRotation(direction);
    }

    IEnumerator FireLaser()
    {
        // Calculate the spawn position outside the VR rig hitbox
        Vector3 spawnPosition = mainCamera.position + mainCamera.forward * spawnDistance;

        // Calculate the launch direction based on the camera's forward direction
        Vector3 launchDirection = mainCamera.forward;

        // Instantiate the laser prefab at the calculated position and rotation
        GameObject laser = Instantiate(laserPrefab, spawnPosition, Quaternion.LookRotation(launchDirection));

        // Get the Rigidbody component of the laser prefab
        Rigidbody laserRigidbody = laser.GetComponent<Rigidbody>();

        // Check if the laserPrefab has a Rigidbody component
        if (laserRigidbody != null)
        {
            // Apply force to move the laser forward
            laserRigidbody.AddForce(launchDirection * laserForce, ForceMode.Impulse);
        }
        else
        {
            Debug.LogError("Laser prefab does not have a Rigidbody component!");
        }

        // Deactivate the laser after the specified duration
        yield return new WaitForSeconds(laserDuration);

        // Destroy the laser object
        Destroy(laser);
    }

    IEnumerator Cooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(cooldownTime);

        // Apply Time.deltaTime to make the cooldown frame rate-independent
        canShoot = true;
    }
}
