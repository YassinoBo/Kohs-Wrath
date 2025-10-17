using UnityEngine;

public class LampInteraction : MonoBehaviour
{
    public GameObject sceneLamp; // Referenz zur Lampe in der Szene
    public GameObject playerLamp; // Referenz zur Lampe des Spielers
    public GameObject playerArm; // Referenz zum Arm des Spielers
    public GameObject qte; // Referenz zum Arm des Spielers
    public float interactionRange = 2f; // Reichweite für die Interaktion
    private bool isInRange = false; // Ob der Spieler in Reichweite ist+
    public GameObject blockade;

    void Update()
    {
        // Prüfen, ob der Spieler in Reichweite ist
        if (isInRange && Input.GetMouseButton(0))
        {
            // Szene-Lampe deaktivieren
            if (sceneLamp != null)
            {
                sceneLamp.SetActive(false);
                blockade.SetActive(false);
            }

            // Spieler-Lampe aktivieren
            if (playerLamp != null && playerArm != null)
            {
                playerLamp.SetActive(true);
                playerArm.SetActive(true);
                qte.SetActive(true);
            }

            // Einmalige Aktion, falls erforderlich
            isInRange = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Prüfen, ob das Objekt der Spieler ist
        if (other.CompareTag("Player"))
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Reichweite verlassen
        if (other.CompareTag("Player"))
        {
            isInRange = false;
        }
    }
    void OnDrawGizmos()
    {
        // Optional: Zeige die maximale Sichtentfernung im Editor als Gizmo
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}