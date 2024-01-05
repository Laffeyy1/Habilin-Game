using EasyTransition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMove : MonoBehaviour
{
    public string sceneName;
    public DemoLoadScene demoLoadScene;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            demoLoadScene.LoadScene(sceneName);
        }
    }
}
