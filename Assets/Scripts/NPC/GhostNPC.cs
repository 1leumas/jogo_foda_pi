using UnityEngine;

public class GhostNPC : MonoBehaviour
{
    public float floatSpeed;
    public float floatHeight;
    private Vector3 startPosition;
    private float timeCounter = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;   
    }

    // Update is called once per frame
    void Update()
    {
        moveUpDown();
    }

    private void moveUpDown() {
        timeCounter += Time.deltaTime * floatSpeed;
        float newY = startPosition.y + (Mathf.Sin(timeCounter) * 0.5f + 0.5f) * floatHeight;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }
}
