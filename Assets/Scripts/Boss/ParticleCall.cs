using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCall : MonoBehaviour
{
    public GameObject itemDrop;
    private void Start()
    {
        StartCoroutine(SpawnItemWithDelay());
    }
    IEnumerator SpawnItemWithDelay()
    {
        yield return new WaitForSeconds(3);

        Instantiate(itemDrop, transform.position, Quaternion.identity);
    }
}
