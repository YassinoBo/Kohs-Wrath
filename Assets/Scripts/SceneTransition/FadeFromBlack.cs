using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeFromBlack : MonoBehaviour
{
    public float fadeDuration = 4.0f; // Dauer des Fades in Sekunden
    private Image fadeImage;

    void Start()
    {
        // Referenz auf das Image holen
        fadeImage = GetComponent<Image>();

        if (fadeImage != null)
        {
            // Startet den Fade-In-Coroutine
            StartCoroutine(FadeIn());
            StartCoroutine(FadeOut());
        }
    }

    public void StartFadeOut()
    {
        // Aktiviert das GameObject und startet den Fade-Out-Coroutine
        if (fadeImage != null)
        {
            fadeImage.gameObject.SetActive(true); // Aktivieren, falls es deaktiviert ist
            StartCoroutine(FadeOut());
        }
    }

    IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        Color startColor = fadeImage.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0); // Ziel: Transparent

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fadeImage.color = Color.Lerp(startColor, endColor, elapsedTime / fadeDuration);
            yield return null; // Warten auf den n�chsten Frame
        }

        // Sicherstellen, dass das Bild vollst�ndig transparent ist
        fadeImage.color = endColor;

    }
    IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        Color endColor = fadeImage.color;
        Color startColor = new Color(endColor.r, endColor.g, endColor.b, 0); // Transparent

        yield return new WaitForSeconds(150.0f);

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fadeImage.color = Color.Lerp(startColor, endColor, elapsedTime / fadeDuration);
            yield return null; // Warten auf den n�chsten Frame
        }

        // Sicherstellen, dass das Bild vollst�ndig transparent ist
        fadeImage.color = endColor;

        SceneManager.LoadScene("Tutorial Area"); // Lade die Tutorial Area

    }
}
