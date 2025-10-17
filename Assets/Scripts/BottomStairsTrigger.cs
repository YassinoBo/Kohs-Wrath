using UnityEngine;

public class BottomStairsTrigger : MonoBehaviour
{
    public bool WasBottom = false; // Status: Ist der Spieler im Bereich?
    public static BottomStairsTrigger Instance {get; private set;}
    public GameObject door;

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
            WasBottom = true;
        }
        door.tag = "MainHallToBasement";
        Debug.Log("Drinne");
    }

    void Update()
    {
        //Debug.Log(WasBottom);
    }

    public void deactivate()
    {
        gameObject.SetActive(false);
    }
}
