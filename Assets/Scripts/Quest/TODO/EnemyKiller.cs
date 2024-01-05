using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKiller : MonoBehaviour
{
    public string enemyName; // The name of the enemy this object represents

    public void EnemyKilled()
    {
        // Assuming you have a QuestSystem instance
        QuestSystem questSystem = QuestSystem.instance;


        if (questSystem != null)
        {
            // Check if there are active quests with objectives related to this item
            foreach (Quest quest in questSystem.activeQuests)
            {
                foreach (Objective objective in quest.objectives)
                {
                    if (!objective.completed && objective.targetName == enemyName)
                    {
                        // Mark the objective as completed
                        objective.completed = true;
                        // Update the UI
                        questSystem.questUI.UpdateObjectiveText(quest);
                    }
                }
            }
        }
    }
}
