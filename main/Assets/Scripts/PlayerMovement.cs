using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;


public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 movementInput;
    public float speed = 5f;                // Bewegungsgeschwindigkeit des Spielers
    public float jumpForce = 5f;            // Sprungkraft des Spielers
    private bool isGrounded;                // Prüft, ob der Spieler am Boden ist
    public float cameraSpeed = 5f;          // Geschwindigkeit der Kamera für das Drehen
    public float mouseSensitivity = 500f;   // Empfindlichkeit der Maus für die Drehung
    private float yaw = 0f;                 // Drehung um die Y-Achse für horizontale Kamerabewegung
    private float pitch = 0f;               // Neigung für vertikale Kamerabewegung





    public int respawnSceneIndex;  // Index der Szene, zu der umgeschaltet werden soll, wenn der Spieler getötet wird.

    private Vector3 startPosition; // Startposition für das Respawnen des Spielers
    private Quaternion startRotation; // Quaternion für die Rotation
    public TextMeshProUGUI deathMessageText;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;                   // Verhindert automatisches Rotieren durch Physik
        Cursor.lockState = CursorLockMode.Locked;   // Sperrt den Mauszeiger im Fenster



        deathMessageText.gameObject.SetActive(false); // Versteckt den Text beim Start
        startPosition = transform.position; // Speichert die Startposition
        startRotation = transform.rotation; // Speichert die Startrotation

    }

    void Update()
    {
        // Mausbewegung für die Kameradrehung
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -90f, 90f);

        transform.localRotation = Quaternion.Euler(pitch, yaw, 0f);

        // Erfasst Tastatureingaben für die Bewegung
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Berechnet die Bewegungsrichtung basierend auf Spielerrotation
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        movementInput = new Vector3(move.x, 0, move.z).normalized; // Nur X und Z beeinflussen die Bewegung
    }

    void OnMove(InputValue movementValue)
    {
        // Liest den Bewegungseingang des Spielers (aus dem InputSystem)
        Vector2 inputVector = movementValue.Get<Vector2>();
        movementInput = new Vector3(inputVector.x, 0, inputVector.y).normalized; // Nur X und Z beeinflussen die Bewegung
    }

    void OnJump()
    {
        if (isGrounded)  // Nur springen, wenn der Spieler am Boden ist
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;  // Spieler ist nun in der Luft
        }
    }

    private void FixedUpdate()
    {
        Vector3 movement = movementInput * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement); // Bewegt den Würfel basierend auf den Eingaben
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; // Der Spieler ist am Boden
        }

        // Überprüfen, ob der Kollisionspartner der Gegner ist
        if (collision.gameObject.tag == "Enemy")
        {
            transform.position = startPosition;
            transform.rotation = startRotation;
            deathMessageText.text = "Du wurdest getötet und zurückgesetzt!";
            deathMessageText.gameObject.SetActive(true);
            Invoke("HideText", 3); // Versteckt den Text nach 3 Sekunden
        }
    }


    void HideText()
    {
        deathMessageText.gameObject.SetActive(false);
    }








    private void OnCollisionStay(Collision collision)
    {
        // Verhindert, dass der Spieler in der Luft bleibt, wenn er auf den Boden trifft
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Wenn der Spieler den Boden verlässt, wird isGrounded auf false gesetzt
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
