using System.Collections;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueControl : MonoBehaviour
{
    [System.Serializable]
    public enum idiom
    {
        pt
    }

    public idiom language;

    [Header("Components")]
    public GameObject dialogueObj;
    public Image profileSprite;
    public Image Waypoint;
    public TextMeshProUGUI speechText;
    public TextMeshProUGUI actorNametext;
    public Button skipButton;
    public Button speechButton;
    public GameObject guideGhost;

    [Header("Settings")]
    public float typingSpeed;

    private bool isShowing;
    private bool isPlayerDialogue;
    private int index;
    private int state;
    private string[] sentences;
    private string[] names;
    private Sprite[] profiles;
    private Player player;

    public static DialogueControl instance;

    public bool IsShowing { get => isShowing; set => isShowing = value; }

    void Start()
    {
        player = FindFirstObjectByType<Player>();
    }

    // Awake is called before all Start() no the scene hierarchy
    private void Awake()
    {
        instance = this;
    }

    IEnumerator TypeSentence()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            speechText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        skipButton.gameObject.SetActive(true);
    }

    public void NextSentence()
    {
        if (index < sentences.Length - 1)
        {
            index++;
            speechText.text = "";
            actorNametext.text = null;
            profileSprite.sprite = null;
            skipButton.gameObject.SetActive(false);
            StartCoroutine(TypeSentence());
            actorNametext.text = names[index];
            profileSprite.sprite = profiles[index];
        }
        else
        {
            speechText.text = "";
            actorNametext.text = null;
            profileSprite.sprite = null;
            index = 0;
            dialogueObj.SetActive(false);
            sentences = null;
            IsShowing = false;
            player.IsTalking = false;
            player.joystick.OnPointerUp(null);
            if (guideGhost.activeSelf)
            {
                guideGhost.SetActive(false);
            }
            if (!isPlayerDialogue)
            {
                speechButton.gameObject.SetActive(true);    
            }       
            Waypoint.gameObject.SetActive(true);
            GameManager.Instance.SetState(state + 1);
        }
    }

    public void Speech(string[] txt, string[] name, Sprite[] img, int nextState, bool playerDialogue)
    {
        if (!IsShowing)
        {
            dialogueObj.SetActive(true);
            sentences = txt;
            names = name;
            profiles = img;
            state = nextState;
            skipButton.gameObject.SetActive(false);
            speechButton.gameObject.SetActive(false);
            Waypoint.gameObject.SetActive(false);

            StartCoroutine(TypeSentence());
            actorNametext.text = names[index];
            profileSprite.sprite = profiles[index];
            IsShowing = true;
            player.IsTalking = true;
            isPlayerDialogue = playerDialogue;
        }
    }
}
