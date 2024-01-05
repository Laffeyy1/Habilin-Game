using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneDialogue : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    public Image speakerImage;

    public GameObject dialogueUI;

    [Header("Objects to Show / Hide after dialogue")]
    public GameObject[] objectsToHide;
    public GameObject[] objectsToShow;

    private bool isDone;

    private StoryScene currentStoryScene;
    private Queue<StoryScene.Sentence> sentences;
    private Coroutine typingCoroutine; // Store the reference to the typing coroutine.

    void Start()
    {
        sentences = new Queue<StoryScene.Sentence>();
        isDone = true;
    }

    public void StartDialogue(StoryScene storyScene)
    {
        Debug.Log("Starting dialogue...");
        isDone = false;

        sentences.Clear();

        currentStoryScene = storyScene;
        foreach (StoryScene.Sentence sentence in storyScene.sentences)
        {
            sentences.Enqueue(sentence);
        }

        // Set the language based on PlayerPrefs
        SetLanguage(PlayerPrefs.GetString("Language", "English"));

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (typingCoroutine != null)
        {
            // If a typing coroutine is running, stop it and show the entire sentence.
            StopCoroutine(typingCoroutine);
            dialogueText.text = sentences.Count > 0 ? GetNextSentenceText(sentences.Peek()) : string.Empty; // Show the entire sentence.
        }

        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        StoryScene.Sentence sentence = sentences.Dequeue();

        // Display speaker's name
        speakerImage.sprite = sentence.speaker != null ? sentence.speaker.speakerSprite : null;
        nameText.text = sentence.speaker != null ? sentence.speaker.speakerName : "";

        typingCoroutine = StartCoroutine(TypeSpeed(GetNextSentenceText(sentence)));
    }

    IEnumerator TypeSpeed(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }

        typingCoroutine = null; // Clear the coroutine reference.
    }

    void EndDialogue()
    {
        Debug.Log("End of conversation");
        dialogueUI.SetActive(false); // Disable the dialogue UI when the queue is empty.
        isDone = true;

        // Deactivate objects to hide
        if (objectsToHide != null)
        {
            foreach (GameObject obj in objectsToHide)
            {
                obj.SetActive(false);
            }
        }

        if (objectsToShow != null)
        {
            // Activate objects to show
            foreach (GameObject obj in objectsToShow)
            {
                obj.SetActive(true);
            }
        }
    }

    private string GetNextSentenceText(StoryScene.Sentence sentence)
    {
        // Determine which language to display based on PlayerPrefs
        string selectedLanguage = PlayerPrefs.GetString("Language", "English");

        if (selectedLanguage == "Tagalog" && !string.IsNullOrEmpty(sentence.tagalogText))
        {
            return sentence.tagalogText;
        }
        else
        {
            return sentence.englishText;
        }
    }

    private void SetLanguage(string language)
    {
        // You can add language-specific settings here, if needed
    }

    void Update()
    {
        if (!isDone)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                DisplayNextSentence();
            }
        }
    }
}
