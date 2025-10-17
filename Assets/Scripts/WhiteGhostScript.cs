using System.Collections;
using UnityEngine;

public class WhiteGhostScript : MonoBehaviour
{
    public GameObject player;
    public Transform playerTransform;
    public float scareDistance = 2f;
    public float scareDuration = 10f;
    public float ghostSpeed = 5f;
    public float heightAdjustmentSpeed = 2f;
    public Camera mainCamera;
    public DoorInteraction doorInteraction;

    private GhostTrigger ghostTrigger;
    private BottomStairsTrigger bottomTrigger;
    private bool isScareActive = false;
    private bool isPlayerAttached = false;
    private float scareTimer = 0f;
    private LampScript lampScript;
    private Rigidbody playerRigidbody;
    private float targetY;
    private MovePlayer movePlayer;
    private MoveCam cam;
    private QTE _qte;
    private SoundScript soundScript;
    private GhostScript ghostScript;

    private Vector3 cameraShakeOffset = Vector3.zero;
    private float shakeMagnitude = 0.1f;
    private float shakeDuration = 2.6f;
    private float shakeTimer = 0f;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        if (player != null)
        {
            playerTransform = player.transform;
            playerRigidbody = player.GetComponent<Rigidbody>();
        }

        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        if (doorInteraction == null)
        {
            doorInteraction = FindObjectOfType<DoorInteraction>();
        }

        ghostTrigger = GhostTrigger.Instance;
        bottomTrigger = BottomStairsTrigger.Instance;
        lampScript = LampScript.Instance;
        movePlayer = MovePlayer.Instance;
        cam = MoveCam.Instance;
        _qte = QTE.Instance;
        soundScript = SoundScript.Instance;
        ghostScript = GhostScript.Instance;
    }

    void Update()
    {
        if (ghostTrigger != null && bottomTrigger != null && ghostTrigger.IsInZone && bottomTrigger.WasBottom && !isScareActive)
        {
            ActivateGhost();
        }

        if (isScareActive)
        {
            UpdateScare();
        }

        // Kamera Zittern, wenn der Geist den Spieler "packt"
        if (shakeTimer > 0f)
        {
            shakeTimer -= Time.deltaTime;

            // Berechne zufällige x- und y-Positionen innerhalb des Shake-Bereichs
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            // Setze neue Position der Kamera mit Zittern
            mainCamera.transform.localPosition = new Vector3(x, y, mainCamera.transform.localPosition.z);
        }
        else
        {
            // Wenn der Timer abgelaufen ist, setze die Kamera auf die ursprüngliche Position zurück
            mainCamera.transform.localPosition = Vector3.zero;
        }
    }

    void ActivateGhost()
    {
        targetY = player.transform.position.y;
        transform.position = new Vector3(player.transform.position.x, targetY, player.transform.position.z + 7f);
        isScareActive = true;
    }

    void UpdateScare()
    {
        Vector3 newPosition = transform.position;
        newPosition.z -= ghostSpeed * Time.deltaTime;

        if (!isPlayerAttached)
        {
            targetY = player.transform.position.y;
            newPosition.y = Mathf.Lerp(transform.position.y, targetY, Time.deltaTime * heightAdjustmentSpeed);
        }

        transform.position = newPosition;

        float distance = Mathf.Abs(transform.position.z - player.transform.position.z);
        if (distance <= scareDistance && !isPlayerAttached)
        {
            AttachPlayer();
        }

        if (isPlayerAttached)
        {
            scareTimer += Time.deltaTime;
            if (scareTimer >= scareDuration)
            {
                EndScare();
            }
        }
    }

    void AttachPlayer()
    {
        isPlayerAttached = true;
        lampScript.setStatus(false);
        _qte.blockEvent = true;
        soundScript.WhiteGhostScareSound();
        soundScript.HeartbeatSound();

        EnableMovement(false);

        // Start camera shake when player is attached
        shakeTimer = shakeDuration;

        if (playerRigidbody != null)
        {
            playerRigidbody.useGravity = false;
            playerRigidbody.isKinematic = true;
        }
    }

    void EndScare()
    {
        isPlayerAttached = false;

        if (playerRigidbody != null)
        {
            playerRigidbody.useGravity = true;
            playerRigidbody.isKinematic = false;
        }

        playerTransform.transform.position = new Vector3(-14.0955887f,-2.72099996f,-13.9060802f);
        doorInteraction.setLocation("Basement");
        ghostScript.SetAllowAttack(true);
        StartCoroutine(EnableMovementAfterTeleport());

        isScareActive = false;
        _qte.blockEvent = false;
    }

    void FixedUpdate()
    {
        if (isPlayerAttached)
        {
            player.transform.position = transform.position - new Vector3(0, -1f, 1f);
            mainCamera.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    void EnableMovement(bool state)
    {
        cam.canMoveCam = state;
        movePlayer.canMove = state;
    }

    IEnumerator EnableMovementAfterTeleport()
    {
        yield return null;
        EnableMovement(true);
    }
}
