using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Burst.CompilerServices;

public class QuestUI : MonoBehaviour
{
    public TMP_Text questTitleText;
    public TMP_Text questDescriptionText;
    public TMP_Text objectiveText;
    public Animator animator;
    public bool state;
    public bool questHint;

    DialogueManager dialogueManager;
    private bool previousState = false;
    private void Start()
    {
        // Initialize UI elements with default values or hide them
        questTitleText.text = "";
        questDescriptionText.text = "";
        objectiveText.text = "";
    }

    public void UpdateUI(Quest quest)
    {
        // Update the UI with quest information
        questTitleText.text = "Quest: " + quest.questTitle;
        questDescriptionText.text = "Description: " + quest.questDescription;
        UpdateObjectiveText(quest);
        questHint = true;
        state = true;
    }

    public void UpdateObjectiveText(Quest quest)
    {
        // Clear the existing objective text
        objectiveText.text = "Objectives:\n";

        // Iterate through the objectives and display their status
        foreach (var objective in quest.objectives)
        {
            state = !objective.completed; // Set state to false when the objective is complete

            string status = objective.completed ? "Completed" : "Incomplete";
            objectiveText.text += "- " + objective.objectiveName + ": " + status + "\n";
        }
    }

    private void Update()
    {
        if (animator != null)
        {

            if (state != previousState)
            {
                if (state)
                {
                    animator.SetTrigger("bOpen");
                }
                else
                {
                    animator.SetTrigger("bClose");
                }
            }

            previousState = state;
        }
    }
    public void questHintButtonEnable()
    {
        state = true;
    }

    public void questHintButtonDisable()
    {
        state = false;
        questHint = true;
    }
}
