using UnityEngine;

public class StromkastenInteraktion : MonoBehaviour
{
    public Transform player; // Referenz zum Spieler
    public float interactionRange = 3f; // Reichweite f�r die Interaktion
    private Camera stromkastenCamera; // Kamera f�r das Minigame (Stromkasten-Perspektive)
    private Camera mainCamera; // Hauptkamera
    private bool isPlaying = false; // Zustand, ob das Minigame aktiv ist

    private Schalter[] switches; // Alle Schalter im Stromkasten

    void Start()
    {

      // schachMinispiel.SetActive(false); // Deaktiviert Schachspiel-Objekte

        // Finde alle Schalter, die Kinder des Stromkastens sind
        switches = GetComponentsInChildren<Schalter>();

        // Speichere die Hauptkamera
        mainCamera = Camera.main;

        // Finde die Stromkasten-Kamera basierend auf dem Tag "StromkastenCamera"
        GameObject cameraObject = GameObject.FindGameObjectWithTag("StromkastenCamera");
        if (cameraObject != null)
        {
            stromkastenCamera = cameraObject.GetComponent<Camera>();
            stromkastenCamera.gameObject.SetActive(false); // Deaktiviere die Stromkasten-Kamera standardm��ig
        }
        else
        {
            Debug.Log("Keine Kamera mit dem Tag 'StromkastenCamera' gefunden. Bitte stelle sicher, dass sie korrekt gesetzt ist.");
        }
    }

    void Update()
    {
        // Berechne die Distanz zwischen Spieler und Stromkasten
        float distance = Vector3.Distance(player.position, transform.position);

        // Interaktion pr�fen
        if (distance <= interactionRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!isPlaying)
            {
                OpenMinigame();
            }
            else
            {
                CloseMinigame();
            }
        }
    }

    void OpenMinigame()
    {
        // Minigame aktivieren
        isPlaying = true;

        // Deaktiviere Hauptkamera und aktiviere Stromkasten-Kamera
        if (mainCamera != null)
            mainCamera.gameObject.SetActive(false);

        if (stromkastenCamera != null)
        {
            stromkastenCamera.gameObject.SetActive(true);

            // Sicherstellen, dass die Culling Mask die Schalter-Layer rendert
            LayerMask schalterLayer = LayerMask.NameToLayer("Default"); // �ndere "Default" auf den Layer deines Schalters
            if ((stromkastenCamera.cullingMask & (1 << schalterLayer)) == 0)
            {
                stromkastenCamera.cullingMask |= (1 << schalterLayer); // Schalter-Layer zur Culling Mask hinzuf�gen
                Debug.Log("Schalter-Layer wurde zur Culling Mask der Kamera hinzugef�gt.");
            }
        }

        // Deaktiviere die Bewegung des Spielers
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        if (playerMovement != null)
            playerMovement.enabled = false;

        // Maus freigeben
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Debug.Log("Minigame gestartet: Stromkasten.");
    }

    void CloseMinigame()
    {
        // Minigame deaktivieren
        isPlaying = false;

        // Reaktiviere Hauptkamera und deaktiviere Stromkasten-Kamera
        if (mainCamera != null)
            mainCamera.gameObject.SetActive(true);

        if (stromkastenCamera != null)
            stromkastenCamera.gameObject.SetActive(false);

        // Spielerbewegung wieder aktivieren
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        if (playerMovement != null)
            playerMovement.enabled = true;

        // Maus sperren
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Debug.Log("Minigame beendet.");
    }

    public void CompleteMinigame()
    {
        // Wird aufgerufen, wenn das Minigame erfolgreich abgeschlossen wurde
        Debug.Log("Minigame erfolgreich abgeschlossen!");
        CloseMinigame();
    }
}
