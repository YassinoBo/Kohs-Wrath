using UnityEngine;

public class TestPanelMovement : MonoBehaviour
{
    public RectTransform rectTransform; // Das RectTransform des Panels
    public float scrollSpeed = 10f;     // Geschwindigkeit des Scrollens
    public RestartButton restartButton;
    
    void Update()
    {
        if (restartButton.tränenFließen)
        {
            if (rectTransform != null)
            {
                // Hole die aktuelle Position (anchoredPosition) des RectTransform
                Vector2 currentPosition = rectTransform.anchoredPosition;
    
                // Bewege den Y-Wert nach oben
                currentPosition.y += scrollSpeed * Time.deltaTime;
    
                // Setze die neue Position (anchoredPosition)
                rectTransform.anchoredPosition = currentPosition;
    
                // Debugge die Position nach der Zuweisung
                Debug.Log("Neue Position Y nach der Veränderung: " + rectTransform.anchoredPosition.y);
            }
            else
            {
                Debug.LogError("RectTransform nicht zugewiesen!");
            }
        }

    }
}