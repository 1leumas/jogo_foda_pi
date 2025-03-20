using Unity.Android.Gradle;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed; // Velocidade do personagem
    private float initialMoveSpeed; // Velocidade inicial do personagem
    private Rigidbody2D rb;
    private Vector3 moveInput;
    private bool _isTalking;

    public bool IsTalking { get => _isTalking; set => _isTalking = value; }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialMoveSpeed = moveSpeed;
    }

    private void Update()
    {
        if (IsTalking) {
            moveSpeed = 0;
        } else {
            moveSpeed = initialMoveSpeed;
        }
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
