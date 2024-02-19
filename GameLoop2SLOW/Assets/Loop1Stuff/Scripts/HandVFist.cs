using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandVFist : MonoBehaviour
{// purpose is that big movements are required to switch the hands to destructive force
    public string tagToAdd = "Fist";
    public string tagToRemove = "Hand";
    public float speedThreshold = 0.3f;
    public float smoothingFactor = 0.1f; // Smoothing factor for the low-pass filter. not perfect but makes it work
    public Color readyColor;
    public Color waitColor;
// smoothing added so that there's not random changes in tags
    private Vector3 previousPosition;
    private float currentSpeed;
    private float smoothedSpeed;

    void Start()
    {
        previousPosition = transform.position;
    }

    void Update()
    {
        Vector3 currentPosition = transform.position;
        float deltaTime = Time.deltaTime;

        // Calculate the distance moved since the last frame
        float distanceMoved = Vector3.Distance(currentPosition, previousPosition);

        // Calculate the speed
        currentSpeed = distanceMoved / deltaTime;

        // Apply low-pass filter
        smoothedSpeed = Mathf.Lerp(smoothedSpeed, currentSpeed, smoothingFactor);

        if (smoothedSpeed > speedThreshold)
        {
            gameObject.tag = tagToAdd;
            GetComponent<Renderer>().material.color = readyColor;
        }
        else
        {
            gameObject.tag = tagToRemove;
            GetComponent<Renderer>().material.color = waitColor;
        }

        previousPosition = currentPosition;
    }
}
