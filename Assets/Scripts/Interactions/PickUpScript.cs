using UnityEngine;

public class PickUpScript : MonoBehaviour
{
    public string name; // Der Name oder die ID des Objekts (eindeutig für jede Tür)
    public Sprite sprite;  // Das Sprite des Objekts, das im Inventar angezeigt wird
    public float sichtEntfernung = 1.5f; // Maximaler Abstand, in dem das Objekt sichtbar ist

    private bool kannAufgenommenWerden = false; // Ob der Spieler das Objekt aufnehmen kann
    private bool ghostEvent = false;
    private SoundScript sound;

    private void Start()
    {
        sound = SoundScript.Instance;
    }
    
    // Methode zum Aufheben des Objekts
    public void Aufheben()
    {
        // Überprüfen, ob das Inventar nicht voll ist
        if (!SpielerInventar.Instance.IstVoll())
        {
            // Objekt aus der Szene entfernen (unsichtbar machen)
            if (gameObject.CompareTag("Key 1") || gameObject.CompareTag("Key 3") )
            {
                sound.KeyPickupSound(gameObject);
            }
            
            gameObject.SetActive(false);
            
            if (gameObject.CompareTag("Head"))
            {
                ghostEvent = true;
            }

            // Füge das Objekt zum Inventar des Spielers hinzu
            SpielerInventar.Instance.FuegeObjektHinzu(name, sprite);

            // Log für Debugging
            Debug.Log("Objekt aufgesammelt: " + name);
        }
        else
        {
            Debug.Log("Inventar ist voll! Kein weiteres Objekt kann aufgenommen werden.");
        }
    }

    // Überprüft, ob das Objekt im Sichtfeld des Spielers ist
    bool KannObjektSehen()
    {
        if (Camera.main == null)
        {
            return false;
        }
        RaycastHit hit;
        Vector3 richtungZurKamera = transform.position - Camera.main.transform.position;

        // Raycast von der Kamera aus, um zu überprüfen, ob der Spieler das Objekt sieht
        if (Physics.Raycast(Camera.main.transform.position, richtungZurKamera.normalized, out hit, sichtEntfernung))
        {
            // Wenn der Raycast das Objekt trifft, kann er aufgenommen werden
            if (hit.collider.gameObject == gameObject)
            {
                return true;
            }
        }

        return false; // Das Objekt ist entweder zu weit entfernt oder nicht sichtbar
    }

    void Update()
    {
        // Überprüfen, ob der Spieler das Objekt sehen kann
        if (KannObjektSehen())
        {
            kannAufgenommenWerden = true;
        }
        else
        {
            kannAufgenommenWerden = false;
        }

        // Überprüfen, ob der Spieler die Taste E drückt, wenn das Objekt sichtbar ist
        if (Input.GetMouseButton(0) && kannAufgenommenWerden)
        {
            Aufheben(); // Objekt aufheben
        }
    }

    // Wenn der Spieler mit dem Objekt kollidiert
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera")) // Spieler-Objekt muss das Tag "MainCamera" haben
        {
            Aufheben(); // Objekt aufheben
        }
    }

    void OnDrawGizmos()
    {
        // Optional: Zeige die maximale Sichtentfernung im Editor als Gizmo
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sichtEntfernung);
    }
}