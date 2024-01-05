using System.Collections;
using UnityEngine;

public class BossRemoveTrap : MonoBehaviour
{
    public GameObject particlePrefab; 


    public void ObjectToRemove(string obj)
    {
        GameObject[] barrierObjects = GameObject.FindGameObjectsWithTag(obj);
        foreach (GameObject barrierObject in barrierObjects)
        {
            barrierObject.SetActive(false);
        }
        Instantiate(particlePrefab, transform.position, Quaternion.identity);
    }
}
