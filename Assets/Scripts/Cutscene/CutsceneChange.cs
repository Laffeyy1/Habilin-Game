using EasyTransition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneChange : MonoBehaviour
{

    public float changeTime;
    public string sceneName;
    public DemoLoadScene demoLoadScene;

    private bool callOnce = true;

    private void Update()
    {
        changeTime -= Time.deltaTime;
        if(changeTime < 0 )
        {
            if(callOnce)
            {
                callOnce = false;
                demoLoadScene.LoadScene(sceneName);
            }
        }
    }
}
