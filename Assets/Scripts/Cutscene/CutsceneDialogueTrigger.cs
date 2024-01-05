using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneDialogueTrigger : MonoBehaviour
{
    public StoryScene storyScene;
    public GameObject dialogueUI;
    public GameObject Trigger;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            dialogueUI.SetActive(true);
            TriggerDialogue();
            Trigger.SetActive(false);
        }
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<CutsceneDialogue>().StartDialogue(storyScene);
    }
}
