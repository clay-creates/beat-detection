using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GoalArea : MonoBehaviour
{
    [Header("End Game UI")]
    public GameObject endGameUI; // Assign End Game UI panel
    public TMP_Text finalTimeText;   // Assign Final Time UI Text
    private GameTimer gameTimer;

    void Start()
    {
        gameTimer = FindFirstObjectByType<GameTimer>(); // Find the timer script in the scene
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure only the player triggers this
        {
            EndGame();
        }
    }

    void EndGame()
    {
        gameTimer.StopTimer(); // Stop the timer
        float finalTime = gameTimer.GetFinalTime();

        // Format and display final time
        int minutes = Mathf.FloorToInt(finalTime / 60);
        int seconds = Mathf.FloorToInt(finalTime % 60);
        int milliseconds = Mathf.FloorToInt((finalTime * 100) % 100);
        finalTimeText.text = string.Format("Final Time: \n{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);

        Time.timeScale = 0f; // Pause the game
        endGameUI.SetActive(true); // Show End Game UI
        Cursor.lockState = CursorLockMode.None; // Unlock cursor for UI
        Cursor.visible = true;
    }
}
