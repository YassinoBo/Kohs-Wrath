using UnityEngine;

public class DoorKeyCheck : MonoBehaviour
{
    public string benoetigterSchluessel; // Name des benötigten Schlüssels (optional)
    public bool istVerschlossen = true; // Tür ist standardmäßig verschlossen

    private SoundScript soundScript;

    void Start()
    {
        soundScript = FindObjectOfType<SoundScript>();
        if (istVerschlossen && string.IsNullOrEmpty(benoetigterSchluessel))
        {
            Debug.LogWarning("Die Tür ist verschlossen, aber kein Schlüssel ist zugewiesen!");
        }
    }

    public bool IstZugangErlaubt()
    {
        // Wenn die Tür nicht verschlossen ist, Zugang gewähren
        if (!istVerschlossen) return true;

        // Wenn ein Schlüssel benötigt wird, prüfen, ob der Spieler den Schlüssel hat
        if (SpielerInventar.Instance.HatObjekt(benoetigterSchluessel))
        {
            // Schlüssel verwenden und Tür öffnen
            SpielerInventar.Instance.EntferneObjekt(benoetigterSchluessel); // Schlüssel entfernen
            istVerschlossen = false; // Tür öffnen

            // Optional: Sound für das Öffnen der Tür
            if (soundScript != null)
            {
                soundScript.UnlockedDoorSound();
            }

            Debug.Log("Tür geöffnet.");
            return false;
        }

        // Wenn der Schlüssel fehlt, Sound abspielen und Zugriff verweigern
        if (soundScript != null)
        {
            soundScript.LockedDoorSound();
        }

        Debug.Log("Zugang verweigert. Schlüssel erforderlich: " + benoetigterSchluessel);
        return false;
    }
}