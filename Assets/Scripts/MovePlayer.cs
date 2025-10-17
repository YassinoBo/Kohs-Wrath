using UnityEngine;
using UnityEngine.Animations;

public class MovePlayer : MonoBehaviour
{
    public CharacterController controller; // Referenz zum CharacterController
    public Transform cameraTransform; // Referenz zur Kamera
    public float moveSpeed = 12f; // Geschwindigkeit

    public bool canMove = true;
    public float gravity = -9.81f;

    private Vector3 velocity;
    private bool isGrounded;
    
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public bool isMovementDisabled = false;


    public static MovePlayer Instance{get; private set;}
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Verhindert doppelte Instanzen
        }
    }


    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isMovementDisabled)
        {
            return;
        }

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        if (canMove)
        {
            // Input erfassen
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            // Bewegung relativ zur Kamera berechnen
            Vector3 forward = cameraTransform.forward;
            Vector3 right = cameraTransform.right;

            // Vertikale Komponente der Kamera entfernen
            forward.y = 0f;
            right.y = 0f;

            // Normalisieren, um konsistente Bewegungen zu gewährleisten
            forward.Normalize();
            right.Normalize();

            // Bewegung berechnen
            Vector3 movement = (forward * z + right * x) * moveSpeed;

            // Bewegung anwenden
            controller.Move(movement * Time.deltaTime);

            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);
        }
    }
    public void DisableMovement()
    {
        isMovementDisabled = true;
    }
}