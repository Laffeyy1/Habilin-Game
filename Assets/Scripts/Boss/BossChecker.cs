using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChecker : MonoBehaviour
{
    public GameObject bossHealth;
    public GameObject bossName;

    private bool isBossDestroyed = false;

    // Update is called once per frame
    void Update()
    {
        // Check if the boss has been destroyed
        if (!isBossDestroyed)
        {
            GameObject boss = GameObject.FindWithTag("Boss");

            if (boss == null)
            {
                isBossDestroyed = true;
                Debug.Log("Boss has been destroyed.");
                bossHealth.SetActive(false);
                bossName.SetActive(false);
            }
        }
    }
}
