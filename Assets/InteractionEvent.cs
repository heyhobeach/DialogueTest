using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionEvent : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] DialogueEvent dialogue;

    public Dialogue[] GetDialogue()
    {
        dialogue.dialouses = DatabaseManager.instance.GetDialogues((int)dialogue.line.x, (int)dialogue.line.y);
        return dialogue.dialouses;
    }
}
