using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineFinishChecker : MonoBehaviour
{
    public GameObject[] objectsToHide;
    public GameObject[] objectsToShow;

    public PlayableDirector playableDirector; // Reference to the PlayableDirector component.

    private bool isFinished = false;

    private void Start()
    {
        if (playableDirector == null)
        {
            Debug.LogError("PlayableDirector is not assigned.");
        }
        else
        {
            playableDirector.stopped += OnTimelineFinished; // Subscribe to the stopped event.
        }
    }

    private void OnTimelineFinished(PlayableDirector director)
    {
        if (director == playableDirector)
        {
            // This event is called when the Timeline finishes.
            isFinished = true;
            
            // Deactivate objects to hide
            if(objectsToHide != null)
            {
                foreach (GameObject obj in objectsToHide)
                {
                    obj.SetActive(false);
                }
            }

            if(objectsToShow != null)
            {
                // Activate objects to show
                foreach (GameObject obj in objectsToShow)
                {
                    obj.SetActive(true);
                }
            }
        }
    }

    private void Update()
    {
        // You can also check the state of the Timeline in Update if needed.
        if (isFinished)
        {
            
        }
    }
}