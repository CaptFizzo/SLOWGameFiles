using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string sceneToLoad; // Name of the scene to load

 private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Fist"))
        {
            LoadScene();
        }
    }
    public void LoadScene()
    {
        // Load the specified scene
        SceneManager.LoadScene(sceneToLoad);
    }
}
