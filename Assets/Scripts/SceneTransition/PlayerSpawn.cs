using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawn : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Überprüfe, ob die aktuelle Szene einen Spieler benötigt
        if (PlayerPrefs.HasKey("SpawnPosX"))
        {
            // Überspringe das Setzen der Position, wenn ein Spieler in der neuen Szene existiert
            GameObject existingPlayer = GameObject.FindGameObjectWithTag("Player");
            if (existingPlayer != null && existingPlayer != gameObject)
            {
                Debug.Log("Ein Spieler existiert bereits in der neuen Szene. Position wird nicht geändert.");
                return;
            }

            // Lade gespeicherte Positionen
            float spawnPosX = PlayerPrefs.GetFloat("SpawnPosX", 0f);
            float spawnPosY = PlayerPrefs.GetFloat("SpawnPosY", 1f);
            float spawnPosZ = PlayerPrefs.GetFloat("SpawnPosZ", 0f);

            transform.position = new Vector3(spawnPosX, spawnPosY, spawnPosZ);

            PlayerPrefs.DeleteKey("SpawnPosX");
            PlayerPrefs.DeleteKey("SpawnPosY");
            PlayerPrefs.DeleteKey("SpawnPosZ");
        }
    }
}