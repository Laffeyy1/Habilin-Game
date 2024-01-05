using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator transition;

    public float transitionIime = 1f;

    AudioManager audioManager;
    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        audioManager.playMainMenuBGM();
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("App quit");
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionIime);

        SceneManager.LoadScene(levelIndex);

    }
}
