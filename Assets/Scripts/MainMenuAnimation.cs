using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuAnimation : MonoBehaviour
{
    public Animator transition;

    public float transitionIime = 1f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LoadAnimation();
        }
    }

    IEnumerator LoadAnimation()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionIime);
    }
}
