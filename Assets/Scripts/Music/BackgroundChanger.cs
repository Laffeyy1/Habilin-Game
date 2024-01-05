using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundChanger : MonoBehaviour
{
    AudioManager audioManager;

    [Header("Choose Audio Clip")]
    public AudioClip desiredBackgroundClip;

    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        // Check if the AudioManager exists
        if (audioManager != null)
        {
            audioManager.SetBackgroundClip(desiredBackgroundClip);
        }
        else
        {
            Debug.LogError("AudioManager not found.");
        }
    }
}
