using UnityEngine;
using UnityEngine.UI;

public class HeartbeatController : MonoBehaviour
{
    public Transform enemy;               // Referenz auf den Gegner
    public AudioSource heartbeat;         // AudioSource für den Herzschlag-Sound
    public RectTransform thermometer;     // RectTransform des Thermometers
    public Image thermometerImage;        // Das UI-Image für die Farbe
    public float maxDistance = 10f;       // Maximale Entfernung
    public float minVolume = 0.1f;        // Minimale Lautstärke
    public float maxVolume = 1f;          // Maximale Lautstärke
    public float minPitch = 0.8f;         // Minimale Geschwindigkeit
    public float maxPitch = 1.5f;         // Maximale Geschwindigkeit

    public Color minColor = Color.green;  // Farbe bei großer Entfernung
    public Color maxColor = Color.red;    // Farbe bei geringer Entfernung

    private Vector2 originalSize;         // Die ursprüngliche Größe des Thermometers

    void Start()
    {
        // Speichere die ursprüngliche Größe des Thermometers
        if (thermometer != null)
        {
            originalSize = thermometer.sizeDelta;
        }
    }

    void Update()
    {
        // Berechne die Entfernung zwischen Spieler und Gegner
        float distance = Vector3.Distance(transform.position, enemy.position);

        // Berechne den Wert für die Füllung (zwischen 0 und 1)
        float fillAmount = Mathf.Clamp01(1 - (distance / maxDistance));

        // Herzschlag synchronisieren
        heartbeat.volume = Mathf.Lerp(minVolume, maxVolume, fillAmount);
        heartbeat.pitch = Mathf.Lerp(minPitch, maxPitch, fillAmount);

        // Aktualisiere die Höhe des Thermometers basierend auf dem Füllwert
        if (thermometer != null)
        {
            thermometer.sizeDelta = new Vector2(originalSize.x, originalSize.y * fillAmount);
        }

        // Ändere die Farbe des Thermometers basierend auf dem Füllwert
        if (thermometerImage != null)
        {
            thermometerImage.color = Color.Lerp(minColor, maxColor, fillAmount);
        }
    }

    public void ResetHeartbeat()
    {
        if (!heartbeat.isPlaying)
        {
            heartbeat.loop = true;
            heartbeat.Play();
        }
    }
}
