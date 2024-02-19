using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StandInBody : MonoBehaviour
{
    public Transform targetTransform; // Reference to the target transform
    public float customYOffset = -4.0f; // Customizable y-distance

    void Update()
    {
        if (targetTransform != null)
        {
            // Match the x and z positions of the current object to the target object
            Vector3 newPosition = new Vector3(targetTransform.position.x, targetTransform.position.y + customYOffset, targetTransform.position.z);

            // Set the position of the current object
            transform.position = newPosition;
        }
        else
        {
            Debug.LogError("Target transform reference is not set. Please assign the target transform in the inspector.");
        }
    }
}
