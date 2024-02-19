using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableV2 : MonoBehaviour
{
    public GameObject destructionParticles;
    private bool hasCollided = false;
    private Rigidbody rb;
    public float collisionForceThreshold = 13f;

    void OnCollisionEnter(Collision collision)
    {
        if ((!hasCollided && (collision.gameObject.CompareTag("Fist")) || (!hasCollided && collision.relativeVelocity.magnitude >= collisionForceThreshold)))
        {
            HandleFirstCollision(collision);
        }
        else if (hasCollided)
        {
            if (collision.gameObject.CompareTag("Fist"))
            {
                StartCoroutine(DeleteAfterDelay(6f));
            }
        }
        else
        {
            // If collided with any other object, start the delay for destruction
            StartCoroutine(DeleteAfterDelay(10f));
        }
    }

    void HandleFirstCollision(Collision collision)
    {
        rb = gameObject.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.AddForce(collision.impulse, ForceMode.Impulse);
        }

        hasCollided = true;
    }

    IEnumerator DeleteAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay * Time.deltaTime); // Apply Time.deltaTime
        DestroyObject();
    }

    void DestroyObject()
    {
        destructionParticles.SetActive(true);
        Instantiate(destructionParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
