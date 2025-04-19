using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;

public class MinigameObject : MonoBehaviour
{
    public float playRange;
    public LayerMask playerLayer;
    public bool playerHit;
    public SceneAsset gameScene;

    private Collider[] hit;
    [SerializeField] private bool activeLight;

    void Start()
    {
        transform.Find("Light").gameObject.SetActive(false);
    }
    void Update()
    {
        if (activeLight == true)
        {
            transform.Find("Light").gameObject.SetActive(true);
            
            ShowMinigame();
        }
    }

    void ShowMinigame()
    {
        hit = Physics.OverlapSphere(transform.position, playRange, playerLayer);

        if (hit.Length > 0 && !playerHit)
        {
            playerHit = true;

            ButtonController.Instance.ActivateBtn("Game", gameScene);
        }
        else if (hit.Length == 0 && playerHit)
        {
            playerHit = false;

            ButtonController.Instance.DeactivateBtn();
        }
    }

    public void StartMinigame()
    {
        //Minigame logic...
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, playRange);
    }
}
