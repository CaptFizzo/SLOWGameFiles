using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Missile : MonoBehaviour
{
    public GameObject explosionPrefab; // Prefab for the explosion effect
    public float explosionForce = 20f; // Force applied to the explosion

    private bool hasHit = false; // To ensure explosion only happens once

    void OnCollisionEnter(Collision collision)
    {
        if (!hasHit)
        {
            // Instantiate an explosion prefab at the point of collision
            InstantiateExplosion(collision.contacts[0].point);
            hasHit = true;

            // Destroy the laser object after the explosion (you can adjust this as needed)
            Destroy(gameObject);
        }
    }

    void InstantiateExplosion(Vector3 position)
    {
        // Instantiate the explosion prefab at the specified position
        GameObject explosionObject = Instantiate(explosionPrefab, position, Quaternion.identity);

        // Get the Rigidbody component of the explosion prefab
        Rigidbody explosionRigidbody = explosionObject.GetComponent<Rigidbody>();

        // Check if the explosionPrefab has a Rigidbody component
        if (explosionRigidbody != null)
        {
            // Apply explosion force to objects within the explosion radius
            explosionRigidbody.AddExplosionForce(explosionForce, position, 5f);
        }
        else
        {
            Debug.LogError("Explosion prefab does not have a Rigidbody component!");
        }

        // Destroy the explosion object after a short delay
        Destroy(explosionObject, 0.5f);
    }
}
