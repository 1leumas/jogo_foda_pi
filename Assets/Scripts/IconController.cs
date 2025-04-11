using UnityEngine;
using UnityEngine.UI;

public class IconController : MonoBehaviour
{
    public Button btn;
    private int _state;
    private string _npc;
    private Animator anim;
    private int lastState = -1;
    private bool wasBtnActive = true;

    public string npc { get => _npc; set => _npc = value; }
    public int state { get => _state; set => _state = value; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = btn.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state != lastState)
        {
            SwitchAnim();
            lastState = state;
        }
        
        if (btn.gameObject.activeSelf && !wasBtnActive)
        {
            anim.SetInteger("icon", state);
        }

        wasBtnActive = btn.gameObject.activeSelf;
    }

    void SwitchAnim()
    {
        switch (state)
        {
            case (1):
                btn.gameObject.SetActive(true);
                anim.SetInteger("icon", state);
                break;
            case (2):
                btn.gameObject.SetActive(true);
                anim.SetInteger("icon", state);
                break;
            case (3):
                btn.gameObject.SetActive(true);
                anim.SetInteger("icon", state);
                break;
            default:
                btn.gameObject.SetActive(false);
                break;
        }
    }

    public void ButtonPress()
    {
        switch (state)
        {
            case (1):
                GameObject npcObj = GameObject.FindWithTag(npc);
                NPC_Dialogue npcScript = npcObj.GetComponent<NPC_Dialogue>();
                npcScript.StartDialogue();
                break;
            case (2):
                break;
            default:
                btn.gameObject.SetActive(false);
                break;
        }
    }
}
