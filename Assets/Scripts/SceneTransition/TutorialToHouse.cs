using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialToHouse : MonoBehaviour
{
    public string sceneToLoad; // Die Szene, die geladen werden soll
    public Vector3 spawnPosition; // Die Position, an der der Spieler in der neuen Szene erscheinen soll

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            // Speichert die Zielposition in PlayerPrefs
            PlayerPrefs.SetFloat("SpawnPosX", spawnPosition.x);
            PlayerPrefs.SetFloat("SpawnPosY", spawnPosition.y);
            PlayerPrefs.SetFloat("SpawnPosZ", spawnPosition.z);

            // Lade die neue Szene
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}