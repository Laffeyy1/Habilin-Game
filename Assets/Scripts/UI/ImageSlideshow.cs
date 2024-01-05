using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ImageSlideshow : MonoBehaviour
{
    public List<Sprite> images;
    public Image imageToFade;
    public float fadeDuration = 1.0f;
    public float displayTime = 2.0f;

    private int currentImageIndex = 0;
    private bool isFading = false;

    void Start()
    {
        imageToFade.sprite = images[0];
        StartCoroutine(StartSlideshow());
    }

    IEnumerator StartSlideshow()
    {
        while (true)
        {
            yield return new WaitForSeconds(displayTime);
            StartCoroutine(FadeToNextImage());
        }
    }

    IEnumerator FadeToNextImage()
    {
        isFading = true;

        // Fade out the current image
        float elapsedTime = 0f;
        Color startColor = imageToFade.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0);

        while (elapsedTime < fadeDuration)
        {
            imageToFade.color = Color.Lerp(startColor, endColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        imageToFade.color = endColor;

        // Change to the next image
        currentImageIndex = (currentImageIndex + 1) % images.Count;
        imageToFade.sprite = images[currentImageIndex];

        // Fade in the next image
        startColor = imageToFade.color;
        endColor = new Color(startColor.r, startColor.g, startColor.b, 1);

        elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            imageToFade.color = Color.Lerp(startColor, endColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        imageToFade.color = endColor;
        isFading = false;
    }
}
