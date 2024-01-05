using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    public GameObject creditUI;

    public Animator animator;

    public void OnFinishCredit()
    {
        creditUI.SetActive(false);
        animator.SetBool("open", false);
    }

    public void OnClose()
    {
        creditUI.SetActive(false);
        animator.SetBool("open", false);
    }

    public void OnOpen()
    {
        creditUI.SetActive(true);
        animator.SetBool("open", true);
    }
}
