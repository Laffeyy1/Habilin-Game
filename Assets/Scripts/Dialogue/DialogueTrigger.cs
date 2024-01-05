using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueTrigger : MonoBehaviour
{
    public StoryScene storyScene;
    public GameObject dialogueUI;
    public GameObject Trigger;

    private void Start()
    {
        if(dialogueUI == null)
        {
            GameObject dialogue = GameObject.FindGameObjectWithTag("Dialogue");
            dialogueUI = dialogue.transform.GetChild(0).gameObject;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Cutscene"))
        {
            dialogueUI.SetActive(true);
            TriggerDialogue();
            Trigger.SetActive(false);
        }
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(storyScene);
    }
}
