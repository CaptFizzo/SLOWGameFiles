using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    public Color targetColor; // What color will something be once it has been hit once? To be replaced with breaking effects
    public GameObject destructionParticles; // the particles that go off after destruction
    private bool firstTime = true; // prevents errors about attaching multiple rigidbodies from being thrown
    private bool hasCollided = false; // checks if the object has been hit once

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Fist") && hasCollided)
        {
            DestroyObject();
        }
        if (collision.gameObject.CompareTag("Fist") && !hasCollided && firstTime)
        {
            Rigidbody rb = GetComponent<Rigidbody>();

            if (rb == null)
            {
                rb = gameObject.AddComponent<Rigidbody>();
                rb.AddForce(collision.impulse, ForceMode.Impulse); // added so that upon first hit the rigidbody gets added, but the surface acts like its been hit
            }

            firstTime = false;
            StartCoroutine(DelayedCollisionNotice(1f)); // using coroutine instead of Invoke
            StartCoroutine(DeleteAfterDelay(10f)); // Wait for 10 seconds before deleting the block
        }
    }

    IEnumerator DelayedCollisionNotice(float delay)
    {
        yield return new WaitForSeconds(delay);
        CollisionNotice();
    }

    void CollisionNotice()
    {
        GetComponent<Renderer>().material.color = targetColor;
        hasCollided = true;
    }

    IEnumerator DeleteAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        DestroyObject();
    }

    void DestroyObject() // particle effects and destruction
    {
        Debug.Log("destroyed");
        destructionParticles.SetActive(true);
        Instantiate(destructionParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
