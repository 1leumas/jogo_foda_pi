using System;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Dialogue : MonoBehaviour
{
    public float dialogueRange;
    public LayerMask playerLayer;
    public DialogueSettings dialogue;
    public bool playerHit;

    private IconController iconCont;
    private List<string> sentences = new List<string>();
    private List<string> names = new List<string>();
    private List<Sprite> profiles = new List<Sprite>();
    public Collider[] hit;
    private string npcTag;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetNPCInfo();
        iconCont = FindFirstObjectByType<IconController>();
        npcTag = gameObject.tag;
    }

    void FixedUpdate()
    {
        ShowDialogue();
    }

    void ShowDialogue()
    {  
        hit = Physics.OverlapSphere(transform.position, dialogueRange, playerLayer);
        
        if (hit.Length > 0)
        {
            playerHit = true;
            iconCont.state = 1;
            iconCont.npc = npcTag;
        }
        else
        {
            playerHit = false;
            iconCont.state = 0;
        }
    }

    public void StartDialogue()
    {
        DialogueControl.instance.Speech(sentences.ToArray(), names.ToArray(), profiles.ToArray());
    }

    void GetNPCInfo()
    {
        for (int i = 0; i < dialogue.dialogues.Count; i++)
        {
            sentences.Add(dialogue.dialogues[i].sentence.portuguese);
            names.Add(dialogue.dialogues[i].actorName);
            profiles.Add(dialogue.dialogues[i].profile);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, dialogueRange);
    }
}
