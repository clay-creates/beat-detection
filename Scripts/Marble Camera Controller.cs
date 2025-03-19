using UnityEngine;

public class MarbleCameraController : MonoBehaviour
{
    [Header("Target & Offset")]
    public Transform target; // The marble player
    public Vector3 offset = new Vector3(0f, 5f, -10f); // Default camera position offset

    [Header("Camera Follow Settings")]
    public float smoothSpeed = 5f; // How smoothly the camera follows

    [Header("Rotation Settings")]
    public float rotationSpeed = 200f; // Speed of rotation
    private float yaw = 0f; // Horizontal rotation
    private float pitch = 10f; // Vertical rotation
    public float minPitch = -20f, maxPitch = 60f; // Clamp vertical rotation

    void Start()
    {
        // Initialize rotation based on current camera direction
        Vector3 angles = transform.eulerAngles;
        yaw = angles.y;
        pitch = angles.x;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Rotate camera with right-click
        if (Input.GetMouseButton(1)) // Right-click held
        {
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

            yaw += mouseX;
            pitch -= mouseY; // Invert Y for natural control
            pitch = Mathf.Clamp(pitch, minPitch, maxPitch); // Clamp up/down rotation
        }

        // Calculate new camera position based on rotation
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 desiredPosition = target.position + rotation * offset;

        // Smoothly move camera to position
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.LookAt(target.position);
    }
}
