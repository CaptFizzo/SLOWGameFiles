using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCon : MonoBehaviour
{
    public int currentScore = 0;
    public int victoryThreshold = 8;
    public string sceneToLoad;

    // Update is called once per frame
    void Update()
    {
        // Check if the current score is above the victory threshold
        if (currentScore >= victoryThreshold)
        {
           Victory(); // Trigger the Victory method
        }
            
    }

    // Victory method to be triggered when the condition is met
    void Victory()
    {
        Debug.Log("Victory achieved!");
        SceneManager.LoadScene(sceneToLoad);
    }
}

