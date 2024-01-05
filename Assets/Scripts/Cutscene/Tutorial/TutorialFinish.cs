using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TutorialFinish : MonoBehaviour, IDataPersistence
{
    public PlayableDirector timelineDirector;
    GameObject keep;
    GameObject player;

    [Header("Objects to Show / Hide if tutorial is finished.")]
    public GameObject[] objectsToHide;
    public GameObject[] objectsToShow;

    private bool isFinished = false;

    // Start is called before the first frame update
    void Start()
    {
        keep = GameObject.FindGameObjectWithTag("Keep");
        player = GameObject.FindGameObjectWithTag("Player");

        if(keep != null )
        {
            Destroy(keep);
            Destroy(player);
        }
        if (isFinished)
        {
            Camera.main.transform.position = new Vector3(4.82f, -4.92f, 0f);
            // Deactivate objects to hide
            if (objectsToHide != null)
            {
                foreach (GameObject obj in objectsToHide)
                {
                    obj.SetActive(false);
                }
            }

            if (objectsToShow != null)
            {
                // Activate objects to show
                foreach (GameObject obj in objectsToShow)
                {
                    obj.SetActive(true);
                }
            }
        }
        else
        {
            PlayTimeline();
        }
    }

    public void LoadData(GameData data)
    {
        isFinished = data.isTutorialFinished;
    }

    public void SaveData(GameData data)
    {
        data.isTutorialFinished = isFinished;
    }

    private void OnEnable()
    {
        // Subscribe to the stopped event of the PlayableDirector.
        if (timelineDirector != null)
        {
            timelineDirector.stopped += OnTimelineFinished;
        }
    }

    private void OnDisable()
    {
        // Unsubscribe from the stopped event to prevent memory leaks.
        if (timelineDirector != null)
        {
            timelineDirector.stopped -= OnTimelineFinished;
        }
    }

    private void OnTimelineFinished(PlayableDirector director)
    {
        if (director == timelineDirector)
        {
            // This event is called when the Timeline finishes.
            Debug.Log("Tutorial has been finished");
            isFinished = true;

            DataPersistenceManager.instance.SaveGame();
        }
    }

    private void PlayTimeline()
    {
        if (timelineDirector != null)
        {
            // Play the associated Timeline.
            timelineDirector.Play();
        }
        else
        {
            Debug.LogWarning("PlayableDirector component not found.");
        }
    }
}
