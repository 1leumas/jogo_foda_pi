using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    private static SaveSystem _instance;
    private Dictionary<string, object> _saveData;
    private string _saveFilePath;
    private const string DEFAULT_FILENAME = "savedata.json";
    
    [SerializeField]
    private bool _saveInScriptFolder = true;

    public static SaveSystem Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject saveSystemObject = new GameObject("SaveSystem");
                _instance = saveSystemObject.AddComponent<SaveSystem>();
                DontDestroyOnLoad(saveSystemObject);
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        InitializeSaveFilePath();
        LoadSaveFile();
    }

    private void InitializeSaveFilePath()
    {
        if (_saveInScriptFolder)
        {
            // Save in the Assets folder (only works in editor)
            #if UNITY_EDITOR
            _saveFilePath = Path.Combine("Assets", DEFAULT_FILENAME);
            #else
            // Fall back to persistent data path in builds
            _saveFilePath = Path.Combine(Application.persistentDataPath, DEFAULT_FILENAME);
            Debug.LogWarning("Saving in script folder only works in the editor. Using persistent data path instead.");
            #endif
        }
        else
        {
            // Use Unity's persistent data path (works in builds)
            _saveFilePath = Path.Combine(Application.persistentDataPath, DEFAULT_FILENAME);
        }
        
        Debug.Log($"Save file path: {_saveFilePath}");
    }

    private void LoadSaveFile()
    {
        if (File.Exists(_saveFilePath))
        {
            string jsonData = File.ReadAllText(_saveFilePath);
            _saveData = JsonUtility.FromJson<SaveData>(jsonData).ToDictionary();
        }
        else
        {
            _saveData = new Dictionary<string, object>();
            SaveToFile();
        }
    }

    private void SaveToFile()
    {
        SaveData saveData = new SaveData(_saveData);
        string jsonData = JsonUtility.ToJson(saveData, true);
        
        // Create directory if it doesn't exist
        string directory = Path.GetDirectoryName(_saveFilePath);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
        
        File.WriteAllText(_saveFilePath, jsonData);
        Debug.Log($"Save data written to: {_saveFilePath}");
    }

    public T GetValue<T>(string key, T defaultValue = default)
    {
        if (_saveData.TryGetValue(key, out object value))
        {
            if (value is T typedValue)
            {
                return typedValue;
            }

            // Handle type conversion for basic types
            try
            {
                return (T)System.Convert.ChangeType(value, typeof(T));
            }
            catch
            {
                Debug.LogWarning($"Could not convert saved value for key '{key}' to type {typeof(T).Name}");
                return defaultValue;
            }
        }

        return defaultValue;
    }

    public void SetValue<T>(string key, T value)
    {
        _saveData[key] = value;
        SaveToFile();
    }

    public void DeleteKey(string key)
    {
        if (_saveData.ContainsKey(key))
        {
            _saveData.Remove(key);
            SaveToFile();
        }
    }

    public void ClearAllData()
    {
        _saveData.Clear();
        SaveToFile();
    }
    
    public void ChangeSavePath(string newPath)
    {
        string oldPath = _saveFilePath;
        _saveFilePath = newPath;
        
        Debug.Log($"Save path changed from {oldPath} to {_saveFilePath}");
        
        // Save to the new location
        SaveToFile();
    }

    // Helper class for serialization
    [System.Serializable]
    private class SaveData
    {
        public List<SerializedKeyValuePair> entries = new List<SerializedKeyValuePair>();

        public SaveData(Dictionary<string, object> dictionary)
        {
            foreach (var pair in dictionary)
            {
                entries.Add(new SerializedKeyValuePair(pair.Key, pair.Value.ToString()));
            }
        }

        public Dictionary<string, object> ToDictionary()
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            foreach (var entry in entries)
            {
                result[entry.key] = entry.value;
            }
            return result;
        }
    }

    [System.Serializable]
    private class SerializedKeyValuePair
    {
        public string key;
        public string value;

        public SerializedKeyValuePair(string key, string value)
        {
            this.key = key;
            this.value = value;
        }
    }
}