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
        pt,
        en,
        sp
    }

    public idiom language;

    [Header("Components")]
    public GameObject dialogueObj;
    public Image profileSprite;
    public TextMeshProUGUI speechText;
    public Button skipButton;
    public Text actorNametext;

    [Header("Settings")]
    public float typingSpeed;

    private bool isShowing;
    private int index;
    private string[] sentences;
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
            skipButton.gameObject.SetActive(false);
            StartCoroutine(TypeSentence());
        }
        else
        {
            speechText.text = "";
            index = 0;
            dialogueObj.SetActive(false);
            sentences = null;
            IsShowing = false;
            player.IsTalking = false;
        }
    }

    public void Speech(string[] txt)
    {
        if (!IsShowing)
        {
            dialogueObj.SetActive(true);
            sentences = txt;
            StartCoroutine(TypeSentence());
            IsShowing = true;
            player.IsTalking = true;
        }
    }
}
