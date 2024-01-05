using EasyTransition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalDrop : MonoBehaviour
{
    public string storySceneName = "Level One";

    private DemoLoadScene demoLoadScene;

    private void Awake()
    {
        demoLoadScene = FindObjectOfType<DemoLoadScene>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            StoryMode();
        }
    }

    public void StoryMode()
    {
        demoLoadScene.LoadScene(storySceneName);
    }

}
