using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    PlayerInput playerInput;

    private InputAction _touchPressAction;


    public Image actorImage;
    public TextMeshProUGUI actorName;
    public TextMeshProUGUI actorLine;
    public GameObject dialogueArea;
    public GameObject dialoguePanel;

    SO_Dialogue_Messages currentMessage;
    int activeMessage = 0;
    public static bool isConversationActive;


    private void Awake()
    {
        dialogueArea.transform.localScale = Vector3.zero;
        dialoguePanel.SetActive(false);
        actorImage.transform.localScale = Vector3.zero;
        playerInput = GetComponent<PlayerInput>();
        _touchPressAction = playerInput.actions["TouchPress"];
    }

    public void OpenDialogue(SO_Dialogue_Messages messages)
    {
        currentMessage = messages;
        _touchPressAction.performed += TouchPress;
        DisplayMessage();
        dialoguePanel.SetActive(true);
        dialogueArea.LeanScale(Vector3.one, 0.5f);
        actorImage.gameObject.LeanScale(Vector3.one, 0.5f);

    }

    public void DisplayMessage()
    {
        Line messageToDisplay = currentMessage.lines[activeMessage];
        actorLine.text = messageToDisplay.text;
    }

    public void NextMessage()
    {
        activeMessage++;
        if (activeMessage < currentMessage.lines.Length)
        {
            DisplayMessage();
        }
        else
        {
            //SaveMessage as Complete to save 
            currentMessage.isCompleted = true;
            SaveStoryProgression.instance.SaveLoadStory(currentMessage);
            Debug.Log("Conversation ended");
            _touchPressAction.performed -= TouchPress;
            dialoguePanel.SetActive(false);

            dialogueArea.transform.localScale = Vector3.zero;
            actorImage.transform.localScale = Vector3.zero;
        }
    }

    void TouchPress(InputAction.CallbackContext context)
    {
        Debug.Log("Touch");
        NextMessage();
    }



}
