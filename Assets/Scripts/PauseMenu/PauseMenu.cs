using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;  // UI-Men�
    public Slider volumeSlider;     // Lautst�rke-Slider
    public Text muteButtonText;     // Button-Text f�r Stummschaltung
    
    public GameObject buttonPanel;
    public Button resumeButton;
    public Button soundButton;
    public Button usageButton;       
    public Button backButton;        
    public Button quitButton;

    public bool isPaused = false;
    private bool isMuted = false;

    public Slider sensitivitySlider; // Slider zur Einstellung der Sensitivität
    public MoveCam moveCam;

    public static PauseMenu Instance {get; private set;}

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); 
        }
    }
    
    void Start()
    {
        // Lautst�rke auf 1 initialisieren
        SetGlobalVolume(1f);

        if (volumeSlider != null)
        {
            volumeSlider.value = 0.2f;
            volumeSlider.onValueChanged.AddListener(SetGlobalVolume);
        }


        // Aktualisieren des Texts auf dem Button beim Start
        if (isMuted)
        {
            muteButtonText.text = "Unmute";  // oder was auch immer Sie f�r "Stummgeschaltet" verwenden m�chten
        }
        else
        {
            muteButtonText.text = "Mute";    // oder was auch immer Sie f�r "Ton an" verwenden m�chten
        }

        buttonPanel.SetActive(false); 
        
        usageButton.gameObject.SetActive(true);
        backButton.gameObject.SetActive(false);
        
        // Maussteuerung beim Start sperren
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        if (GameStateManager.Instance != null && GameStateManager.Instance.IsRestarted)
        {
            ResumeGame(); // Aktiviert das Pause-Menü
            GameStateManager.Instance.IsRestarted = false; // Setzt den Zustand zurück
        }
        else
        {
            ResumeGame();
        }

        // Initialisiere den Slider mit einem gespeicherten Wert oder einem Standardwert
        sensitivitySlider.value = PlayerPrefs.GetFloat("MouseSensitivity", 0.5f);
        sensitivitySlider.onValueChanged.AddListener(SetSensitivity);

        // Setze die anfängliche Sensitivität basierend auf dem Slider-Wert
        SetSensitivity(sensitivitySlider.value);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }


    public void SetSensitivity(float sensitivity)
    {
        // Stelle sicher, dass MoveCam verfügbar und anpassbar ist
        if (moveCam != null)
        {
            moveCam.sens = sensitivity * 1000; 
        }
        // Speichere die Einstellung
        PlayerPrefs.SetFloat("MouseSensitivity", sensitivity);
    }


    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void PauseGame()
    {
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(true);
        }
        
        Time.timeScale = 0f;
        isPaused = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
    public void ShowButtonUsage()
    {
        buttonPanel.SetActive(true); 
        
        resumeButton.gameObject.SetActive(false);  
        soundButton.gameObject.SetActive(false);
        usageButton.gameObject.SetActive(false);
        backButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(false);
        volumeSlider.gameObject.SetActive(false);             
    }

    public void CloseButtonUsage()
    {
        buttonPanel.SetActive(false); 
        
        resumeButton.gameObject.SetActive(true);
        soundButton.gameObject.SetActive(true);
        usageButton.gameObject.SetActive(true);
        backButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(true);
        volumeSlider.gameObject.SetActive(true);
    }

    // Setzt die Lautst�rke f�r alle Audioquellen in der Szene

    /*
    public void SetGlobalVolume(float volume)
    {
        Debug.Log("Volume Set to: " + volume);  // Zum Debuggen
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();

        foreach (AudioSource audioSource in allAudioSources)
        {
            audioSource.volume = volume;
        }
    }
    */

    public void SetGlobalVolume(float volume)
    {
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audioSource in allAudioSources)
        {
            audioSource.volume = volume;
        }

        // Button-Text basierend auf der Lautst�rke aktualisieren
        if (volume == 0f)
        {
            muteButtonText.text = "Unmute"; // Text wenn stummgeschaltet
            isMuted = true;                // Zustand auf Muted setzen
        }
        else
        {
            muteButtonText.text = "Mute";  // Text wenn nicht stummgeschaltet
            isMuted = false;               // Zustand auf Unmuted setzen
        }
    }


    // Mute-Button: Schaltet Sound an/aus und aktualisiert Slider

    /*
    public void ToggleMute()
    {
        isMuted = !isMuted;
        float newVolume = isMuted ? 0f : 1f;

        if (volumeSlider != null)
        {
            volumeSlider.value = newVolume;
        }

        SetGlobalVolume(newVolume);
        UpdateAudioSourcesMuteState(isMuted); // Diese Funktion muss implementiert werden
    }
    */

    public void ToggleMute()
    {
        isMuted = !isMuted;
        float newVolume = isMuted ? 0f : 1f;

        if (volumeSlider != null)
        {
            volumeSlider.value = newVolume;
        }

        SetGlobalVolume(newVolume);

        // Update des Button-Textes basierend auf dem Mute-Zustand
        if (isMuted)
        {
            muteButtonText.text = "Unmute";  // Text f�r "Sound ist aus"
        }
        else
        {
            muteButtonText.text = "Mute";    // Text f�r "Sound ist an"
        }
    }


    private void UpdateAudioSourcesMuteState(bool isMuted)
    {
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audioSource in allAudioSources)
        {
            audioSource.mute = isMuted; // Stummschaltung direkt anwenden
        }
    }



    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
