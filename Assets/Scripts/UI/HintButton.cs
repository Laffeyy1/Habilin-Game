using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintButton : MonoBehaviour
{
    public QuestUI questUI;
    public ObjectiveHint objectiveHint;

    public void hintButtonClick()
    {
        if (questUI.questHint)
        {
            questUI.questHintButtonEnable();
        }
        else if (objectiveHint.objectiveHint)
        {
            objectiveHint.hintButtonEnable();
        }
        else
        {
            Debug.Log("Ewan ko na talga");
        }
    }
}