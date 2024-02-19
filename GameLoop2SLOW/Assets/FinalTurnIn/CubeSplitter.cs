using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSplitter : MonoBehaviour
{
    public GameObject destructablePrefab; // Assign the "destructable" prefab in the Inspector
    public float splitForce = 1f; // Adjust this value based on your game's requirements
    public float impactTremor = 13f;
    public float raycastDistance = 2f;
    public float addedRigidbodyMass = 20f;

    void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is from an impact, then trigger the split.
        // You might want to add additional conditions based on your game logic.
        if ((collision.relativeVelocity.magnitude > impactTremor) || collision.gameObject.CompareTag("Fist")) // Adjust impact threshold as needed
        {
            Rigidbody rb = GetComponent<Rigidbody>();

            if (rb == null)
            {
                rb = gameObject.AddComponent<Rigidbody>();
            }
            DetectCubesAbove();
            SplitCube();
        }
    }

    void SplitCube()
    {
        // Define the dimensions of the smaller cubes
        float smallerCubeSize = transform.localScale.x / 3.0f;
        // Calculate the offset for each smaller cube in a grid
        for (int x = -1; x <= 1; x += 2) // Only -1, 0, 1
        {
            for (int y = -1; y <= 1; y += 2) // Only -1, 0, 1
            {
                for (int z = -1; z <= 1; z += 2) // Only -1, 0, 1
                {
                    // Instantiate the "destructable" prefab
                    GameObject smallerCube = Instantiate(destructablePrefab, transform.position + new Vector3(x * smallerCubeSize, y * smallerCubeSize, z * smallerCubeSize), Quaternion.identity);

                    // Apply force to the smaller cubes based on their position relative to the original cube
                    Vector3 forceDirection = (smallerCube.transform.position - transform.position).normalized;
                    smallerCube.GetComponent<Rigidbody>().AddForce(forceDirection * splitForce * Time.deltaTime, ForceMode.Impulse); // Apply Time.deltaTime
                }
            }
        }

        // Destroy the original cube
        Destroy(gameObject);
    }

    void DetectCubesAbove()
    {
        RaycastHit hit;
        Vector3 raycastOrigin = transform.position;

        // Perform multiple raycasts in the upward direction
        while (Physics.Raycast(raycastOrigin, Vector3.up, out hit, raycastDistance))
        {
            GameObject hitObject = hit.collider.gameObject;

            // Check if the hit object is a cube and doesn't have a Rigidbody
            if (hitObject.CompareTag("Destructable") && hitObject.GetComponent<Rigidbody>() == null)
            {
                // Add a Rigidbody to the cube above
                Rigidbody nearbyRigidbody = hitObject.AddComponent<Rigidbody>();
                nearbyRigidbody.mass = addedRigidbodyMass;
            }

            // Move the raycast origin to the top of the currently detected cube
            raycastOrigin = hit.point + Vector3.up * 0.01f; // Slightly above to avoid immediate re-detection
        }
    }
}
