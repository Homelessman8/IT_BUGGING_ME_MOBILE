using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SplatDisappear : MonoBehaviour
{
    float despawnTime = 3f;
    Material material;
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().material;
        if(Application.isPlaying)
        {
            StartCoroutine(SpawnFade(despawnTime));
        }
    }

    private IEnumerator SpawnFade(float timer)
    {
        // Store the initial alpha value of the material
        float initialAlpha = material.color.a;

        // Calculate the alpha increment per second to achieve a smooth fade
        float alphaIncrement = initialAlpha / timer;

        // Create a timer variable
        float elapsedTimer = 0f;

        while (elapsedTimer < timer)
        {
            // Calculate the new alpha value for the material
            float newAlpha = Mathf.Lerp(initialAlpha, 0f, elapsedTimer / timer);

            // Apply the new alpha to the material's color
            Color newColor = material.color;
            newColor.a = newAlpha;
            material.color = newColor;

            // Update the elapsed time
            elapsedTimer += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // Ensure that the alpha is completely zero
        Color finalColor = material.color;
        finalColor.a = 0f;
        material.color = finalColor;

        // Optionally, you can destroy the GameObject after fading out
        Destroy(gameObject);
    }
}
