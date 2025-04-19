using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Vector3 playerPosition;
    public int fuses;
    public int gameState;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
