using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float speed = 5f; // Adjust the speed of camera movement as needed

    private GameObject keep;
    private GameObject player;

    private void Start()
    {
        keep = GameObject.FindGameObjectWithTag("Keep");
        player = GameObject.FindGameObjectWithTag("Player");
        if (keep != null)
        {
            Destroy(keep);
            Destroy(player);
        }
    }
    void Update()
    {
        float moveAmount = speed * Time.deltaTime;
        transform.Translate(Vector3.right * moveAmount);
    }
}
