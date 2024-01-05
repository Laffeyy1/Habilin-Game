using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetType : MonoBehaviour
{
    public GameObject trigger;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        trigger.SetActive(false);
        PlayerPrefs.SetString("Mode", "Story");
        PlayerPrefs.SetInt("Level", 1);
        PlayerPrefs.Save();
    }
}
