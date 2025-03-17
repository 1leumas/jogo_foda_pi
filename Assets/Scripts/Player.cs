using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidade do personagem
    private Rigidbody2D rb;
    private Vector3 moveInput;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Captura o input no eixo X (A/D) e no eixo Z (W/S)
        float moveX = Input.GetAxisRaw("Horizontal"); // A/D (←/→)
        float moveZ = Input.GetAxisRaw("Vertical");   // W/S (↑/↓)

        // Cria o vetor de movimento corretamente no espaço 3D (X e Z)
        moveInput = new Vector3(moveX, 0f, moveZ).normalized;
    }

    private void FixedUpdate()
    {
        // Como Rigidbody2D não aceita Z, usamos transform.position
        transform.position += moveInput * moveSpeed * Time.fixedDeltaTime;
    }
}
