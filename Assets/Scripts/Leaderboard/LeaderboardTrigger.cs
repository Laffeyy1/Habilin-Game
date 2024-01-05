using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LeaderboardTrigger : MonoBehaviour
{
    GameObject leaderboardUI;

    private bool inRange = false;
    public InputAction interactAction;

    private void Awake()
    {
        leaderboardUI = GameObject.FindGameObjectWithTag("Leaderboard");
        interactAction.Enable();
        interactAction.performed += _ => Interact();
    }

    private void OnDisable()
    {
        interactAction.Disable(); // Disable the interactAction when this script is disabled
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player in range");
            inRange = true;
            UpdateUI();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
            UpdateUI();
        }
    }

    private void Interact()
    {
        if (inRange)
        {
            leaderboardUI.transform.GetChild(0).gameObject.SetActive(true);
            Debug.Log("Player interacted with the object.");
        }
    }

    private void UpdateUI()
    {
        GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
        if (gameController != null)
        {
            Transform attackUI = gameController.transform.Find("AttackUI");
            Transform interactUI = gameController.transform.Find("InteractUI");

            if (attackUI != null && interactUI != null)
            {
                attackUI.gameObject.SetActive(!inRange);
                interactUI.gameObject.SetActive(inRange);
            }
        }
    }
}
