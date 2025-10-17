using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro; // Für TextMeshPro

public class EscapeMenu : MonoBehaviour
{
    public GameObject buttonPanel;
    public Button resumeButton;
    public Button soundButton;
    public Button usageButton;       
    public Button backButton;        
    public Button quitButton;
    public Slider volumeSlider;      

    private PauseMenu pauseMenu;

    public void Start()
    {
        //pauseMenu = PauseMenu.Instance;
        
        buttonPanel.SetActive(false); 
        
        usageButton.gameObject.SetActive(true);
        backButton.gameObject.SetActive(false);
    }

    public void Update()
    {
        if (pauseMenu.pauseMenuUI.activeSelf)
        {
            usageButton.gameObject.SetActive(true);
        }
        else
        {
            usageButton.gameObject.SetActive(false);
        }
        
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
}