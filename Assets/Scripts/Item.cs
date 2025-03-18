using System;
using Unity.Android.Gradle;
using UnityEngine;

public class Item : MonoBehaviour
{
    public float floatSpeed = 5f;
    public float floatHeight = 0.5f;
    public Sprite spriteSelec;

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
    }

    void Update()
    {
        if (playerNearby)
        {
            timeCounter += Time.deltaTime * floatSpeed;
            float newY = startPosition.y + (Mathf.Sin(timeCounter) * 0.5f + 0.5f) * floatHeight;
            transform.position = new Vector3(startPosition.x, newY, startPosition.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            timeCounter = -Mathf.PI / 2;
            spriteRenderer.sprite = spriteSelec;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            timeCounter = 0;
            transform.position = startPosition;
            spriteRenderer.sprite = spriteBase;
        }
    }
}
