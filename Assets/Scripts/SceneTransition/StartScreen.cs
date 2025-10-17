using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro; // Für TextMeshPro

public class StartScreen : MonoBehaviour
{
    // Referenzen für UI-Elemente
    public GameObject optionsPanel;  // Das Panel, das die Lautstärkeregelung enthält
    public Button startButton;       // Der Start-Button
    public Button quitButton;        // Der Quit-Button
    public Slider volumeSlider;      // Der Lautstärke-Slider
    public Slider mouseSpeedSlider;  // Der Mausgeschwindigkeits-Slider
    public TMP_Text musicLabel;      // Text für "Music: "
    public TMP_Text mouseSpeedLabel; // Text für "Mouse Speed: "
    public TMP_Text volumePercentage; // Prozentanzeige für Lautstärke
    public TMP_Text mouseSpeedPercentage; // Prozentanzeige für Mausgeschwindigkeit
    
    private float defaultMouseSpeed = 1.0f; // Standard-Mausgeschwindigkeit
    private bool oneUse = true;
    
    void Start()
    {
        // Setze die Werte der Slider auf aktuelle Einstellungen
        volumeSlider.value = AudioListener.volume;
        volumeSlider.onValueChanged.AddListener(OnVolumeChange); // Lauscht auf Änderungen des Lautstärkereglers
        UpdateVolumePercentage(volumeSlider.value);

        // Mausgeschwindigkeit: Setze den Standardwert oder lade ihn aus den PlayerPrefs (Speicher)
        mouseSpeedSlider.value = PlayerPrefs.GetFloat("MouseSpeed", defaultMouseSpeed);
        mouseSpeedSlider.onValueChanged.AddListener(OnMouseSpeedChange); // Lauscht auf Änderungen des Mausgeschwindigkeits-Sliders
        UpdateMouseSpeedPercentage(mouseSpeedSlider.value);

        //optionsPanel.SetActive(false);  // Anfangs ist das Options-Panel unsichtbar

        // Setze die Labels für die Lautstärke und Mausgeschwindigkeit
        musicLabel.text = "Sound: ";
        mouseSpeedLabel.text = "Sensitivity: ";
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Intro Cutscene");
        
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // Beende das Spiel
#endif
    }

    public void ShowOptions()
    {
        startButton.gameObject.SetActive(false);  // Starte Button ausblenden
        quitButton.gameObject.SetActive(false);   // Quit Button ausblenden
        optionsPanel.SetActive(true);             // Option Panel einblenden
    }

    public void OnVolumeChange(float value)
    {
        AudioListener.volume = value;  // Lautstärke des Spiels ändern
        UpdateVolumePercentage(value); // Aktualisiere die Prozentanzeige
    }

    public void OnMouseSpeedChange(float value)
    {
        PlayerPrefs.SetFloat("MouseSpeed", value); // Speichert die Geschwindigkeit der Maus
        UpdateMouseSpeedPercentage(value); // Aktualisiere die Prozentanzeige
    }

    public void CloseOptions()
    {
        startButton.gameObject.SetActive(true);  // Starte Button wieder einblenden
        quitButton.gameObject.SetActive(true);   // Quit Button wieder einblenden
        optionsPanel.SetActive(false);           // Option Panel ausblenden
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))  // Wenn Escape gedrückt wird
        {
            if (optionsPanel.activeSelf)  // Wenn das Options Panel offen ist
            {
                CloseOptions();  // Schließe die Optionen
            }
        }
    }

    // Aktualisiert die Prozentanzeige des Lautstärke-Sliders
    private void UpdateVolumePercentage(float value)
    {
        int percentage = Mathf.RoundToInt(value * 100); // Konvertiere den Wert (0-1) in Prozent
        volumePercentage.text = percentage + "%"; // Zeigt z. B. "75%" an
    }

    // Aktualisiert die Prozentanzeige des Mausgeschwindigkeits-Sliders
    private void UpdateMouseSpeedPercentage(float value)
    {
        int percentage = Mathf.RoundToInt(value * 100); // Konvertiere den Wert (0-1) in Prozent
        mouseSpeedPercentage.text = percentage + "%"; // Zeigt z. B. "50%" an
    }
}
