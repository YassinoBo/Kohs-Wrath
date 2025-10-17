using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KohScript : MonoBehaviour
{
    public Transform player;
    public float speed = 3.0f;
    public float stoppingDistance = 1.0f;
    public float shakeAmplitude = 0.075f; // Amplitude des Zitterns
    public float shakeFrequency = 25.0f; // Frequenz des Zitterns
    public float phaseOffset = 0.75f; // Verz�gerung zwischen den Sprites
    public GameObject endScreenCanvas;

    private Transform[] childSprites;
    private Vector3[] initialLocalPositions;
    private bool isChasing = false;
    private SoundScript sound;
    private bool gameOverTriggered = false; // Variable, um zu prüfen, ob Game Over ausgelöst wurde

    void Start()
    {
        sound = SoundScript.Instance;
       gameObject.SetActive(false);
        if (player == null)
        {
            // Standardm��ig auf die Hauptkamera ausrichten
            player = Camera.main.transform;
        }

        // Hole alle Kindobjekte (die Sprites) aus der Hierarchie
        childSprites = GetComponentsInChildren<Transform>();
        // Speichere die Anfangspositionen der Sprites
        initialLocalPositions = new Vector3[childSprites.Length];

        for (int i = 0; i < childSprites.Length; i++)
        {
            if (childSprites[i] == transform) continue;
            initialLocalPositions[i] = childSprites[i].localPosition;
        }
    }

    void Update()
    {
        if (!isChasing || player == null) { return; }
        
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance > stoppingDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            transform.LookAt(player);

            for (int i = 0; i < childSprites.Length; i++)
            {
                // Sprite auf das Ziel ausrichten
                childSprites[i].LookAt(player);
                childSprites[i].rotation = Quaternion.Euler(0f, childSprites[i].rotation.eulerAngles.y, 0f);

                // Schl�ngel-Effekt hinzuf�gen
                if (childSprites[i] == transform) continue; // �berspringe das Parent-Objekt
                AddSnakeEffect(childSprites[i], i);

            }
        } 
        else
        {
            TriggerGameOver();
        }
    }

    void AddSnakeEffect(Transform sprite, int index)
    {
        // Berechne die horizontale Verschiebung mit einer Phasenverschiebung
        float offset = Mathf.Sin(Time.time * shakeFrequency + index * phaseOffset) * shakeAmplitude;

        // Aktualisiere die Position des Sprites
        sprite.localPosition = new Vector3(
            initialLocalPositions[index].x + offset,
            initialLocalPositions[index].y,
            initialLocalPositions[index].z
        );
    }

    public void StartChasing()
    {
        gameObject.SetActive(true);
        sound.StopBackgroundSound();
        sound.BadEndingVoiceSound();
        StartCoroutine(ChaseWithDelay());
        
    }

    private IEnumerator ChaseWithDelay()
    {
        yield return new WaitForSeconds(6.0f); // Verzögerung, bevor die Verfolgung beginnt
        isChasing = true;
    }
    

    private void TriggerGameOver()
    {
        if (gameOverTriggered) return; // Verhindert mehrfaches Auslösen

        gameOverTriggered = true; // Setzt den Status auf ausgelöst
        sound.StopBackgroundSound();
        sound.EdgarScreamSound();
        endScreenCanvas.SetActive(true);
        Time.timeScale = 0f; // Spiel pausieren
    }
}