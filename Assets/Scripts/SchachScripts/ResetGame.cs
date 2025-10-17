using UnityEngine;

public class ResetGame : MonoBehaviour
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

    public SanityLossScript sanityLossScript;

    // Die Spray-Objekte oben am Automaten
    public GameObject[] sprays; // Array für mehrere Sprays

    // Dauer der Effekte
    public float sprayEffectDuration = 1.0f;
    private SoundScript soundScript;
    
    public GameManager gameManager;

    // Wird aufgerufen, bevor das Spiel startet
    private void Start()
    {
        soundScript = SoundScript.Instance;
        if (figure != null)
        {
            // Speichert die ursprüngliche Position der Figur
            originalPosition = figure.transform.position;
        }
        else
        {
            Debug.LogWarning("Keine Figur zugewiesen!");
        }

        if (sprays == null || sprays.Length == 0)
        {
            Debug.LogWarning("Keine Sprays zugewiesen!");
        }
        else
        {
            // Stelle sicher, dass die Sprays initial deaktiviert sind
            foreach (GameObject spray in sprays)
            {
                if (spray != null)
                {
                    spray.SetActive(false);
                }
            }
        }
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

        if (gameManager == null || gameManager.hasWon)
        {
            return;
        }

       
          if (figure != null)
                  {
                      // Setzt die Figur auf ihre ursprüngliche Position zurück
                      figure.transform.position = originalPosition;
                      
                      // Setzt die Züge zurück
                      if (gameManager != null)
                      {
                          gameManager.ResetMoves();
                      }
                      else
                      {
                          Debug.LogWarning("GameManager ist nicht gesetzt!");
                      }
          
                      // Sanity-Verlust bei falscher Kombination auslösen
                      if (sanityLossScript != null)
                      {
                          sanityLossScript.LoseSanity();
                      }
                      else
                      {
                          Debug.LogError("SanityLossScript ist nicht gesetzt!");
                      }
          
                      // Starte die Coroutine für den Knopfdrück-Effekt
                      if (buttonTransform != null)
                      {
                          StartCoroutine(PressButton());
                      }
                      else
                      {
                          Debug.LogWarning("Kein Button Transform zugewiesen!");
                      }
          
                      // Starte den Spray-Effekt
                      if (sprays != null && sprays.Length > 0)
                      {
                          StartCoroutine(TriggerSprayEffects());
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
        soundScript.ArcadeButtonSound();

        // Warte für die angegebene Verzögerung
        yield return new WaitForSeconds(buttonResetDelay);

        // Setze die Platte zurück
        buttonTransform.localPosition = originalButtonPosition;
    }

    // Coroutine, um die Sprays für eine kurze Zeit zu aktivieren
    private System.Collections.IEnumerator TriggerSprayEffects()
    {
        // Aktiviere alle Sprays
        foreach (GameObject spray in sprays)
        {
            if (spray != null)
            {
                spray.SetActive(true);
            }
        }

        // Warte für die angegebene Dauer
        yield return new WaitForSeconds(sprayEffectDuration);

        // Deaktiviere alle Sprays wieder
        foreach (GameObject spray in sprays)
        {
            if (spray != null)
            {
                spray.SetActive(false);
            }
        }
    }
}
