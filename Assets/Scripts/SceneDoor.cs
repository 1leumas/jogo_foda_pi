using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDoor : MonoBehaviour
{
    [SerializeField] private string destinoCena;
    private Player player;

    private void Start()
    {
        player = FindFirstObjectByType<Player>();
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            ProcessTouch(touch.position);
        }
        else if (Input.GetMouseButtonDown(0))
        {
            ProcessTouch(Input.mousePosition);
        }
    }

    void ProcessTouch(Vector2 touchPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform == transform && Vector3.Distance(player.transform.position, transform.position) < 2.5f)
            {
                DoorInteract();
            }
        }
    }

    private void DoorInteract()
    {
        Vector3 pos = transform.position + transform.forward * 4f;
        string posString = pos.ToString();
        SaveSystem.Instance.SetValue("playerPosition", posString);
        SaveSystem.Instance.SetValue("scene", destinoCena);
        SceneManager.LoadScene(destinoCena);
    }
}
