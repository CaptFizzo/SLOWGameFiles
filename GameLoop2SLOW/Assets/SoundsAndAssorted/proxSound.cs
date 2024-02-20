using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Copilot
public class proxSound : MonoBehaviour
{ 
    public float triggerDistance = 5.0f; // The distance at which the sound will play
    public AudioSource controlledAudioSource; // The AudioSource to control

    private GameObject player; // Reference to the player

    void Start()
    {
        // Set the player to be the main camera
        player = Camera.main.gameObject;
    }

    void Update()
    {
        // Calculate the distance on the XZ plane (ignoring Y)
        Vector3 delta = player.transform.position - transform.position;
        delta.y = 0; // Ignore Y distance

        // If the player is close enough on the XZ plane, play the sound
        if (delta.magnitude < triggerDistance)
        {
            if (!controlledAudioSource.isPlaying)
            {
                controlledAudioSource.Play();
                Debug.Log("Playing sound");
            }
        }
        else if (controlledAudioSource.isPlaying)
        {
            controlledAudioSource.Stop();
        }
    }
}
