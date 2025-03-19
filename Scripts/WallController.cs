using UnityEngine;
using System.Collections;

public class WallController : MonoBehaviour
{
    [Header("Audio Reactive Settings")]
    public int audioBandIndex = 2;  // Which frequency band controls this wall
    public float beatThreshold = 0.5f; // How strong the beat must be to activate the wall
    public float cooldownTime = 1f; // Prevents constant triggering

    [Header("Wall Movement Settings")]
    public float moveDistance = 3f; // How far the wall moves down
    public float moveSpeed = 2f;    // How fast the wall moves
    public float waitTime = 2f;     // How long the wall stays down

    private Vector3 originalPosition;
    private Vector3 loweredPosition;
    private bool isMoving = false;
    private float lastTriggerTime = 0f;

    void Start()
    {
        originalPosition = transform.position;
        loweredPosition = originalPosition - new Vector3(0, moveDistance, 0); // Move down by moveDistance
    }

    void Update()
    {
        if (AudioPeer._audioBandBuffer[audioBandIndex] > beatThreshold && Time.time > lastTriggerTime + cooldownTime)
        {
            TriggerWall();
            lastTriggerTime = Time.time; // Update cooldown
        }
    }

    public void TriggerWall()
    {
        if (!isMoving)
        {
            StartCoroutine(MoveWall());
        }
    }

    IEnumerator MoveWall()
    {
        isMoving = true;

        // Move down
        while (Vector3.Distance(transform.position, loweredPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, loweredPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // Wait before rising
        yield return new WaitForSeconds(waitTime);

        // Move up
        while (Vector3.Distance(transform.position, originalPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        isMoving = false;
    }
}
