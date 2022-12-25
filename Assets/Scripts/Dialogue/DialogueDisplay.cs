using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueDisplay : MonoBehaviour
{
    public GameObject dialogueCanvas;
    public TextMeshProUGUI textDisplay;
    public Dialogue dialogue;
    private int index;

    [SerializeField] private Button continueButton;

    IEnumerator Type()
    {
        foreach (char letter in dialogue.sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            if (dialogue.sentences[index] == textDisplay.text)
            {
                continueButton.gameObject.SetActive(true);
                yield return null;
            }
            yield return new WaitForSeconds(0.02f);
        }
    }

    public void StartConversation()
    {
        dialogueCanvas.SetActive(true);
        index = 0;
        continueButton.onClick.RemoveAllListeners();
        continueButton.onClick.AddListener(NextSentence);
        NextSentence();
    }

    public void EndConversation()
    {
        continueButton.onClick.RemoveAllListeners();
        dialogueCanvas.SetActive(false);
    }

    public void NextSentence()
    {
        continueButton.gameObject.SetActive(false);
        if (index < dialogue.sentences.Count - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            textDisplay.text = "";
            EndConversation();
        }
    }
}
