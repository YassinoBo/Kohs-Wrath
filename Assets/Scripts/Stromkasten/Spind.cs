using System.Collections;
using UnityEngine;

public class Spind : MonoBehaviour
{
    public Transform tuere; // Referenz auf die Tür des Spinds
    public bool isOpen = false; // Status der Tür
    
    public Lampensteuerung lampe; // Referenz auf die Lampensteuerung des Spinds
    
    private SoundScript sound;

    private static bool oneUse = true;

    public float scareCloseDistance = 2f; 
    public Transform player;
    public GameObject Head;
    private Camera playerCamera; 
    
    public float pushBackDistance = 10f; 
    public float pushBackSpeed = 4f; 
    public float shakeMagnitude = 0.1f; // Intensität des Zitterns
    
    private RedGhostScript redGhost;
    private GhostScript blackGhost;

    void Start()
    {
        sound = SoundScript.Instance;

        // Fallback: Spieler automatisch finden, falls nicht im Editor zugewiesen
        if (player == null && GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        
        if (Head == null && GameObject.FindGameObjectWithTag("Head") != null)
        {
            Head = GameObject.FindGameObjectWithTag("Head");
        }
        
            playerCamera = Camera.main;
            redGhost = RedGhostScript.Instance;
            blackGhost = GhostScript.Instance;
    }

    void Update()
    {
        if(player == null || gameObject == null) return;
        if (oneUse && Vector3.Distance(player.position, transform.position) <= scareCloseDistance && gameObject.CompareTag("Spind2") && isOpen)
        {
            blackGhost.SetAllowAttack(false);
            oneUse = false; 
            closeScare();
        }
    }

    // Methode zum Öffnen der Tür
    public void OeffneTuere()
    {
        if (!isOpen)
        {
            sound.SpindSound(gameObject);

            if (gameObject.CompareTag("Spind2") && oneUse)
            {
                blackGhost.SetAllowAttack(false);
            }
            if (gameObject.CompareTag("Spind3"))
            {
                blackGhost.SetAllowAttack(false);
                tuere.Rotate(90f, 0f, 0f);
                tuere.position = tuere.position - new Vector3(0f, 0.8f, -1f);
                sound.HeartbeatSound();
                redGhost.setBasementEvent(true);
                redGhost.activate();
                /*Head.transform.position = redGhost.transform.position;
                Head.transform.position -= new Vector3(0f, 0.4f, 0f); 
                Head.transform.Rotate(0f, 0f, 180f);*/
            }
            else
            {
                tuere.Rotate(0, -90, 0); // Tür um -90 Grad um die Y-Achse drehen
            }
            
            isOpen = true;

            // Lampe auf grün setzen
            if (lampe != null)
            {
                lampe.SetLampeGruen();
            }
        }
    }

    private IEnumerator SchliesseTuereNachWarten()
    {
        yield return new WaitForSeconds(5f);
        closeScare();
    }

    public void closeScare()
    {
        sound.ScareSound();
        tuere.Rotate(0, 90, 0);
        //sound.SpindScareSound();
        sound.HeartbeatSound();

        // Spieler nach vorne verschieben (positive Z-Richtung)
        StartCoroutine(PushPlayerForward());

        // Kamera zittern lassen während des gesamten Push-Vorgangs
        StartCoroutine(CameraShake());

        StartCoroutine(oeffne());
    }
    
    private IEnumerator PushPlayerForward()
    {
        Vector3 originalPosition = player.position; 
        Vector3 targetPosition = player.position + new Vector3(0, 0, pushBackDistance);

        float journeyLength = Vector3.Distance(originalPosition, targetPosition);
        float pushDuration = journeyLength / pushBackSpeed; // Zeit für den Push

        float elapsed = 0f;

        // Bewege den Spieler ohne Verzögerung
        while (elapsed < pushDuration)
        {
            elapsed += Time.deltaTime;
            float fractionOfJourney = elapsed / pushDuration;
            player.position = Vector3.Lerp(originalPosition, targetPosition, fractionOfJourney);

            yield return null; // Warten auf den nächsten Frame
        }

        // Sicherstellen, dass der Spieler genau am Ziel ankommt
        player.position = targetPosition;
    }


    // Methode, um die Kamera zum Zittern zu bringen
    private IEnumerator CameraShake()
    {
        Vector3 originalPos = playerCamera.transform.localPosition;

        float journeyLength = pushBackDistance / pushBackSpeed; // Zeit für den gesamten Push
        float elapsed = 0f;

        while (elapsed < journeyLength)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            // Neue Position der Kamera mit Zittern
            playerCamera.transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null; // Warten auf den nächsten Frame
        }

        // Kamera wieder auf die ursprüngliche Position setzen
        playerCamera.transform.localPosition = originalPos;
    }

    private IEnumerator oeffne()
    {
        // 5 Sekunden warten
        yield return new WaitForSeconds(5f);

        // Spind-Status zurücksetzen
        isOpen = false;
        
        blackGhost.SetAllowAttack(true);
    }
}
