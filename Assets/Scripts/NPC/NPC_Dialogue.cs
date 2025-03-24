using System;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Dialogue : MonoBehaviour
{
    public float dialogueRange;
    public LayerMask playerLayer;
    public DialogueSettings dialogue;
    public bool playerHit;

    private List<string> sentences = new List<string>();
    private List<string> names = new List<string>();
    private List<Sprite> profiles = new List<Sprite>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetNPCInfo();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            ProcessTouch(touch.position);
        }
        else if (Input.GetMouseButtonDown(0))
        {
            ProcessTouch(Input.mousePosition);
        }
    }

    void FixedUpdate()
    {
        ShowDialogue();
    }

    void ShowDialogue()
    {
        Collider[] hit = Physics.OverlapSphere(transform.position, dialogueRange, playerLayer);

        if (hit.Length > 0)
        {
            playerHit = true;
        }
        else
        {
            playerHit = false;
        }
    }

    void ProcessTouch(Vector2 touchPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == gameObject && playerHit)
            {
                DialogueControl.instance.Speech(sentences.ToArray(), names.ToArray(), profiles.ToArray());
            }
        }
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
