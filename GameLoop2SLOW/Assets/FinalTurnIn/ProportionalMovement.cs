using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProportionalMovement : MonoBehaviour
{
    public Transform objectA;
    public Transform objectB;
    public Transform objectC;
    public float scaleFactor = 12f;

    void FixedUpdate()
    {
        if (objectA == null || objectB == null || objectC == null)
        {
            return;
        }

        objectB.rotation = objectA.rotation;
        Vector3 movementRelativeToC = objectA.position - objectC.position;
        objectB.position = objectC.position + movementRelativeToC * scaleFactor;
    }
}
