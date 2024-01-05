using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

public class Pause : MonoBehaviour
{
    private bool isPaused;
    public GameObject storyPause;
    public GameObject endlessPause;

    public GameObject pausePanel;

    private void Update()
    {
        if (PlayerPrefs.HasKey("Mode"))
        {
            string mode = PlayerPrefs.GetString("Mode");
            if (mode == "Story")
            {
                storyPause.SetActive(true);
                endlessPause.SetActive(false);
            }
            else if (mode == "Endless")
            {
                storyPause.SetActive(false);
                endlessPause.SetActive(true);
            }
        }
    }
    public void OnPause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0;
            pausePanel.SetActive(true);
        }
    }

    public void OnResume()
    {
        Time .timeScale = 1;
        pausePanel.SetActive(false);
    }

    public void RemoveComponents()
    {
        Component[] components = gameObject.GetComponents<Component>();

        foreach (var component in components)
        {
            if (!(component is Transform))
            {
                Destroy(component);
            }
        }
    }
}
