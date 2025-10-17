using UnityEngine;
using UnityEngine.SceneManagement;

public class WinningFieldManager : MonoBehaviour
{
    // Referenz auf den Collider der WinArea (der zu Beginn deaktiviert wird)
    private Collider winAreaCollider;

    // Zielkoordinaten, zu denen das Objekt verschoben wird
    private Vector3 targetPosition = new Vector3(0.841000021f,0.465000004f,0.0410000011f);

    void Start()
    {
        // Hole den Collider des aktuellen GameObjects (WinArea)
        winAreaCollider = GetComponent<Collider>();
        
        // Deaktiviere den Collider zu Beginn, damit das Betreten der WinArea nicht möglich ist
        winAreaCollider.enabled = false;
    }

    void Update()
    {
        // Wenn der Hebel umgelegt wurde, aktiviere den Collider der WinArea
        if (!winAreaCollider.enabled)
        {
            // Aktiviere den Collider der WinArea
            winAreaCollider.enabled = true;
            Debug.Log("WinArea aktiviert, du kannst nun gewinnen!");

            // Verschiebe das GameObject (WinArea) zu den angegebenen Koordinaten
            winAreaCollider.transform.position = targetPosition;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Überprüft, ob der Spieler die WinArea betritt
        if (other.CompareTag("Player"))
        {
            // Lade die Credits-Szene
            SceneManager.LoadScene("Credits");  // Hier den Namen der Credits-Szene eintragen
        }
    }
}
