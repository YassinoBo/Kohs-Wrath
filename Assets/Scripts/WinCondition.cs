using UnityEngine;

public class WinCondition : MonoBehaviour
{
    private bool hasWon = false; // Flag, um Mehrfachausgabe zu verhindern

    private void Start()
    {
        // Sicherstellen, dass der BoxCollider als Trigger eingestellt ist
        BoxCollider collider = GetComponent<BoxCollider>();
        if (collider == null)
        {
            Debug.LogError("Kein BoxCollider an diesem Objekt gefunden!");
        }
        else if (!collider.isTrigger)
        {
            Debug.LogWarning("BoxCollider ist kein Trigger. Setze 'Is Trigger' auf true.");
            collider.isTrigger = true;
        }
    }

    // Wird aufgerufen, wenn ein anderer Collider in das Triggerfeld eintritt
    private void OnTriggerEnter(Collider other)
    {
        // Prüfen, ob das kollidierende Objekt die Spielfigur ist (per Tag identifiziert)
        if (other.CompareTag("Player") && !hasWon)
        {
            hasWon = true;
            Debug.Log("Gewonnen! Die Spielfigur hat das Gewinnfeld betreten.");
        }
    }
}