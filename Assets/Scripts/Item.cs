using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    public float floatSpeed;
    public float floatHeight;
    public float dialogueRange;
    public LayerMask playerLayer;
    public Sprite spriteSelec;

    private IconController iconCont;
    private Sprite spriteBase;
    private SpriteRenderer spriteRenderer;
    private bool playerNearby = false;
    private Vector3 startPosition;
    private float timeCounter = 0;

    void Start()
    {
        startPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteBase = spriteRenderer.sprite;
        iconCont = FindFirstObjectByType<IconController>();
    }

    void Update()
    {
        DetectPlayer();

        if (playerNearby)
        {
            timeCounter += Time.deltaTime * floatSpeed;
            float newY = startPosition.y + (Mathf.Sin(timeCounter) * 0.5f + 0.5f) * floatHeight;
            transform.position = new Vector3(startPosition.x, newY, startPosition.z);
            iconCont.state = 2;
        }
    }

    void DetectPlayer()
    {
        Collider[] hit = Physics.OverlapSphere(transform.position, dialogueRange, playerLayer);

        if (hit.Length > 0 && !playerNearby)
        {
            playerNearby = true;
            timeCounter = -Mathf.PI / 2; // Evita resetar o tempo toda vez que o jogador é detectado
            spriteRenderer.sprite = spriteSelec;
        }
        else if (hit.Length == 0 && playerNearby)
        {
            playerNearby = false;
            transform.position = startPosition;
            spriteRenderer.sprite = spriteBase;
        }
    }

    public void PickItem()
    {
        Inventory.Instance.hasFlashlight = true;
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, dialogueRange);
    }
}
