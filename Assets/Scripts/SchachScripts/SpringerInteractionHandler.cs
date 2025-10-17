using UnityEngine;

public class SpringerButtonHandler : MonoBehaviour
{
    // Der Springer, der aus dem Inventar entfernt werden soll (die Figur, die sich auf dem Feld befindet und zuerst deaktiviert ist)
    public GameObject springerAufFeld;

    // Der Springer, der im Inventar ist (muss im Inventar sein, damit er entfernt wird)
    public string springerNameImInventar = "Springer"; // Name des Springers im Inventar

    // Der Knopf, der gedrückt wird
    public GameObject button;
    
    public static bool startButtonPressed = false;  // Flag für den erfolgreichen Startknopf

    // Referenz zum Inventar-System
    private bool springerImInventar = false;
    
    private SoundScript soundScript;

    void Start()
    {
        soundScript = SoundScript.Instance;
        // Anfangszustand prüfen
        UpdateSpringerStatus();
    }

    void Update()
    {
        // Überprüfen, ob der Springer im Inventar ist
        UpdateSpringerStatus();
    }

    // Diese Methode prüft, ob der Springer im Inventar ist
    private void UpdateSpringerStatus()
    {
        // Überprüfen, ob der Springer im Inventar des Spielers ist
        springerImInventar = SpielerInventar.Instance.HatObjekt(springerNameImInventar);
    }

    // Diese Methode wird aufgerufen, wenn der Spieler auf den Knopf klickt
    private void OnMouseDown()
    {
        if (!springerImInventar)
        {
            Debug.Log("Du musst den Springer zuerst im Inventar haben!");
            return; // Der Spieler kann nicht fortfahren, wenn der Springer nicht im Inventar ist
        }

        // Den Springer aus dem Inventar entfernen
        SpielerInventar.Instance.EntferneObjekt(springerNameImInventar);

        // Den Springer, der auf dem Feld ist, sichtbar machen
        if (springerAufFeld != null)
        {
            springerAufFeld.SetActive(true);
            startButtonPressed = true;  // Der Startknopf wurde erfolgreich gedrückt
            Debug.Log("Springer wurde sichtbar und kann jetzt bewegt werden!");
        }
        else
        {
            Debug.LogWarning("Kein Springer auf dem Feld zugewiesen!");
        }

        // Optional: Knopfdruck visuell simulieren (z. B. eine Bewegung des Knopfes)
        StartCoroutine(PressButton());
    }

    // Coroutine, um den Knopf visuell zu drücken
    private System.Collections.IEnumerator PressButton()
    {
        // Speichern der ursprünglichen Position des Knopfs
        Vector3 originalPosition = button.transform.localPosition;

        // Bewege den Knopf (nach unten)
        button.transform.localPosition += new Vector3(0f, -0.0078f, 0f);
        soundScript.ArcadeButtonSound();

        // Warte für eine kurze Zeit
        yield return new WaitForSeconds(0.5f);

        // Stelle den Knopf zurück
        button.transform.localPosition = originalPosition;
    }
}
