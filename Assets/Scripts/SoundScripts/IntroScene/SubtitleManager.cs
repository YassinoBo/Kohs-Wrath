using System.Collections;
using UnityEngine;
using TMPro;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;

public class SubtitleManager : MonoBehaviour
{
    public TextMeshProUGUI subtitleText; // Zuweisung im Inspector
    public AudioSource audioSource;
    public float displayDuration = 5.0f; 
    public CutsceneManager cutsceneManager;

    private string[] subtitles = {
        "Edgar: And another day done. I wonder what's for dinner.",
        "Edgar: Oh shoot. (clears throat) Hello, he- hello. Alright!",
        "Edgar: Hello. (voice cracks) I mean, hello, Edgar the exterminator speaking. What can I do for you?",
        "Mysterious Caller: ...Am I speaking with Edgar Greaves, the Exterminator?",
        "Edgar: The one and only, how can I help you?",
        "Mysterious Caller: I have a, let's say, SMALL centipede issue. And a little bird told me you were the right person for the job.",
        "Edgar: That little bird tells no lies. There has been quite an uprise in centipede activity lately, and I know exactly what to do with those creatures.",
        "Mysterious Caller: Is that so? Pray tell, what is the best way to get rid of a centipede?",
        "Edgar: It largely depends on the size of the little critters, but generally poison gas does the trick. Afterward, the remains are burned. If the centipedes are more on the larger side, I usually use a stronger poison and chop them up before burning them.",
        "Mysterious Caller: That... won't be necessary, alas the centipedes here are all meager, pitifully so. But hypothetically speaking, what would you do with a human-sized centipede?",
        "Edgar: A human-sized centipede? (laughs) Why I would catch it and sell it to a circus! Not that I've ever seen one larger than a baby's forearm.",
        "Mysterious Caller: You'd be surprised how large some can get. But only hypothetically of course. When can I expect your arrival?",
        "Edgar: I was just on my way home for the day. I'll be there tomorrow bright and early.",
        "Mysterious Caller: That would be far too late. My house is just around that corner by the farm. Just a quick visit should be enough to ease my nerves for the day.",
        "Edgar: Do you mean the corn farm?",
        "Mysterious Caller: Yes, I will be expecting you!",
        "Edgar: ...what an odd fellow. But, a quick inspection won't hurt."
    };
    public AudioClip[] audioClips;
    private int currentSubtitleIndex = 0;

    void Start()
    {
        if (audioClips.Length != subtitles.Length)
        {
            Debug.LogError("Die Anzahl der Audioclips stimmt nicht mit der Anzahl der Untertitel überein!");
            return;
        }

        StartCoroutine(ShowSubtitles());
    }

    IEnumerator ShowSubtitles()
    {
        while (currentSubtitleIndex < subtitles.Length)
        {
            if (currentSubtitleIndex == 0)
            {
                yield return new WaitForSeconds(6.0f);
            }
            if (currentSubtitleIndex == 1)
            {
                cutsceneManager.PlayRingtone();
            }
            if (currentSubtitleIndex == 2)
            {
                cutsceneManager.StopRingtone();
            }

            // Zeige den Untertitel
            subtitleText.text = subtitles[currentSubtitleIndex];

            
            // Spiele den passenden Audioclip ab
            if (audioClips[currentSubtitleIndex] != null)
            {
                audioSource.clip = audioClips[currentSubtitleIndex];
                audioSource.Play();
            }

            // Warte entweder die Länge des Audioclips oder die Standardzeit
            float waitTime = audioClips[currentSubtitleIndex] != null ? audioClips[currentSubtitleIndex].length : displayDuration;
            yield return new WaitForSeconds(waitTime);

            // Zum nächsten Untertitel wechseln
            currentSubtitleIndex++;
        }

        // Nach dem letzten Untertitel leere den Text
        subtitleText.text = "";
    }
}
