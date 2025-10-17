using System;
using UnityEngine;

public class HeadTrigger : MonoBehaviour
{
    public bool playerInZone = false; // Status: Ist der Spieler im Bereich?

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Nur den Spieler erkennen
        {
            playerInZone = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Status zurücksetzen, wenn der Spieler den Bereich verlässt
        {
            playerInZone = false;
        }
    }

    public void deactivate()
    {
        gameObject.SetActive(false);
    }
}
