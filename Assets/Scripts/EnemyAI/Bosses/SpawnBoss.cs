using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoss : MonoBehaviour
{
    public GameObject bossPrefab;
    public Transform spawnTarget;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log("PLayer entered");
            Instantiate(bossPrefab, spawnTarget.position, spawnTarget.rotation);
            Destroy(gameObject);
        }
    }
}
