using UnityEngine;
using UnityEngine.UI;

public class ThermometerWithoutSprite : MonoBehaviour
{
    public Transform enemy;               // Referenz auf den Gegner
    public Transform player;              // Referenz auf den Spieler
    public RectTransform thermometerRect; // Das Thermometer (UI-Image RectTransform)
    public float maxDistance = 10f;       // Maximale Entfernung

    public Color farColor = Color.green;  // Farbe bei großer Entfernung
    public Color nearColor = Color.red;   // Farbe bei geringer Entfernung

    private Image thermometerImage;       // Das UI-Image für die Farbe

    void Start()
    {
        // Referenz zum Image-Component
        thermometerImage = thermometerRect.GetComponent<Image>();
    }

    void Update()
    {
        // Berechne die Entfernung zwischen Spieler und Gegner
        float distance = Vector3.Distance(player.position, enemy.position);

        // Berechne die relative Entfernung (0 = nah, 1 = weit)
        float normalizedDistance = Mathf.Clamp01(1 - (distance / maxDistance));

        // Passen die Höhe des Thermometers an (Füllung von unten nach oben)
        thermometerRect.localScale = new Vector3(1, normalizedDistance, 1);

        // Ändere die Farbe basierend auf der Entfernung
        thermometerImage.color = Color.Lerp(farColor, nearColor, normalizedDistance);
    }
}
