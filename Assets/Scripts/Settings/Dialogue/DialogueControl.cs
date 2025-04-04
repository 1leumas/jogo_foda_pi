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
    public TextMeshProUGUI speechText;
    public Button skipButton;
    public TextMeshProUGUI actorNametext;

    [Header("Settings")]
    public float typingSpeed;

    private bool isShowing;
    private int index;
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
        }
    }

    public void Speech(string[] txt, string[] name, Sprite[] img)
    {
        if (!IsShowing)
        {
            dialogueObj.SetActive(true);
            sentences = txt;
            names = name;
            profiles = img;
            skipButton.gameObject.SetActive(false);

            StartCoroutine(TypeSentence());
            actorNametext.text = names[index];
            profileSprite.sprite = profiles[index];
            IsShowing = true;
            player.IsTalking = true;
        }
    }
}
