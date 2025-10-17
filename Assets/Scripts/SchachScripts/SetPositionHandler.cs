using UnityEngine;

public class SetPositionHandler : MonoBehaviour
{
    // Die Figur, die bewegt werden soll
    public GameObject figure;

    // Variable zum Speichern der ursprünglichen Position
    private Vector3 originalPosition;

    // Referenz für die "Platte" (Knopf-Oberfläche)
    public Transform buttonTransform;

    // Bewegung des Knopfes beim Drücken
    public float buttonPressOffset = -0.0078f; // Bewegung nach unten
    public float buttonResetDelay = 0.5f;      // Zeit bis der Knopf zurückspringt (in Sekunden)

    private SoundScript sound;
    
    public GameManager gameManager;

    
    // Wird aufgerufen, bevor das Spiel startet
    private void Start()
    {
        sound = SoundScript.Instance;
        if (figure != null)
        {
            // Speichert die ursprüngliche Position der Figur
            originalPosition = figure.transform.position;
        }
        else
        {
            Debug.LogWarning("Keine Figur zugewiesen!");
        }
    }

    // Wird aufgerufen, wenn der Button angeklickt wird
    private void OnMouseDown()
    {
        // Verhindern, dass sich die Figur bewegt, wenn das Spiel gewonnen ist oder keine Züge mehr übrig sind
        if (gameManager == null || gameManager.remainingMoves <= 0 || gameManager.hasWon)
        {
            return;
        }
        
        if (figure != null)
        {
            // Setzt die Figur auf ihre ursprüngliche Position zurück
            figure.transform.position = originalPosition;

            // Starte die Coroutine für den Knopfdrück-Effekt
            if (buttonTransform != null)
            {
                StartCoroutine(PressButton());
            }
            else
            {
                Debug.LogWarning("Kein Button Transform zugewiesen!");
            }
        }
        else
        {
            Debug.LogWarning("Keine Figur zugewiesen!");
        }
    }
    
    // Coroutine, um den Knopf zu drücken und nach einer kurzen Zeit zurückzusetzen
    private System.Collections.IEnumerator PressButton()
    {
        // Speichere die ursprüngliche Position der Platte
        Vector3 originalButtonPosition = buttonTransform.localPosition;

        // Bewege die Platte nach unten
        buttonTransform.localPosition += new Vector3(0f, buttonPressOffset, 0f);
        sound.ArcadeButtonSound();

        // Warte für die angegebene Verzögerung
        yield return new WaitForSeconds(buttonResetDelay);

        // Setze die Platte zurück
        buttonTransform.localPosition = originalButtonPosition;
    }
}
