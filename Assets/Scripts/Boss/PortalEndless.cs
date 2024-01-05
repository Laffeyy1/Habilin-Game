using EasyTransition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalEndless : MonoBehaviour
{
    public string[] endlessModeLevels;

    private DemoLoadScene demoLoadScene;

    private void Awake()
    {
        demoLoadScene = FindObjectOfType<DemoLoadScene>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            int temp = PlayerPrefs.GetInt("Level") + 1;
            PlayerPrefs.SetInt("Level", temp);
            PlayerPrefs.Save();
            Debug.Log(PlayerPrefs.GetInt("Level"));
            NextLevel();
        }
    }

    public void NextLevel()
    {
        if (endlessModeLevels.Length > 0)
        {
            int randomIndex = Random.Range(0, endlessModeLevels.Length);
            string sceneToLoad = endlessModeLevels[randomIndex];
            demoLoadScene.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogError("No scenes in the endlessModeLevels array.");
        }
    }
}
