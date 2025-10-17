using System;
using UnityEngine;

public class CustomButtonHandler : MonoBehaviour
{
    // Die Figur, die bewegt werden soll
    public GameObject figure;

    // Bewegungsoffset-Werte
    public float xWert = 0f;
    public float yWert = 0f;

    // Collider der Begrenzungsbox
    public BoxCollider boundaryBox;

    // Referenz für die "Platte" (Knopf-Oberfläche)
    public Transform buttonTransform;

    // Bewegung des Knopfes beim Drücken
    public float buttonPressOffset = -0.0078f; // Bewegung nach unten
    public float buttonResetDelay = 0.5f;       // Zeit bis der Knopf zurückspringt (in Sekunden)

    private SoundScript soundScript;

    // Referenz auf den GameManager
    public GameManager gameManager;

    private void Start()
    {
        soundScript = SoundScript.Instance;
    }

    // Wird aufgerufen, wenn der Button angeklickt wird
    private void OnMouseDown()
    {
        // Überprüft, ob der Startknopf gedrückt wurde
        if (!SpringerButtonHandler.startButtonPressed)
        {
            Debug.Log("Bitte drücke zuerst den Startknopf.");
            return;  // Verhindert die Interaktion, wenn der Startknopf noch nicht gedrückt wurde
        }
        
        // Verhindern, dass sich die Figur bewegt, wenn das Spiel gewonnen ist oder keine Züge mehr übrig sind
        if (gameManager == null || gameManager.remainingMoves <= 0 || gameManager.hasWon)
        {
            return;
        }

        // Informiere den GameManager, dass ein Button gedrückt wurde
        gameManager.OnButtonPressed();

        if (figure != null)
        {
            // Bewegungsoffset basierend auf aktuellen Werten berechnen
            Vector3 moveOffset = new Vector3(xWert, 0f, yWert);
            Vector3 newPosition = figure.transform.position + moveOffset;

            // Prüfen, ob die neue Position innerhalb der Grenze ist
            if (boundaryBox.bounds.Contains(newPosition))
            {
                figure.transform.position = newPosition;
                // Zugzähler verringern
                gameManager.OnMoveMade();
            }
            else
            {
                Debug.LogWarning("Bewegung ungültig! Zielposition außerhalb der Spielfeldgrenzen.");
            }
        }
        else
        {
            Debug.LogWarning("Keine Figur zugewiesen!");
        }

        // Simuliere den Knopfdruck
        if (buttonTransform != null)
        {
            StartCoroutine(PressButton());
        }
    }


    // Coroutine, um den Knopf zu drücken und nach einer kurzen Zeit zurückzusetzen
    private System.Collections.IEnumerator PressButton()
    {
        // Speichere die ursprüngliche Position der Platte
        Vector3 originalPosition = buttonTransform.localPosition;

        // Bewege die Platte nach unten
        buttonTransform.localPosition += new Vector3(0f, buttonPressOffset, 0f);
        
        soundScript.ArcadeButtonSound();

        // Warte für die angegebene Verzögerung
        yield return new WaitForSeconds(buttonResetDelay);

        // Setze die Platte zurück
        buttonTransform.localPosition = originalPosition;
    }
}
