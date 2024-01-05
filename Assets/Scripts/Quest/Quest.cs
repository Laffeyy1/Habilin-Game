using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class QuestReward
{
    public GameObject item; // Name of the item or reward
    public int quantity; // Quantity of the reward
    public Transform spawnLocation; // Position to instantiate the reward
}

[System.Serializable]
public class Quest
{
    public string questTitle;
    public string questDescription;
    public List<Objective> objectives = new List<Objective>();
    public List<QuestReward> rewards = new List<QuestReward>();

    public bool IsComplete()
    {
        foreach (var objective in objectives)
        {
            if (!objective.completed)
            {
                return false;
            }
        }
        return true;
    }

    public void CompleteObjective(string objective)
    {
        foreach (var obj in objectives)
        {
            if (obj.objectiveName == objective)
            {
                obj.completed = true;
                break;
            }
        }
    }
}