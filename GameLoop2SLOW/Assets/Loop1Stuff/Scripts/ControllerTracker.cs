using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerTracker : MonoBehaviour
{
    public Transform objectToMatch;
    public Transform objectToAdjust;
    public float moveSpeed = 8f;

    void Update()
    {
        if (objectToMatch == null || objectToAdjust == null)
        {
            return;
        }

        Vector3 newPosition = objectToMatch.position;
        Quaternion newRotation = objectToMatch.rotation;

        float distance = Vector3.Distance(objectToAdjust.position, newPosition);
        float timeToReach = distance / moveSpeed;

        if (timeToReach <= Time.fixedDeltaTime)
        {
            objectToAdjust.position = newPosition;
            objectToAdjust.rotation = newRotation;
        }
        else
        {
            objectToAdjust.position = Vector3.Lerp(objectToAdjust.position, newPosition, Time.fixedDeltaTime / timeToReach);
            objectToAdjust.rotation = Quaternion.Lerp(objectToAdjust.rotation, newRotation, Time.fixedDeltaTime / timeToReach);
        }
    }
}
