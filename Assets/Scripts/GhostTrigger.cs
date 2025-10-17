using UnityEngine;

public class GhostTrigger : MonoBehaviour
{
    public bool IsInZone = false; // Status: Ist der Spieler im Bereich?
    public static GhostTrigger Instance {get; private set;}

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Nur den Spieler erkennen
        {
            IsInZone = true;
        }
        Debug.Log("Drinne");
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Status zurücksetzen, wenn der Spieler den Bereich verlässt
        {
            IsInZone = false;
        }
        Debug.Log("Draussen");

    }

    void Update()
    {
        //Debug.Log(IsInZone);
    }

    public void deactivate()
    {
        gameObject.SetActive(false);
    }
}
