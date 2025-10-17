using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class DeathSound : MonoBehaviour
{
    public AudioSource deathSoundSource;     // AudioSource f�r Todes-Sound
    public TextMeshProUGUI deathMessageText; // Text bei Tod
    public Image bloodOverlay;              // Blut-Overlay-Bild
    public AudioSource heartbeatSource;     // Herzschlag-Sound
    public Transform cameraTransform;       // Kamera-Transform

    public IEnumerator DeathSequence(PlayerMovement player)
    {
        // Stoppe den Herzschlag-Sound
        if (heartbeatSource != null && heartbeatSource.isPlaying)
        {
            heartbeatSource.Stop();
        }

        // Spiele den Todes-Sound
        if (deathSoundSource != null && deathSoundSource.clip != null)
        {
            deathSoundSource.Play();
        }

        // Zeige Blut-Overlay
        if (bloodOverlay != null)
        {
            bloodOverlay.gameObject.SetActive(true);
            Color bloodColor = bloodOverlay.color;
            bloodColor.a = 1f;
            bloodOverlay.color = bloodColor;
        }

        // Zeige Todes-Text
        if (deathMessageText != null)
        {
            deathMessageText.gameObject.SetActive(true);
            deathMessageText.text = "Du wurdest get�tet!";
        }



        // Warte, bis der Todes-Sound abgespielt wurde
        yield return new WaitForSeconds(deathSoundSource.clip.length);

        // Respawn
        /*player.transform.position = player.startPosition;
        player.transform.rotation = player.startRotation;*/

        // Herzschlag wieder starten
        if (heartbeatSource != null)
        {
            heartbeatSource.Play();
        }

        // Blende Blut-Overlay und Text aus
        if (bloodOverlay != null)
        {
            bloodOverlay.gameObject.SetActive(false);
        }
        if (deathMessageText != null)
        {
            deathMessageText.gameObject.SetActive(false);
        }
    }
}

    
