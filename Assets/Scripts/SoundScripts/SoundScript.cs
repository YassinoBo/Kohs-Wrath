using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SoundScript : MonoBehaviour
{
    private DoorInteraction interaction;
    public AudioSource DoorSoundSource;
    public AudioSource ButtonSoundSource;
    public AudioSource BackgroundSoundSource;
    //public AudioSource Background2SoundSource;
    public AudioSource SecondCamBackgroundSoundSource;
    public AudioSource LockedDoorSoundSource;
    public AudioSource UnlockedDoorSoundSource;
    public AudioSource LeverSoundSource;
    public AudioSource Ghost2SoundSource;
    public AudioSource HeartbeatSoundSource;
    public AudioSource SpindScareSoundSource;
    private AudioSource Spind2ScareSoundSource;
    public AudioSource ScareSoundSource;
    public AudioSource OilLampOffSoundSource;
    public AudioSource RedGhostVoiceSoundSource;
    public AudioSource StairsSoundSource;
    public AudioSource MysterySoundSource;
    public AudioSource WhiteGhostScareSoundSource;
    public AudioSource WhiteGhostMusikSoundSource;
    private AudioSource SpindSoundSource;
    public AudioSource ArcadeButtonSoundSource;
    public AudioSource HummingSoundSource;
    public AudioSource ChestSoundSource;
    public AudioSource Vent1SoundSource;
    public AudioSource Vent2SoundSource;
    public AudioSource Vent3SoundSource;
    public AudioSource Vent4SoundSource;
    public AudioSource GlassSoundSource;
    public AudioSource EdgarScreamSoundSource;
    public AudioSource CrawlingSoundSource;
    public AudioSource GoodEndingVoiceSoundSource;
    public AudioSource BadEndingVoiceSoundSource;
    public AudioSource TutVoiceSoundSource;
    public AudioSource GateSoundSource;
    private bool oneUse = true;
    public static SoundScript Instance {get; private set;}

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
        interaction = GetComponent<DoorInteraction>();
        BackgroundSound();

    }

    public void TutVoiceSound()
    {
        TutVoiceSoundSource.Play();
    }
    public void GoodEndingVoiceSound()
    {
        GoodEndingVoiceSoundSource.Play();
    }

    public void BadEndingVoiceSound()
    {
        BadEndingVoiceSoundSource.Play();
    }

    public void CrawlingSound()
    {
        Debug.Log("Crawling sound");
        if (oneUse) // Überprüfen, ob der Sound bereits abgespielt wird
        {
            Debug.Log("Crawling");
            oneUse = false;
            CrawlingSoundSource.volume = 0.5f;
            CrawlingSoundSource.Play();
        }
    }

    public void EdgarScreamSound()
    {
        EdgarScreamSoundSource.Play();
    }
    
    public void StopEdgarScreamSound()
    {
        EdgarScreamSoundSource.Stop();
    }


    public void GlassSound()
    {
        GlassSoundSource.Play();
    }

    public void BackgroundSound()
    {
        //Background2SoundSource.Stop();
        SecondCamBackgroundSoundSource.Stop();
        BackgroundSoundSource.Play();
    }
    
    public void StopBackgroundSound()
    {
        BackgroundSoundSource.Stop();
    }
    
    public void SecondCamBackgroundSound()
    {
        //Background2SoundSource.Stop();
        BackgroundSoundSource.Stop();
        SecondCamBackgroundSoundSource.Play();
    }

    /*public void Background2Sound()
    {
        BackgroundSoundSource.Stop();
        Background2SoundSource.volume = 0.1f;
        Background2SoundSource.Play();
    }*/

    public void DoorSound()
    {
        DoorSoundSource.Play();
    }

    public void LockedDoorSound()
    {
        LockedDoorSoundSource.Play();
    }

    public void ButtonSound(GameObject targetObject)
    {
        if (targetObject == null)
        {
            Debug.LogWarning("TargetObject oder SoundClip ist nicht gesetzt.");
            return;
        }
        
        AudioSource sound = targetObject.GetComponent<AudioSource>();
        sound.volume = 0.8f;
        sound.Play();
    }

    public void LeverSound(GameObject targetObject)
    {
        if (targetObject == null)
        {
            Debug.LogWarning("TargetObject oder SoundClip ist nicht gesetzt.");
            return;
        }
        
        AudioSource sound = targetObject.GetComponent<AudioSource>();
        sound.volume = 0.5f;
        sound.Play();
    }

    public void Ghost2Sound()
    {
        Ghost2SoundSource.Play();
    }

    public void UnlockedDoorSound()
    {
        UnlockedDoorSoundSource.Play();
    }

    public void HeartbeatSound()
    {
        StartCoroutine(PlayHeartbeatTwice());
    }

    private IEnumerator PlayHeartbeatTwice()
    {
        HeartbeatSoundSource.Play();
        yield return new WaitForSeconds(HeartbeatSoundSource.clip.length);
        HeartbeatSoundSource.Play();
    }

    public void ScareSound()
    {
        ScareSoundSource.Play();
    }

    public void SpindSound(GameObject targetObject)
    {
        if (targetObject == null)
        {
            Debug.LogWarning("TargetObject oder SoundClip ist nicht gesetzt.");
            return;
        }
        
        AudioSource sound = targetObject.GetComponent<AudioSource>();
        
        //targetAudioSource.spatialBlend = 1.0f; // Setze auf 3D-Sound
        sound.volume = 0.7f;
        sound.Play();
    }

    public void SpindScareSound()
    {
        SpindScareSoundSource.Play();
    }

    public void Spind2ScareSound()
    {
        Spind2ScareSoundSource.Play();
    }

    public void OilLampOffSound()
    {
        OilLampOffSoundSource.Play();
    }

    public void RedGhostVoiceSound()
    {
        RedGhostVoiceSoundSource.Play();
    }
    
    public void KeyPadSound(GameObject targetObject)
    {
        if (targetObject == null)
        {
            return;
        }
        AudioSource sound = targetObject.GetComponent<AudioSource>();

        sound.volume = 0.1f;
        sound.Play();
    }
    
    public void KeyPadIncorrectSound(GameObject targetObject)
    {
        if (targetObject == null)
        {
            return;
        }

        AudioSource[] audioSources = targetObject.GetComponents<AudioSource>();

        foreach (AudioSource source in audioSources)
        {
            if (source.clip != null && source.clip.name == "Ping")
            {
                source.volume = 0.1f;
                source.Play();
                break;
            }
        }
    }
    
    public void PaperSound(GameObject targetObject)
    {
        if (targetObject == null)
        {
            return;
        }
        AudioSource sound = targetObject.GetComponent<AudioSource>();

        sound.volume = 0.5f;
        sound.Play();
    }
    
    public void KeyPickupSound(GameObject targetObject)
    {
        if (targetObject == null)
        {
            return;
        }
        AudioSource sound = targetObject.GetComponent<AudioSource>();
        sound.volume = 1f;
        sound.Play();
    }

    public void StairsSound()
    {
        StairsSoundSource.Play();
    }

    public void MysterySound()
    {
        MysterySoundSource.Play();
    }

    public void WhiteGhostScareSound()
    {
        WhiteGhostMusikSoundSource.Play();
    }

    public void ArcadeButtonSound()
    {
        ArcadeButtonSoundSource.Play();
    }

    public void HummingSound()
    {
        HummingSoundSource.volume = 3f;
        HummingSoundSource.Play();
    }
    
    public void StopHummingSound()
    {
        HummingSoundSource.Stop();
    }

    public void ChestSound()
    {
        ChestSoundSource.Play();
    }

    public void Vent1Sound()
    {
        Vent1SoundSource.Play();
    }
    
    public void Vent2Sound()
    {
        Vent2SoundSource.Play();
    }
    
    public void Vent3Sound()
    {
        Vent3SoundSource.Play();
    }
    
    public void Vent4Sound()
    {
        Vent4SoundSource.Play();
    }

    public void GateSound()
    {
        GateSoundSource.Play();
    }
}
