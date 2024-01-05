using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BlinkingText : MonoBehaviour
{
    public float blinkInterval = 0.5f; // Duration of each blink cycle in seconds

    private TMP_Text textComponent;
    private bool isVisible = true;

    private void Start()
    {
        textComponent = GetComponent<TMP_Text>();

        // Start the blinking coroutine
        StartCoroutine(BlinkText());
    }

    private System.Collections.IEnumerator BlinkText()
    {
        while (true)
        {
            // Toggle the visibility of the text component
            textComponent.enabled = isVisible;
            isVisible = !isVisible;

            // Wait for the specified blink interval
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}
