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
        //transform.position = StringToVector3(vectorString);
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

        float moveX = joystick.Horizontal;
        float moveZ = joystick.Vertical;

        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        moveInput = (forward * moveZ + right * moveX).normalized;

        if (flashlight != null)
        {
            if (flashlight.activeSelf && moveInput.magnitude > 0.1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveInput);
                flashlight.transform.rotation = Quaternion.Slerp(flashlight.transform.rotation, targetRotation, Time.deltaTime * 10f);
            }
        }
    }

    private void FixedUpdate()
    {
        if (moveInput.magnitude > 0.01f)
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
        str = str.Trim('(', ')');
        string[] values = str.Split(',');

        return new Vector3(
            float.Parse(values[0], System.Globalization.CultureInfo.InvariantCulture),
            float.Parse(values[1], System.Globalization.CultureInfo.InvariantCulture),
            float.Parse(values[2], System.Globalization.CultureInfo.InvariantCulture)
        );
    }
}
