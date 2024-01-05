using EasyTransition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Cinemachine.CinemachineTriggerAction.ActionSettings;

public class GamemodeSelection : MonoBehaviour
{
    public GameObject gameMode;

    public string storySceneName;
    public string mactanEndless = "Mactan";
    public string manilaEndless = "Manila";
    public DemoLoadScene demoLoadScene;

    public GameObject toHide;

    AudioManager audioManager;

    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            gameMode.SetActive(true);
            toHide.SetActive(false);
        }
    }

    public void StoryMode()
    {
        PlayerPrefs.SetString("Mode", "Story");
        PlayerPrefs.Save();

        audioManager.Portal();
        demoLoadScene.LoadScene(storySceneName);
        toHide.SetActive(true);
    }

    public void EndlessMode()
    {
        PlayerPrefs.SetString("Mode", "Endless");
        PlayerPrefs.Save();

        audioManager.Portal();

        int randomIndex = Random.Range(0, 2);

        string selectedScene = (randomIndex == 0) ? mactanEndless : manilaEndless;

        demoLoadScene.LoadScene(selectedScene);
        toHide.SetActive(true);
    }
}
