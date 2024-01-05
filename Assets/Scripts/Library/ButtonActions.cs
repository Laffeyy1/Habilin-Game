using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonActions : MonoBehaviour
{
    private GameObject targetObject; // The object you want to toggle.

    public void SetTargetObject(GameObject target)
    {
        targetObject = target;
    }

    public void ToggleObjectActiveState()
    {
        if (targetObject != null)
        {
            targetObject.SetActive(!targetObject.activeSelf);
        }
        else
        {
            Debug.LogWarning("Target object is not set.");
        }
    }
}