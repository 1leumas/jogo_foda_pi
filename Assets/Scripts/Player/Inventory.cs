using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }
    public bool hasFlashlight;

    private GameObject flashLight;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        flashLight = transform.Find("Light").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasFlashlight)
        {
            flashLight.SetActive(true);
        }
    }
}
