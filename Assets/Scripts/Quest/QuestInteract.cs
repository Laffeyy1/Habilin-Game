using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class QuestInteract : MonoBehaviour
{
    public StoryScene storyScene;
    public Quest questToAssign;

    private GameObject dialogueUI;
    public GameObject Trigger;
    private Collider2D questCollider;
    private void Start()
    {
        if (dialogueUI == null)
        {
            GameObject dialogue = GameObject.FindGameObjectWithTag("Dialogue");
            dialogueUI = dialogue.transform.GetChild(0).gameObject;
        }
        questCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Show the dialogue UI and trigger the dialogue
            dialogueUI.SetActive(true);
            TriggerDialogue();

            // Assign the quest to the player
            QuestSystem questSystem = QuestSystem.instance;
            if (questSystem != null)
            {
                questSystem.AcceptQuest(questToAssign);
            }
            questCollider.enabled = false;
        }
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(storyScene);
    }
}
