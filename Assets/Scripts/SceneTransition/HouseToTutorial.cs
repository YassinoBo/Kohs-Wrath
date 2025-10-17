using UnityEngine;
using UnityEngine.SceneManagement;

public class HouseToTutorial : MonoBehaviour
{
    public bool canTriggerKoh = false; // Boolean-Schalter für Rückkehr
    public KohScript kohScript;
    public bool doorOpen = false;
    private bool schalter = true;

    public Transform player; // Referenz zum Spieler-Transform
    public MovePlayer playerMovement; // Referenz zum PlayerMovement-Skript
    public MoveCam cameraController;

    void Update()
    {
        if (Input.GetMouseButton(0) && canTriggerKoh && doorOpen)
        {
            if (schalter)
            {
              // Speichere keine Position, sondern wechsle einfach zur neuen Szene
              Debug.Log("Update Triggered Koh");
              DisablePlayerControls();
              RotatePlayerToEnemy();
              kohScript.StartChasing();
              schalter = false;
            }
            
        }
    }
    private void DisablePlayerControls()
    {
        if (playerMovement != null)
        {
            Debug.Log("Disable Movement");
            playerMovement.DisableMovement(); // Spielerbewegung deaktivieren
        }

        if (cameraController != null)
        {
            Debug.Log("Lock Camera");
            cameraController.LockCamera(); // Kamera-Steuerung deaktivieren
        }
    }

    private void RotatePlayerToEnemy()
    {
        if (player != null && kohScript != null)
        {
            Debug.Log("Rotating Player");

            Transform enemy = kohScript.transform; // Gegner-Position
            Vector3 directionToEnemy = (enemy.position - player.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToEnemy.x, 0, directionToEnemy.z));

            // Weiche Drehung des Spielers
            StartCoroutine(RotateOverTime(player, lookRotation, 1.0f)); // 1 Sekunde Drehzeit
        }
    }

    private System.Collections.IEnumerator RotateOverTime(Transform target, Quaternion targetRotation, float duration)
    {
        Quaternion startRotation = target.rotation;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            target.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        target.rotation = targetRotation; // Am Ende exakt ausrichten
    }

}