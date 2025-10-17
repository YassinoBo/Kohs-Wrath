using UnityEngine;
using TMPro; // Benötigt für TextMeshPro

public class InteractableObject : MonoBehaviour
{
    [Header("UI Settings")]
    public GameObject pressEText; // Referenz auf das TextMeshPro-Objekt (der "Press E"-Text)

    [Header("Interaction Settings")]
    public float interactionDistance = 3f; // Maximale Distanz, in der der Text angezeigt wird
    public float fieldOfViewAngle = 60f; // Der Winkel des Sichtfelds (Field of View) des Spielers

    private Transform playerCamera; // Referenz zur Kamera des Spielers

    void Start()
    {
        // Hole die Hauptkamera des Spielers
        playerCamera = Camera.main.transform;
        
        // Stelle sicher, dass der Text zu Beginn deaktiviert ist
        if (pressEText != null)
        {
            pressEText.SetActive(false);
        }
    }

    void Update()
    {
        CheckPlayerProximityAndView();
    }

    void CheckPlayerProximityAndView()
    {
        if (playerCamera == null || pressEText == null) return;

        // Berechne die Distanz zwischen dem Spieler und dem Objekt
        float distanceToPlayer = Vector3.Distance(playerCamera.position, transform.position);

        if (distanceToPlayer <= interactionDistance)
        {
            // Berechne den Richtungsvektor vom Spieler zum Objekt
            Vector3 directionToObject = (transform.position - playerCamera.position).normalized;
            
            // Berechne den Winkel zwischen der Blickrichtung des Spielers und der Richtung zum Objekt
            float angleToObject = Vector3.Angle(playerCamera.forward, directionToObject);

            if (angleToObject <= fieldOfViewAngle / 2f)
            {
                // Spieler ist in der Nähe und schaut auf das Objekt -> Text anzeigen
                pressEText.SetActive(true);
            }
            else
            {
                // Spieler schaut nicht auf das Objekt -> Text ausblenden
                pressEText.SetActive(false);
            }
        }
        else
        {
            // Spieler ist zu weit weg -> Text ausblenden
            pressEText.SetActive(false);
        }
    }

    // Optional: Zeige das Sichtfeld im Editor, um es zu visualisieren
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionDistance);

        if (playerCamera != null)
        {
            Vector3 forward = playerCamera.forward;
            
            // Zeichne die Begrenzungslinien des Sichtfelds
            Vector3 leftBoundary = Quaternion.Euler(0, -fieldOfViewAngle / 2f, 0) * forward;
            Vector3 rightBoundary = Quaternion.Euler(0, fieldOfViewAngle / 2f, 0) * forward;

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.position + leftBoundary * interactionDistance);
            Gizmos.DrawLine(transform.position, transform.position + rightBoundary * interactionDistance);
        }
    }
}
