using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class RadioMinigame : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject endgamePanel;
    public TextMeshProUGUI endgameText;
    public Transform livesObj;
    public int lives;
    public GameObject wordPrefab;
    public List<RectTransform> slots;
    public List<string> sentences;
    public List<string> translatedSentences;
    public List<string> randomWords;
    [HideInInspector] public int score = 0;

    private List<GameObject> lifes = new List<GameObject>();
    private bool playing;

    public bool Playing { get => playing; set => playing = value; }

    void Start()
    {
        foreach (Transform life in livesObj)
        {
            lifes.Add(life.gameObject);
        }
    }

    private void Update()
    {
        if (score >= 4 && playing)
        {
            Win();
        }
    }

    public void GoBack()
    {
        SceneManager.LoadScene("Game");
    }

    public void StartGame()
    {
        lives = 3;
        score = 0;

        GameObject answerSlot = GameObject.Find("Answer Slot");
        GameObject questionSlot = GameObject.Find("Question Slot");
        answerSlot.GetComponent<TextMeshProUGUI>().text = "";

        int sentenceIndex = Random.Range(0, sentences.Count);
        questionSlot.GetComponent<TextMeshProUGUI>().text = sentences[sentenceIndex];

        string[] words = translatedSentences[sentenceIndex].Split(' ');

        List<RectTransform> avaliableSlots = slots;

        foreach (string word in words)
        {
            int slotIndex = Random.Range(0, avaliableSlots.Count);

            GameObject currentWord = Instantiate(wordPrefab, slots[slotIndex].position, Quaternion.identity, slots[slotIndex]);
            currentWord.GetComponent<SelectableWords>().word = word;
            currentWord.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = word;

            avaliableSlots.RemoveAt(slotIndex);
        }

        List<string> avaliablewords = randomWords;
        foreach (RectTransform slot in avaliableSlots)
        {
            int wordIndex = Random.Range(0, avaliablewords.Count);

            GameObject currentWord = Instantiate(wordPrefab, slot.position, Quaternion.identity, slot);
            currentWord.GetComponent<SelectableWords>().word = avaliablewords[wordIndex];
            currentWord.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = avaliablewords[wordIndex];

            avaliablewords.RemoveAt(wordIndex);
        }

        foreach (Transform life in livesObj)
        {
            life.gameObject.SetActive(true);
            lifes.Add(life.gameObject);
        }
        playing = true;
        menuPanel.SetActive(false);
        endgamePanel.SetActive(false);
    }

    public void Hit()
    {
        lives--;
        lifes[lifes.Count - 1].SetActive(false);
        lifes.RemoveAt(lifes.Count - 1);
        if (lives <= 0)
        {
            Lose();
        }
    }

    public void EndGame(bool victory)
    {
        endgamePanel.SetActive(true);

        if (victory)
        {
            endgameText.text = "Vitória!";
        }
        else
        {
            endgameText.text = "Derrota";
        }
    }

    void Win()
    {
        playing = false;
        EndGame(true);

        if (GameManager.Instance.gameState == 18)
        {
            GameManager.Instance.gameState++;
        }
    }

    void Lose()
    {
        playing = false;
        EndGame(false);
    }
}
