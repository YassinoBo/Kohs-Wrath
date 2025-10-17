using System.Collections;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    public AudioSource backgroundAudioSource; // Für Hintergrundmusik oder andere Sounds
    public AudioSource ringtoneAudioSource;   // Für den Klingelton

    void Start()
    {
        // Beispiel: Hintergrundsound abspielen
        if (backgroundAudioSource != null)
        {
            backgroundAudioSource.Play();
        }
    }

    public void PlayRingtone()
    {
        if (ringtoneAudioSource != null && !ringtoneAudioSource.isPlaying)
        {
            ringtoneAudioSource.Play(); // Klingelton abspielen
        }
    }
    public void StopRingtone()
    {
        if (ringtoneAudioSource != null)
        {
            ringtoneAudioSource.Stop(); // Klingelton abspielen
        }
    }
}
