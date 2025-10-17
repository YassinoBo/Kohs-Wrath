using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 movementInput;
    public float speed = 5f;
    public float jumpForce = 5f;
    private bool isGrounded;
    public float cameraSpeed = 5f;
    public float mouseSensitivity = 500f;
    private float yaw = 0f;
    private float pitch = 0f;
    public Transform cameraTransform;      // Referenz zur Kamera
    public TextMeshProUGUI deathMessageText; // Todesmeldung
    public Image bloodOverlay;             // Blut-Overlay*/
    

    public Vector3 startPosition;          // Startposition für Respawn
    public Quaternion startRotation;       // Startrotation für Respawn
    public AudioSource heartbeatSource;    // Herzschlag-Sound
    public AudioSource deathSoundSource;   // Todes-Sound
    public AudioClip deathSoundClip;       // AudioClip für Todes-Sound

    private bool isDead = false;
    
    public bool canMove = true; // Flag, ob der Player sich bewegen darf
    public bool canMoveCam = true;

    private bool moveForward = false;
    private bool moveBackward = false;
    private bool moveRight = false;
    private bool moveLeft = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        Cursor.lockState = CursorLockMode.Locked;

        /*startPosition = transform.position;
        startRotation = transform.rotation;*/

        /*if (bloodOverlay != null)
        {
            bloodOverlay.gameObject.SetActive(false); // Versteckt das Blut-Overlay
        }
        if (deathMessageText != null)
        {
            deathMessageText.gameObject.SetActive(false); // Versteckt den Todes-Text
        }*/
    }

    void Update()
    {
        if (canMoveCam)
        {
        // Mausbewegung für die Kameradrehung
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -90f, 90f);

        transform.localRotation = Quaternion.Euler(pitch, yaw, 0f);

        }

        if (canMove)
        {
            // Bewegungstasten prüfen und Booleans setzen
            moveForward = Input.GetKey(KeyCode.W);
            moveBackward = Input.GetKey(KeyCode.S);
            moveRight = Input.GetKey(KeyCode.D);
            moveLeft = Input.GetKey(KeyCode.A);
        }
        else
        {
            // Bewegung stoppen
            moveForward = moveBackward = moveRight = moveLeft = false;
        }
    }


    public bool getMove()
    {
        return canMove;
    }

    public void setMove(bool newMove)
    {
        canMove = newMove;
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            Vector3 moveDirection = Vector3.zero;

            if (moveForward) moveDirection += transform.forward;
            if (moveBackward) moveDirection -= transform.forward;
            if (moveRight) moveDirection += transform.right;
            if (moveLeft) moveDirection -= transform.right;

            moveDirection.Normalize(); // Sicherstellen, dass die Bewegung gleichmäßig bleibt
            rb.MovePosition(rb.position + moveDirection * speed * Time.fixedDeltaTime);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Enemy") && !isDead)
        {
            isDead = true;
            StartCoroutine(DeathSequence());
        }
        
    }

    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.CompareTag("WinArea") && c.gameObject.activeSelf) // Nur wenn WinArea aktiv ist
        {
            canMove = false; // Bewegung stoppen
            canMoveCam = false;
            Debug.Log("Du hast gewonnen!"); // Nachricht in der Konsole ausgeben
        }
    }

    private IEnumerator DeathSequence()
    {
        // Herzschlag-Sound stoppen
        if (heartbeatSource != null && heartbeatSource.isPlaying)
        {
            heartbeatSource.Stop();
        }

        // Todes-Sound abspielen
        if (deathSoundSource != null && deathSoundClip != null)
        {
            deathSoundSource.clip = deathSoundClip;
            deathSoundSource.Play();
        }

        // Blut-Overlay und Text anzeigen
        if (bloodOverlay != null)
        {
            bloodOverlay.gameObject.SetActive(true);
            Color bloodColor = bloodOverlay.color;
            bloodColor.a = 1f;
            bloodOverlay.color = bloodColor;
        }
        if (deathMessageText != null)
        {
            deathMessageText.gameObject.SetActive(true);
            deathMessageText.text = "Du wurdest getötet!";
        }

        // Kamera nach hinten kippen
        if (cameraTransform != null)
        {
            StartCoroutine(CameraFallEffect());
        }

        // Warte auf das Ende des Todes-Sounds oder mindestens 3 Sekunden
        float waitTime = deathSoundClip != null ? deathSoundClip.length : 3f;
        yield return new WaitForSeconds(waitTime);

        // Spieler zurücksetzen
        transform.position = startPosition;
        transform.rotation = startRotation;

        // Herzschlag wieder starten
        if (heartbeatSource != null)
        {
            heartbeatSource.loop = true;
            heartbeatSource.Play();
        }

        // Text und Blut-Overlay ausblenden
        if (bloodOverlay != null)
        {
            bloodOverlay.gameObject.SetActive(false);
        }
        if (deathMessageText != null)
        {
            deathMessageText.gameObject.SetActive(false);
        }

        isDead = false; // Spieler ist wieder am Leben
    }

    private IEnumerator CameraFallEffect()
    {
        // Ausgangsrotation und Zielrotation der Kamera
        Quaternion startRotation = cameraTransform.rotation;
        Quaternion endRotation = Quaternion.Euler(cameraTransform.eulerAngles.x - 45f, cameraTransform.eulerAngles.y, cameraTransform.eulerAngles.z);

        float duration = 1f; // Dauer des Effekts
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            // Interpoliere zwischen der Ausgangs- und Zielrotation
            cameraTransform.rotation = Quaternion.Lerp(startRotation, endRotation, elapsedTime / duration);

            yield return null; // Warte auf den nächsten Frame
        }
    }
}
