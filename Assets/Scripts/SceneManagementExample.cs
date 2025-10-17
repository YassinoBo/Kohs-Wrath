using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagementExample : MonoBehaviour
{
    private void Start()
    {
        // Beispiel: Zerstöre das GameObject, bevor die Szene wechselt
        if (gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}