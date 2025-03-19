using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    [Header("Player Respawn Settings")]
    public Transform spawnPoint; // Assign the player�s respawn position

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure only the player triggers this
        {
            RespawnPlayer(other.gameObject);
        }
    }

    void RespawnPlayer(GameObject player)
    {
        Rigidbody rb = player.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero; // Reset velocity to avoid momentum issues
            rb.angularVelocity = Vector3.zero;
        }

        player.transform.position = spawnPoint.position; // Move player to spawn
    }
}
