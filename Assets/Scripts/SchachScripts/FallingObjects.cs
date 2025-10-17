using UnityEngine;

public class FallingObjects : MonoBehaviour
{
    // Referenzen für den Arm und den Hammer
    public GameObject fallingArm;
    public GameObject fallingHammer;

    // Flag für den Gewinnstatus (wird von GameManager gesetzt)
    private bool hasWon = false;

    // Flags für einmalige Aktivierung
    private bool isArmActivated = false;
    private bool isHammerActivated = false;

    public GameManager gameManager; // Referenz auf GameManager (wird über Inspektor zugewiesen)

    private void Start()
    {
        if (gameManager != null)
        {
            hasWon = gameManager.hasWon;  // Zugriff auf die hasWon-Variable von GameManager
        }

        // Sicherstellen, dass der Arm deaktiviert ist
        if (fallingArm != null)
        {
            fallingArm.SetActive(false);
        }

        // Sicherstellen, dass der Hammer deaktiviert ist
        if (fallingHammer != null)
        {
            fallingHammer.SetActive(false);
        }
    }

    void Update()
    {
        if (gameManager != null)
        {
            hasWon = gameManager.hasWon;  // Zugriff auf die hasWon-Variable von GameManager
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("22ff2fefe");
        Debug.Log(other.tag);
        Debug.Log(hasWon);
        // Überprüfen, ob der Spieler durch den Trigger läuft
        if (other.CompareTag("Player") && hasWon)
        {
            Debug.Log("Arm und Hammer fallen runter!");
            ActivateFallingArm(); // Arm aktivieren
            ActivateFallingHammer(); // Hammer aktivieren
        }
    }

    private void ActivateFallingArm()
    {
        if (fallingArm != null && !isArmActivated)
        {
            fallingArm.SetActive(true); // Arm aktivieren
            isArmActivated = true; // Markiere den Arm als aktiviert
            Rigidbody armRigidbody = fallingArm.GetComponent<Rigidbody>();
            if (armRigidbody == null)
            {
                Debug.LogWarning("Der Arm hat keinen Rigidbody! Füge einen Rigidbody hinzu, damit er fallen kann.");
            }
        }
        else if (isArmActivated)
        {
            Debug.Log("Der Arm wurde bereits aktiviert.");
        }
        else
        {
            Debug.LogWarning("Falling Arm ist nicht zugewiesen!");
        }
    }

    private void ActivateFallingHammer()
    {
        if (fallingHammer != null && !isHammerActivated)
        {
            fallingHammer.SetActive(true); // Hammer aktivieren
            isHammerActivated = true; // Markiere den Hammer als aktiviert
            Rigidbody hammerRigidbody = fallingHammer.GetComponent<Rigidbody>();
            if (hammerRigidbody == null)
            {
                Debug.LogWarning("Der Hammer hat keinen Rigidbody! Füge einen Rigidbody hinzu, damit er fallen kann.");
            }
        }
        else if (isHammerActivated)
        {
            Debug.Log("Der Hammer wurde bereits aktiviert.");
        }
        else
        {
            Debug.LogWarning("Falling Hammer ist nicht zugewiesen!");
        }
    }
}