using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public string playerTag = "Player";
    public float followSpeed = 5f;

    private Transform playerTransform;
    private bool shouldFollowPlayer = true; // Flag to control camera following

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag(playerTag);
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogWarning("No GameObject with the 'Player' tag found in the scene.");
        }
    }

    private void Update()
    {
        if (shouldFollowPlayer && playerTransform != null)
        {
            Vector3 newPos = new Vector3(playerTransform.position.x, playerTransform.position.y, -10f);
            transform.position = Vector3.Slerp(transform.position, newPos, followSpeed * Time.deltaTime);
        }
    }

    // Method to enable/disable camera following
    public void SetCameraFollowing(bool shouldFollow)
    {
        shouldFollowPlayer = shouldFollow;
    }
}
