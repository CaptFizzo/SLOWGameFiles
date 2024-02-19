using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDestruction : MonoBehaviour
{
    void Start()
    {
        // Find the GameObject with the WinCon script
        GameObject winconObject = GameObject.FindWithTag("WinCon");

        // Check if the WinCon script is found
        if (winconObject != null)
        {
            // Get the WinCon script from the GameObject
            wincon = winconObject.GetComponent<WinCon>();
        }
        else
        {
            // Handle the case where the WinCon script is not found
            Debug.LogError("WinCon script not found in the scene.");
        }
    }
    private WinCon wincon;
void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Fist")) 
        {
            wincon.currentScore += 1;
            Destroy(gameObject);
        }
          
    }
}
