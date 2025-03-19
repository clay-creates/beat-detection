using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class EndGameUI : MonoBehaviour
{
    public Button menuButton;
    public Button replayButton;
    public Button quitButton;

    public GameObject endGameUI; // Assign the End Game UI panel

    private void Start()
    {
        menuButton.onClick.AddListener(ReturnToMainMenu);
        replayButton.onClick.AddListener(RestartGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Resume time
        SceneManager.LoadScene("Maze"); // Reload current level
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f; // Resume time
        SceneManager.LoadScene("Main Menu"); // Load Main Menu scene (Make sure it's added to Build Settings)
    }

    public void QuitGame()
    {
        Application.Quit(); // Quit the game
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stop play mode in Unity Editor
#endif
    }
}
