using Unity.Android.Gradle;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed; // Velocidade do personagem
    private float initialMoveSpeed; // Velocidade inicial do personagem
    private Vector3 moveInput;
    private bool _isTalking;

    public bool IsTalking { get => _isTalking; set => _isTalking = value; }

    private void Start()
    {
        initialMoveSpeed = moveSpeed;

        string vectorString = SaveSystem.Instance.GetValue<string>("playerPosition"); // Initial position = (0, 0, -2)
        transform.position = StringToVector3(vectorString);
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

    private void OnApplicationQuit()
    {
        SaveSystem.Instance.SetValue("playerPosition", transform.position);
    }

    Vector3 StringToVector3(string str)
    {
        // Remove parênteses e divide a string pelos separadores ", "
        str = str.Trim('(', ')');
        string[] values = str.Split(',');

        // Converte os valores para float e retorna um Vector3
        return new Vector3(
            float.Parse(values[0], System.Globalization.CultureInfo.InvariantCulture),
            float.Parse(values[1], System.Globalization.CultureInfo.InvariantCulture),
            float.Parse(values[2], System.Globalization.CultureInfo.InvariantCulture)
        );
    }
}
