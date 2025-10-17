using UnityEngine;
using UnityEngine.UI; // Für die Text-Komponente
using TMPro;

public class MinigameInteraction : MonoBehaviour
{
    public Camera minigameCamera; // Kamera für das Minispiel
    public Transform player;      // Referenz zum Spieler
    public float interactionRange = 3f; // Reichweite, in der der Spieler das Minispiel aktivieren kann
    private Camera mainCamera;    // Referenz zur Hauptkamera
    private bool isPlaying = false; // Zustand, ob das Minispiel aktiv ist
    public TextMeshProUGUI  moveCounterText;  // Referenz zum UI-Text für den Zugzähler
    private GhostScript ghost;
    private SoundScript sound;
    private bool attackMob = true;
    public DoorInteraction doorInteraction;
    public SpielerInventar spielerInventar;
    public GameObject buttonExplanation;
    public bool explanationVisible = false;
    public TextMeshProUGUI buttonText;
    
    
    void Start()
    {
        // Finde die Hauptkamera
        mainCamera = Camera.main;
        sound = SoundScript.Instance;

        if (buttonExplanation != null)
        {
            buttonExplanation.SetActive(false);
        }

        // Deaktiviere die Minigame-Kamera standardmäßig
        if (minigameCamera != null)
        {
            minigameCamera.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Minigame-Kamera nicht zugewiesen!");
        }

        // Verstecke den Text zu Beginn des Spiels
        if (buttonText != null)
        {
            buttonText.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("buttonText wurde nicht zugewiesen!");
        }

        if (moveCounterText != null)
        {
            moveCounterText.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("MoveCounterText wurde nicht zugewiesen!");
        }

        ghost = GhostScript.Instance;
    }

    void Update()
    {
        if(player == null) return;
        // Abstand zwischen Spieler und Stromkasten berechnen
        float distance = Vector3.Distance(player.position, transform.position);

        // Wenn der Spieler in der Nähe ist und "E" drückt
        if (distance <= interactionRange && Input.GetKeyDown(KeyCode.E))
        {
            if (spielerInventar != null && !isPlaying) // Null-Prüfung für spielerInventar
            {
                if (spielerInventar.HatObjekt("Springer")) // Überprüft, ob der Spieler das Objekt "Springer" hat
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
            else
            {
                CloseMinigame();
            }
            
        }

        if (Input.GetKeyDown(KeyCode.Tab) && isPlaying)
        {
            ToggleButtonExplanation();
        }
    }

    void ToggleButtonExplanation()
    {
        if (explanationVisible)
        {
            buttonExplanation.SetActive(false);
            explanationVisible = false;
        }
        else
        {
            buttonExplanation.SetActive(true);
            explanationVisible = true;
        }
    }

    void OpenMinigame()
    {
        // Minispiel aktivieren
        doorInteraction.setLocation("Entry");
        isPlaying = true;
        ghost.SetAllowAttack(false);
        setAttack(false);

        // Hauptkamera deaktivieren und Minigame-Kamera aktivieren
        if (mainCamera != null)
        {
            mainCamera.gameObject.SetActive(false);
        }
        if (minigameCamera != null)
        {
            minigameCamera.gameObject.SetActive(true);
        }

        // Text sichtbar machen
        if (moveCounterText != null)
        {
            moveCounterText.gameObject.SetActive(true);
        }

        if (buttonText != null)
        {
            buttonText.gameObject.SetActive(true);
        }

        // Mauszeiger entsperren und sichtbar machen
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Bewegung des Spielers deaktivieren (optional, falls gewünscht)
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }
        
        sound.SecondCamBackgroundSound();

        Debug.Log("Minigame started with Minigame Camera.");
    }

    void CloseMinigame()
    {
        // Minispiel deaktivieren
        doorInteraction.setLocation("MainHall");
        isPlaying = false;
        ghost.SetAllowAttack(true);
        setAttack(true);
        buttonExplanation.SetActive(false);
        explanationVisible = false;
        

        // Hauptkamera aktivieren und Minigame-Kamera deaktivieren
        if (mainCamera != null)
        {
            mainCamera.gameObject.SetActive(true);
        }
        if (minigameCamera != null)
        {
            minigameCamera.gameObject.SetActive(false);
        }

        // Text unsichtbar machen
        if (moveCounterText != null)
        {
            moveCounterText.gameObject.SetActive(false);
        }

        if (buttonText != null)
        {
            buttonText.gameObject.SetActive(false);
        }

        // Mauszeiger sperren und unsichtbar machen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Bewegung des Spielers wieder aktivieren (optional, falls deaktiviert wurde)
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }

        sound.BackgroundSound();

        Debug.Log("Minigame closed and Main Camera reactivated.");
    }

    public void setAttack(bool value)
    {
        attackMob = value;
    }

    public bool getAttack()
    {
        return attackMob;
    }
    
}
