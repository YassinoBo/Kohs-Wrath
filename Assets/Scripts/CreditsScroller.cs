using UnityEngine;
using UnityEngine.SceneManagement; // Für den Szenenwechsel erforderlich

public class CreditsScroller : MonoBehaviour
{
    public float scrollSpeed = 30f; // Geschwindigkeit des Scrollens
    public float timeToSwitchScene = 72f; // Zeit bis zum Szenenwechsel in Sekunden
    private RectTransform rectTransform;
    private float timer = 0f; // Timer zur Verfolgung der Zeit

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            Debug.Log("Credits Canvas ist aktiv.");
        }
        else
        {
            Debug.LogError("Kein RectTransform gefunden!");
        }
    }

    void Update()
    {
        // Credits scrollen
        if (rectTransform != null)
        {
            rectTransform.anchoredPosition += Vector2.up * scrollSpeed * Time.unscaledDeltaTime;
        }

        // Timer hochzählen
        timer += Time.unscaledDeltaTime;

        // Szenenwechsel auslösen, wenn die Zeit abgelaufen ist
        if (timer >= timeToSwitchScene)
        {
            Debug.LogError("Szenenwechsel wird ausgelöst.");
            SceneManager.LoadScene("HouseInnerHalls");
        }
    }
}