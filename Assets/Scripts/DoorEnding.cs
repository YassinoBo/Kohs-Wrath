using System;
using UnityEngine;

public class DoorEnding : MonoBehaviour
{
    public GameObject endScreenCanvas; // Das Endscreen-Canvas
    private bool isDoorOpen = false;
    private SoundScript soundScript;

    private void Start()
    {
        soundScript = SoundScript.Instance;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Linksklick
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
            {
                if (Tastenfeld2.Instance.houseToTutorial.doorOpen) // Tür wurde per Code geöffnet
                {
                    OpenEndScreen();
                }
            }
        }
    }

    void OpenEndScreen()
    {
        if (!isDoorOpen && endScreenCanvas != null)
        {
            endScreenCanvas.SetActive(true);
            soundScript.StopBackgroundSound();
            soundScript.GoodEndingVoiceSound();
            isDoorOpen = true;
            Debug.Log("Endscreen aktiviert!");
        }
    }
}
