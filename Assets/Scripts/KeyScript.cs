using UnityEngine;

public class KeyScript : MonoBehaviour
{
    public GameObject key; // Der Schlüssel, der ein- oder ausgeblendet wird
    public GameManager gameManager; // Referenz auf den GameManager
    public HebelUmlegen hebelUmlegen; // Referenz auf HebelUmlegen, falls benötigt

    void Start()
    {
        if (key != null)
        {
            // Schlüssel zu Beginn deaktivieren
            key.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Kein Key zugewiesen!");
        }
    }

    void Update()
    {
        // Prüfen, ob das Spiel gewonnen wurde
        if (gameManager != null && gameManager.hasWon || hebelUmlegen != null && hebelUmlegen.isHebelUmgelegt)
        {
            if (key != null)
            {
                key.SetActive(true); // Schlüssel anzeigen
            }
        }
    }
}
