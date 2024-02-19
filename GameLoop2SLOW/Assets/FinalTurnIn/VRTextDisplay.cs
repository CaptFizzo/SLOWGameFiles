using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VRTextDisplay : MonoBehaviour
{
    public string displayText = "S.L.O.W.";
    public float fadeInTime = 2f;
    public float displayTime = 3f;
    public float fadeOutTime = 2f;

    private TextMeshPro textMeshPro;
    private float startTime;

    void Start()
    {
        textMeshPro = gameObject.AddComponent<TextMeshPro>();
        textMeshPro.text = displayText;
        textMeshPro.fontSize = 36;

        Color textColor = textMeshPro.color;
        textColor.a = 0f;
        textMeshPro.color = textColor;

        startTime = Time.time;

        // Ensure the text always faces the camera by computing the correct rotation
        Vector3 lookAtPoint = Camera.main.transform.position + Camera.main.transform.forward * 1000f;
        transform.LookAt(lookAtPoint);

        // Correct the rotation to face the opposite direction
        transform.rotation = Quaternion.LookRotation(transform.forward, Vector3.up);
    }

    void Update()
    {
        float elapsedTime = Time.time - startTime;

        if (elapsedTime < fadeInTime)
        {
            FadeIn(elapsedTime / fadeInTime);
        }
        else if (elapsedTime < fadeInTime + displayTime)
        {
            Display();
        }
        else if (elapsedTime < fadeInTime + displayTime + fadeOutTime)
        {
            FadeOut((elapsedTime - fadeInTime - displayTime) / fadeOutTime);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void FadeIn(float alpha)
    {
        Color textColor = textMeshPro.color;
        textColor.a = alpha;
        textMeshPro.color = textColor;
    }

    void Display()
    {
        // Do nothing during the display time
    }

    void FadeOut(float alpha)
    {
        Color textColor = textMeshPro.color;
        textColor.a = 1f - alpha;
        textMeshPro.color = textColor;
    }
}
