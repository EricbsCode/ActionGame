using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    DialogueController dialogueController;
    public Dialogue[] dialogues;
    int currentDia = 0;

    private void Awake()
    {
        dialogueController = FindObjectOfType<DialogueController>();
    }

    private void OnMouseDown()
    {
        dialogueController.StartDialogue(dialogues[currentDia]);
        currentDia = (currentDia + 1) % dialogues.Length;
    }

}
