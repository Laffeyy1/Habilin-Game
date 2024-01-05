using EasyTransition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneHider : MonoBehaviour
{
    public string[] tagsToDestroy;

    void Start()
    {
        foreach (string tag in tagsToDestroy)
        {
            GameObject[] objectsToDestroy = GameObject.FindGameObjectsWithTag(tag);

            foreach (GameObject obj in objectsToDestroy)
            {
                Destroy(obj);
            }
        }
    }
}
