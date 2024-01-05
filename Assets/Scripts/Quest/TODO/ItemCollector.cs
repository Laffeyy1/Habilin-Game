using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    public string itemName; // The name of the item this object represents

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Assuming you have a QuestSystem instance
            QuestSystem questSystem = QuestSystem.instance;
            bool itemCollected = false; // Flag to track if the item was collected

            if (questSystem != null)
            {
                // Check if there are active quests with objectives related to this item
                foreach (Quest quest in questSystem.activeQuests)
                {
                    foreach (Objective objective in quest.objectives)
                    {
                        if (!objective.completed && objective.targetName == itemName)
                        {
                            // Mark the objective as completed
                            objective.completed = true;
                            // Update the UI
                            questSystem.questUI.UpdateObjectiveText(quest);
                            itemCollected = true; // Set the flag to true
                        }
                    }
                }
            }

            // Disable the collected item if it was collected
            if (itemCollected)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
