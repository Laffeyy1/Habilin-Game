using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResetPos : MonoBehaviour
{
    public string playerTag = "Player"; // The tag of the player you want to reposition
    public Transform targetTransform;   // The target transform to which you want to reposition the player

    void Start()
    {
        // Find the player GameObject with the specified tag
        GameObject player = GameObject.FindGameObjectWithTag(playerTag);

        // Check if a player was found and a targetTransform is specified
        if (player != null && targetTransform != null)
        {
            // Reposition the player to the targetTransform's position and rotation
            player.transform.position = targetTransform.position;
            player.transform.rotation = targetTransform.rotation;
        }
        else
        {
            Debug.LogWarning("Player or targetTransform not found or specified.");
        }
    }
}
