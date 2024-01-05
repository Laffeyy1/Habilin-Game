using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSystem : MonoBehaviour
{
    public List<Quest> activeQuests = new List<Quest>();
    public static QuestSystem instance; // Singleton instance

    // Reference to the UI element
    public QuestUI questUI;

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        // Create a list to store quests to be removed
        List<Quest> questsToRemove = new List<Quest>();

        // Loop through the active quests
        foreach (Quest quest in activeQuests)
        {
            // Check if the quest is complete
            if (quest.IsComplete())
            {
                Debug.Log("Quest completed: " + quest.questTitle);

                // Add the quest to the removal list
                questsToRemove.Add(quest);

                // Instantiate quest rewards when the quest is completed
                GrantQuestRewards(quest);

                // Update the UI when a quest is removed
                questUI.UpdateUI(quest);
            }
        }

        // Remove completed quests after the loop
        foreach (Quest quest in questsToRemove)
        {
            activeQuests.Remove(quest);
        }
    }

    public void AcceptQuest(Quest quest)
    {
        activeQuests.Add(quest);
        Debug.Log("Quest accepted: " + quest.questTitle);

        // Update the UI when a new quest is accepted
        questUI.UpdateUI(quest);
    }

    public void CompleteObjective(Quest quest, string objective)
    {
        quest.CompleteObjective(objective);

        // Update the UI when an objective is completed
        questUI.UpdateObjectiveText(quest);
    }

public void GrantQuestRewards(Quest quest)
{
    Debug.Log("Granting rewards for quest: " + quest.questTitle);

    foreach (var reward in quest.rewards)
    {
        Debug.Log("Reward item: " + reward.item.name);
        Debug.Log("Quantity: " + reward.quantity);
        Debug.Log("Spawn Location: " + reward.spawnLocation.position);

        for (int i = 0; i < reward.quantity; i++)
        {
            Instantiate(reward.item, reward.spawnLocation.position, Quaternion.identity);
            Debug.Log("Reward instantiated.");
        }
    }
}
}