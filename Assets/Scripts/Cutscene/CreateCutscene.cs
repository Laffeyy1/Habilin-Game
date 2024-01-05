using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCutscene : MonoBehaviour
{
    public GameObject timelinePrefab;
    public float destroyDelay = 3.0f; // Adjust this value for the desired delay in seconds

    private Transform canvas;

    void Start()
    {
        // Find the Canvas by tag (assuming your Canvas object is tagged as "Canvas").
        canvas = GameObject.FindGameObjectWithTag("Canvas")?.transform;

        if (canvas == null)
        {
            Debug.LogError("Canvas not found.");
        }
        InstantiateUI();
    }

    public void InstantiateUI()
    {
        if (canvas != null)
        {
            Debug.Log("Instantiating UI");
            GameObject newTimeline = Instantiate(timelinePrefab);

            // Set the parent of the new UI elements to the Canvas
            newTimeline.transform.SetParent(canvas, false);

            // Start a timer to destroy the prefab after the specified delay
            Destroy(newTimeline, destroyDelay);
        }
        else
        {
            Debug.Log("No canvas found");
        }
    }
}
