using UnityEngine;

public class GoodNote : MonoBehaviour
{
    public GameObject note;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Nur den Spieler erkennen
        {
            ZeigNote();
        }
    }

    private void ZeigNote()
    {
        if (note != null)
        {
            note.SetActive(true);
            Debug.Log("Das Objekt wurde aktiviert!");
        }
        else
        {
            Debug.LogWarning("Kein Objekt zugewiesen!");
        }
    }
}
