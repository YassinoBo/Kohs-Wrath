using UnityEngine;

public class SanityLossScript : MonoBehaviour
{
    public SanityTestScript sanityTestScript; // Referenz auf das SanityTestScript
    public float sanityLossValue = 10f;       // Fester Wert des Sanity-Verlusts

    void Start()
    {
        if (sanityTestScript == null)
        {
            GameObject player = GameObject.FindWithTag("MainCamera");
            if (player != null)
            {
                sanityTestScript = player.GetComponent<SanityTestScript>();
            }

            if (sanityTestScript == null)
            {
                Debug.LogError("SanityTestScript konnte nicht gefunden werden! Verknüpfe das Skript im Inspector oder überprüfe den Spieler.");
            }
        }
    }

    public void LoseSanity()
    {
        if (sanityTestScript == null)
        {
            Debug.LogError("SanityTestScript ist nicht gesetzt! Überprüfe die Verknüpfung.");
            return;
        }

        sanityTestScript.sanity -= sanityLossValue;
        if (sanityTestScript.sanity < 0)
        {
            sanityTestScript.sanity = 0; // Sicherheit: Sanity darf nicht negativ werden
        }

        Debug.Log($"Sanity um {sanityLossValue} reduziert. Aktuelle Sanity: {sanityTestScript.sanity}");
    }
}