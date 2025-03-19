using UnityEngine;

public class PillarController : MonoBehaviour
{
    public int frequencyBand = 3; // Choose which frequency band (0-7) controls this pillar
    public float scaleMultiplier = 2.0f; // Max size multiplier for scaling
    public float rotationSpeed = 30f; // How fast it rotates on beat
    public bool enableScaling = true; // Toggle scaling effect
    public bool enableRotation = false; // Toggle rotation effect
    public Vector3 rotationAxis = Vector3.up; // Choose axis for rotation

    private Vector3 initialScale;
    private Quaternion initialRotation;

    void Start()
    {
        initialScale = transform.localScale;
        initialRotation = transform.rotation;
    }

    void Update()
    {
        float intensity = AudioPeer._audioBandBuffer[frequencyBand]; // Get beat intensity

        if (enableScaling)
        {
            // Scale the pillar based on the beat
            float newScale = Mathf.Lerp(initialScale.y, initialScale.y * scaleMultiplier, intensity);
            transform.localScale = new Vector3(initialScale.x, newScale, initialScale.z);
        }

        if (enableRotation)
        {
            // Rotate the pillar based on the beat
            float rotationAmount = Mathf.Lerp(0, rotationSpeed, intensity) * Time.deltaTime;
            transform.Rotate(rotationAxis * rotationAmount);
        }
    }
}
