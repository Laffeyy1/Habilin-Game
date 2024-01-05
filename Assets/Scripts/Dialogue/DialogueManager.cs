using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    public Image speakerImage;

    public GameObject dialogueUI;

    [Header("Objects to Show / Hide before dialogue")]
    public GameObject[] objectsToHideBefore;
    public GameObject[] objectsToShowBefore;

    [Header("Objects to Show / Hide after dialogue")]
    public GameObject[] objectsToHideAfter;
    public GameObject[] objectsToShowAfter;

    public bool isDone;
    bool isTyping;

    private StoryScene currentStoryScene;
    private Queue<StoryScene.Sentence> sentences;
    private ObjectiveHint objectiveHint;
    private Coroutine typingCoroutine; // Store the reference to the typing coroutine.

    void Start()
    {
        objectiveHint = GameObject.FindWithTag("Hint").GetComponent<ObjectiveHint>();

        if(dialogueUI == null)
        {
            GameObject dialogue = GameObject.FindGameObjectWithTag("Dialogue");
            dialogueUI = dialogue.transform.GetChild(0).gameObject;

            GameObject image = dialogueUI.transform.GetChild(0).gameObject;
            GameObject player = image.transform.GetChild(0).gameObject;

            speakerImage = player.transform.GetChild(0).GetComponent<Image>();
            nameText = dialogueUI.transform.GetChild(1).GetComponent<TMP_Text>();
            dialogueText = dialogueUI.transform.GetChild(2).GetComponent<TMP_Text>();
        }

        sentences = new Queue<StoryScene.Sentence>();
        isDone = true;
    }

    public void StartDialogue(StoryScene storyScene)
    {
        AudioManager am = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        am.DialogueNext();
        isTyping = true;
        // Deactivate objects to hide
        if (objectsToHideBefore != null)
        {
            foreach (GameObject obj in objectsToHideBefore)
            {
                if (obj != null)
                {
                    obj.SetActive(false);
                }
            }
        }

        if (objectsToShowBefore != null)
        {
            foreach (GameObject obj in objectsToShowBefore)
            {
                if (obj != null)
                {
                    obj.SetActive(true);
                }
            }
        }

        isDone = false;
        if (objectiveHint != null)
        {
            objectiveHint.state = false;
        }
        sentences.Clear();

        currentStoryScene = storyScene;
        foreach (StoryScene.Sentence sentence in storyScene.sentences)
        {
            sentences.Enqueue(sentence);
        }

        // Set the language based on PlayerPrefs
        SetLanguage(PlayerPrefs.GetString("Language", "English"));

        isTyping = false;
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        isTyping = true; 
        AudioManager am = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        am.DialogueNext();

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

        isTyping = false;
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
        dialogueUI.SetActive(false); // Disable the dialogue UI when the queue is empty.
        isDone = true;
        isTyping = false;
        if (currentStoryScene != null)
        {
            objectiveHint.hint = currentStoryScene.toHint;
            if (currentStoryScene.hasHint)
            {
                objectiveHint.state = true;
            }
        }

        // Deactivate objects to hide
        if (objectsToHideAfter != null)
        {
            foreach (GameObject obj in objectsToHideAfter)
            {
                if (obj != null)
                {
                    obj.SetActive(false);
                }
            }
        }

        if (objectsToShowAfter != null)
        {
            foreach (GameObject obj in objectsToShowAfter)
            {
                if (obj != null)
                {
                    obj.SetActive(true);
                }
            }
        }
    }
    private void SetLanguage(string language)
    {
        switch (language)
        {
            case "English":
                // Set your UI to display English text here
                break;

            case "Tagalog":
                // Set your UI to display Tagalog text here
                break;

            // You can add more cases for other languages if needed

            default:
                // Handle the default case or an unsupported language
                break;
        }
    }

    void Update()
    {
        if (!isDone)
        {
            if (!isTyping && Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                DisplayNextSentence();
            }
        }
    }
}