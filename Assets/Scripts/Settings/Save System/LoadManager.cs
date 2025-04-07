using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        string savedScene = SaveSystem.Instance.GetValue<string>("scene");

        StartCoroutine(LoadSceneAndApplySave(savedScene));
    }

    private IEnumerator LoadSceneAndApplySave(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        yield return null;

        ApplySavedData();
    }

    private void ApplySavedData()
    {
        // Adicione outras alterações aqui, como restaurar estados de inimigos, inventário, etc.
    }

    private void OnApplicationQuit()
    {
        SaveSystem.Instance.SetValue("scene", SceneManager.GetActiveScene().name);
    }
}
