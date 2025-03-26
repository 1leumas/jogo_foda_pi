using Unity.Android.Gradle;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public DynamicJoystick joystick;
    private float initialMoveSpeed;
    private Vector3 moveInput;
    private GameObject flashlight;
    private Rigidbody rb;
    private bool _isTalking;

    public bool IsTalking { get => _isTalking; set => _isTalking = value; }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        initialMoveSpeed = moveSpeed;
        flashlight = transform.Find("Light")?.gameObject;

        string vectorString = SaveSystem.Instance.GetValue<string>("playerPosition"); // Initial position = (0, 0, -2)
        transform.position = StringToVector3(vectorString);
    }

    private void Update()
    {
        if (IsTalking)
        {
            joystick.gameObject.SetActive(false);
            moveSpeed = 0;
        }
        else
        {
            joystick.gameObject.SetActive(true);
            moveSpeed = initialMoveSpeed;
        }
        // Captura o input no eixo X (A/D) e no eixo Z (W/S)
        float moveX = joystick.Horizontal; // A/D (←/→)
        float moveZ = joystick.Vertical; // W/S (↑/↓)

        // Cria o vetor de movimento corretamente no espaço 3D (X e Z)
        moveInput = new Vector3(moveX, 0f, moveZ).normalized;

        if (flashlight.activeSelf && moveInput.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveInput);
            flashlight.transform.rotation = Quaternion.Slerp(flashlight.transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }

    private void FixedUpdate()
    {
        if (moveInput.magnitude > 0.01f) // Apenas move se houver entrada
        {
            Vector3 newPosition = rb.position + moveInput * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(newPosition);
        }
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
