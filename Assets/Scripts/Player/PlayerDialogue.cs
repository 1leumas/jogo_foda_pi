using UnityEngine;
using System.Collections.Generic;

public class PlayerDialogue : MonoBehaviour
{
    public DialogueSettings dialogue;

    private List<string> sentences = new List<string>();
    private List<string> names = new List<string>();
    private List<Sprite> profiles = new List<Sprite>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetInfo();
    }

    public void StartDialogue()
    {
        DialogueControl.instance.Speech(sentences.ToArray(), names.ToArray(), profiles.ToArray());
    }

    void GetInfo()
    {
        for (int i = 0; i < dialogue.dialogues.Count; i++)
        {
            sentences.Add(dialogue.dialogues[i].sentence.portuguese);
            names.Add(dialogue.dialogues[i].actorName);
            profiles.Add(dialogue.dialogues[i].profile);
        }
    }
}
