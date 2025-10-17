using UnityEngine;

public class MonsterSoundController : MonoBehaviour
{
    public Transform player;               // Referenz auf den Spieler
    public AudioSource monsterSound;       // Die AudioSource mit dem Monster-Sound
    public float maxDistance = 20f;        // Maximale Entfernung, bei der der Sound noch hörbar ist
    public float minVolume = 0.1f;         // Minimale Lautstärke
    public float maxVolume = 1f;           // Maximale Lautstärke

    void Update()
    {
        // Berechne die Entfernung zwischen dem Monster und dem Spieler
        float distance = Vector3.Distance(transform.position, player.position);

        // Berechne die Lautstärke basierend auf der Entfernung
        float volume = Mathf.Clamp01(1 - (distance / maxDistance));
        monsterSound.volume = Mathf.Lerp(minVolume, maxVolume, volume);

        // Optional: Sound starten, wenn er nicht läuft
        if (!monsterSound.isPlaying)
        {
            monsterSound.Play();
        }
    }
}
