using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrigger : MonoBehaviour
{
    // Reference to the Quest you want to assign
    public Quest questToAssign;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Assuming you have a QuestSystem instance
            QuestSystem questSystem = QuestSystem.instance;

            if (questSystem != null)
            {
                // Assign the quest to the player
                questSystem.AcceptQuest(questToAssign);
                // Optionally, disable the trigger so the quest can't be assigned again
                gameObject.SetActive(false);
            }
        }
    }
}
