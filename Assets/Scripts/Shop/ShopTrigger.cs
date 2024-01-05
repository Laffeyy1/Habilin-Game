using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms;

public class ShopTrigger : MonoBehaviour
{
    GameObject shopUI;
    private bool inRange = false;
    public InputAction interactAction;
    public ShopItem shopItem;

    private void Awake()
    {
        shopUI = GameObject.FindGameObjectWithTag("Shop");
        interactAction.Enable(); // Enable the interactAction
        interactAction.performed += _ => Interact(); // Register a callback for when the action is performed
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = true;
            UpdateUI();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = false;
            UpdateUI();
            shopUI.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    private void Interact()
    {
        if (inRange)
        {
            shopUI.transform.GetChild(0).gameObject.SetActive(true);
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

    private void OnDisable()
    {
        interactAction.Disable(); // Disable the interactAction when this script is disabled
    }

}