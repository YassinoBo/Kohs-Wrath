using UnityEngine;

public class PlayerWinOnCollision : MonoBehaviour
{
    public float moveSpeed = 5f; // Bewegungsgeschwindigkeit des Players
    private bool canMove = true; // Flag, ob der Player sich bewegen darf

    void Update()
    {
        if (canMove)
        {
            // Bewegung des Players mit den Pfeiltasten oder WASD
            float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
            float moveZ = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

            transform.Translate(moveX, 0, moveZ);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Überprüfen, ob die Kollision mit dem Tag "WinArea" ist
        if (collision.gameObject.CompareTag("WinArea"))
        {
            canMove = false; // Bewegung stoppen
            Debug.Log("Du hast gewonnen!"); // Nachricht in der Konsole ausgeben
        }
    }
}