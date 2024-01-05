using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Chest : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private int coinsToDrop = 5;
    [SerializeField] private LootTable lootTable;
    private bool inRange = false;
    private bool isOpened = false;
    public InputAction interactAction;

    private Collider2D col;
    AudioManager audioManager;
    private void OnEnable()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        col = GetComponent<Collider2D>();
        interactAction.Enable();
        interactAction.performed += _ => TryOpenChest();
    }

    private void OnDisable()
    {
        interactAction.Disable();
        interactAction.performed -= _ => TryOpenChest();
    }

    private void TryOpenChest()
    {
        if (inRange && !isOpened)
        {
            Debug.Log("Player opened chest");
            OpenChest();
        }
    }

    private void OpenChest()
    {
        if (!isOpened)
        {
            audioManager.openChest();
            anim.SetTrigger("open");
            isOpened = true;
            col.enabled = false;
            DropCoins();
            DropItems();
        }
    }

    private void DropCoins()
    {
        for (int i = 0; i < coinsToDrop; i++)
        {
            Instantiate(coinPrefab, transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player in range");
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
        }
    }

    private void DropItems()
    {
        GameObject itemToDrop = lootTable.GetRandomLootItem();

        if (itemToDrop != null)
        {
            Instantiate(itemToDrop, transform.position, Quaternion.identity);
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