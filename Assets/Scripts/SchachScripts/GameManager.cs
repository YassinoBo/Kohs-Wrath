using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int maxMoves = 20;
    public int remainingMoves;

    public GameObject figure;
    public BoxCollider winningBox;
    public bool hasWon = false;

    public TextMeshProUGUI moveCounterText;
    public SanityLossScript sanityLossScript;

    private BlackHuman human;

    // Referenz für den Arm
    public GameObject fallingArm;

    // Referenz für den Hammer
    public GameObject fallingHammer;

    // Flag für einmalige Aktivierung des Arms
    private bool isArmActivated = false;

    // Flag für einmalige Aktivierung des Hammers
    private bool isHammerActivated = false;
    
    void Start()
    {
        remainingMoves = maxMoves;
        
        human = BlackHuman.Instance;

        if (figure == null)
        {
            Debug.LogWarning("Die Spielfigur wurde nicht zugewiesen!");
        }

        if (moveCounterText == null)
        {
            Debug.LogWarning("Das Text-UI-Element für den Zugzähler wurde nicht zugewiesen!");
        }
        else
        {
            moveCounterText.gameObject.SetActive(false);
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
        
        UpdateMoveCounterText();
    }

    void Update()
    {
        if (remainingMoves <= 0)
        {
            StopGame();
        }

        if (winningBox.bounds.Contains(figure.transform.position) && !hasWon)
        {
            hasWon = true;
            human.activate();
            ActivateFallingArm(); // Arm aktivieren
            ActivateFallingHammer(); // Hammer aktivieren
            StopGame();
            Debug.Log("Gewonnen!");
        }
    }

    public void OnButtonPressed()
    {
        if (!moveCounterText.gameObject.activeSelf)
        {
            moveCounterText.gameObject.SetActive(true);
        }
    }

    public void OnMoveMade()
    {
        if (remainingMoves > 0 && !hasWon)
        {
            remainingMoves--;
            UpdateMoveCounterText();
        }
    }

    public void ResetMoves()
    {
        remainingMoves = maxMoves;
        UpdateMoveCounterText();
        Debug.Log("Züge wurden zurückgesetzt auf: " + maxMoves);
    }

    private void StopGame()
    {
        if (moveCounterText != null)
        {
            moveCounterText.gameObject.SetActive(false);
        }

        if (figure != null)
        {
            CustomButtonHandler buttonHandler = figure.GetComponent<CustomButtonHandler>();
            if (buttonHandler != null)
            {
                buttonHandler.enabled = false;
            }
            else
            {
                Debug.LogWarning("CustomButtonHandler nicht an der Spielfigur gefunden!");
            }
        }
        else
        {
            Debug.LogWarning("Die Spielfigur ist null!");
        }
    }

    private void UpdateMoveCounterText()
    {
        if (moveCounterText != null)
        {
            if (remainingMoves > 10)
            {
                moveCounterText.color = Color.white;
            }
            else if (remainingMoves <= 10 && remainingMoves > 5)
            {
                moveCounterText.color = Color.yellow;
            }
            else
            {
                moveCounterText.color = Color.red;
            }

            moveCounterText.text = "Züge: " + remainingMoves;
        }
        else
        {
            Debug.LogWarning("MoveCounterText ist nicht zugewiesen!");
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