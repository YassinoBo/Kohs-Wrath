using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenManager : MonoBehaviour
{
    public GameObject endScreenCanvas;

    // Aktiviert den EndScreen
    public void ShowEndScreen()
    {
        endScreenCanvas.SetActive(true);
        Time.timeScale = 0f; // Spiel pausieren
        SceneManager.LoadScene("Credits");
    }

    // Neustart des Spiels
    public void RestartGame()
    {
        Time.timeScale = 1f; // Spiel fortsetzen
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Beenden des Spiels
    public void QuitGame()
    {
        Debug.Log("Spiel beendet!");
        Application.Quit();
    }
}