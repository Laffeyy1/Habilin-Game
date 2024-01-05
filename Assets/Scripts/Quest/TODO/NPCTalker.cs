using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTalker : MonoBehaviour
{
    public string npcName; // The name of the NPC this object represents
    public StoryScene storyScene; // Reference to your dialogue system script

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Assuming you have a QuestSystem instance
            QuestSystem questSystem = QuestSystem.instance;
            bool npcInteracted = false; // Flag to track if the NPC was interacted with

            if (questSystem != null)
            {
                // Check if there are active quests with objectives related to this NPC
                foreach (Quest quest in questSystem.activeQuests)
                {
                    foreach (Objective objective in quest.objectives)
                    {
                        if (!objective.completed && objective.targetName == npcName)
                        {
                            // Mark the objective as completed
                            objective.completed = true;
                            // Update the UI
                            questSystem.questUI.UpdateObjectiveText(quest);
                            npcInteracted = true; // Set the flag to true
                        }
                    }
                }
            }

            // Initiate dialogue with the NPC if it was interacted with
            if (npcInteracted)
            {
                // Assuming you have a dialogue system script
                TriggerDialogue();
            }
        }
    }
    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(storyScene);
    }
}



