using System;
using UnityEngine;

public class GateTrigger : MonoBehaviour
{
    public GameObject leftFence; // Der linke Zaun
    public GameObject rightFence; // Der rechte Zaun
    public float rotationAngle = 45f; // Drehwinkel
    private bool hasTriggered = false; // Verhindert mehrmaliges Auslösen
    private SoundScript soundScript;

    private void Start()
    {
        soundScript = SoundScript.Instance;
    }

    void OnTriggerEnter(Collider other)
    {
        // Überprüfen, ob der Spieler den Trigger betritt
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true; // Sicherstellen, dass dies nur einmal passiert

            // Linken Zaun drehen
            if (leftFence != null)
            {
                leftFence.transform.Rotate(0, 0, rotationAngle);
            }

            // Rechten Zaun drehen
            if (rightFence != null)
            {
                rightFence.transform.Rotate(0, 0, -rotationAngle);
                soundScript.GateSound();

            }
        }
    }
}