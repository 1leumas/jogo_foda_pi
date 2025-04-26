using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewGame : MonoBehaviour
{
    public GameObject laodingScreen;
    public Slider slider;
    
    public void LoadNewGame()
    {
        StartCoroutine(LoadAsync());
    }

    IEnumerator LoadAsync ()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);

        laodingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            Debug.Log(progress);

            slider.value = progress;

            yield return null;
        }
    }
}
