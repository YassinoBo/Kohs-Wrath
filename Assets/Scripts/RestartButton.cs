using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    private SoundScript soundScript;

    public GameObject mainCamera;      // Die Hauptkamera
    public GameObject creditsCamera;   // Die neue Kamera für die Credits

    public GameObject endscreenmanager;
    public GameObject creditsPlane;
    public GameObject canvas;
    public GameObject playerCanvas;
    public GameObject creditsText1;
    public GameObject creditsText2;
    public GameObject creditsText3;
    public GameObject creditsText4;
    public GameObject inventory;

    public RectTransform rectTransform; // Das RectTransform des Panels
    
    
    // Hier referenzierst du das andere Skript, das aktiviert werden soll
    public MonoBehaviour otherScript;
    public bool tränenFließen = false;

    void Start()
    {
        soundScript = SoundScript.Instance;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CreditsGame()
    {
        soundScript.StopHummingSound();
        soundScript.StopEdgarScreamSound();
        Debug.Log("CreditsGame aufgerufen. Versuche Objekte zu aktivieren...");
        Destroy(GameObject.Find("PlayerObj"));
        SceneManager.LoadScene("Credits");

        // Kamerawechsel
        /*if (mainCamera != null && creditsCamera != null)
        {
            mainCamera.SetActive(false);  // Hauptkamera deaktivieren
            creditsCamera.SetActive(true); // Credits-Kamera aktivieren
            Debug.Log("Kamera wurde gewechselt.");
        }

        // Andere Objekte aktivieren/deaktivieren
        endscreenmanager.SetActive(false);
        playerCanvas.SetActive(false);
        inventory.SetActive(false);
        canvas.SetActive(true);
        creditsPlane.SetActive(true);
        creditsText1.gameObject.SetActive(true);
        creditsText2.gameObject.SetActive(true);
        creditsText3.gameObject.SetActive(true);
        creditsText4.gameObject.SetActive(true);
        
        tränenFließen = true;
        */
    }

    public void RestartGame()
    {
        soundScript.StopHummingSound();
        soundScript.StopEdgarScreamSound();
        Destroy(GameObject.Find("PlayerObj"));
        GameStateManager.Instance.IsRestarted = true;
        //DestroyPersistentObjects();
        SceneManager.LoadScene("HouseInnerHalls");
    }
    
}
