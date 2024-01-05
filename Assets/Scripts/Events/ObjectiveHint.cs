using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectiveHint : MonoBehaviour
{
    public Animator animator;
    public TMP_Text hintDisplay;
    public string hint;
    public bool state;
    public bool objectiveHint;

    private bool previousState = false;
    private void Update()
    {
        if(animator != null)
        {
            hintDisplay.SetText(hint);

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
    
    public void hintButtonEnable()
    {
        state = true;
    }

    public void hintButtonDisable()
    {
        state = false;
        objectiveHint = true;
    }
}
