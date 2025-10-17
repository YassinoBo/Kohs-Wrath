using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SkipButtonManager : MonoBehaviour
{
    public Button skipButton;        // Der Button, um die Szene zu überspringen
    public Image fadeImage;          // Das Image für den Fade-Effekt
    public float inactivityTime = 3f; // Zeit ohne Eingabe, nach der der Button verschwindet
    public float fadeDuration = 4.0f; // Dauer des Fade-Effekts

    private Vector3 lastMousePosition; // Speichert die letzte Mausposition
    private float lastInputTime;       // Zeitpunkt der letzten Eingabe

    private void Start()
    {
        // Stelle sicher, dass der Skip-Button zu Beginn sichtbar ist
        skipButton.gameObject.SetActive(true);
        lastInputTime = Time.time;
        lastMousePosition = Input.mousePosition;

        // Füge eine Listener-Funktion für den Skip-Button hinzu
        skipButton.onClick.AddListener(OnSkipButtonClick);
    }

    private void Update()
    {
        // Überprüfe Mausbewegung oder Tastatureingabe
        if (Input.mousePosition != lastMousePosition)
        {
            lastInputTime = Time.time; // Setze den Zeitpunkt der letzten Eingabe zurück
            lastMousePosition = Input.mousePosition;

            // Zeige den Button, falls er ausgeblendet war
            if (!skipButton.gameObject.activeSelf)
            {
                skipButton.gameObject.SetActive(true);
            }
        }

        // Verberge den Button nur, wenn die Inaktivitätszeit überschritten wurde
        if (Time.time - lastInputTime >= inactivityTime)
        {
            skipButton.gameObject.SetActive(false);
        }
    }

    // Wenn der Skip-Button geklickt wird, starten wir den Fade-Out-Effekt und laden die nächste Szene
    public void OnSkipButtonClick()
    {
        StartCoroutine(FadeOutAndLoadScene());
    }

    private IEnumerator FadeOutAndLoadScene()
    {
        // Stelle sicher, dass das Fade-Image aktiv ist
        fadeImage.gameObject.SetActive(true);

        // Fade-Out-Effekt starten
        float elapsedTime = 0f;
        Color startColor = fadeImage.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1); // Voll sichtbar

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fadeImage.color = Color.Lerp(startColor, endColor, elapsedTime / fadeDuration);
            yield return null;
        }

        // Sicherstellen, dass das Bild voll sichtbar ist
        fadeImage.color = endColor;

        // Lade die nächste Szene nach dem Fade-Out
        SceneManager.LoadScene("Tutorial Area"); // Ersetze "Tutorial Area" mit deinem Szenennamen
    }
}
