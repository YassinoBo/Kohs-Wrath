using UnityEngine;

public class SnakeEffect : MonoBehaviour
{
    public Transform target; // Der Spieler oder die Kamera, auf die die Sprites ausgerichtet werden sollen
    public float shakeAmplitude = 0.075f; // Amplitude des Zitterns
    public float shakeFrequency = 25.0f; // Frequenz des Zitterns
    public float phaseOffset = 0.75f; // Verzögerung zwischen den Sprites

    private Transform[] childSprites;
    private Vector3[] initialPositions;

    void Start()
    {
        if (target == null)
        {
            // Standardmäßig auf die Hauptkamera ausrichten
            target = Camera.main.transform;
        }

        // Hole alle Kindobjekte (die Sprites) aus der Hierarchie
        childSprites = GetComponentsInChildren<Transform>();

        // Speichere die Anfangspositionen der Sprites
        initialPositions = new Vector3[childSprites.Length];
        for (int i = 0; i < childSprites.Length; i++)
        {
            initialPositions[i] = childSprites[i].position;
        }
    }

    void Update()
    {
        for (int i = 0; i < childSprites.Length; i++)
        {
            // Sprite auf das Ziel ausrichten
            childSprites[i].LookAt(target);
            childSprites[i].rotation = Quaternion.Euler(0f, childSprites[i].rotation.eulerAngles.y, 0f);

            // Schlängel-Effekt hinzufügen
            AddSnakeEffect(childSprites[i], i);

            /*AddShakeEffect();*/
        }
    }
    /*void AddShakeEffect()
    {
        // Berechnet die horizontale Verschiebung mit einer Sinuswelle
        float offset = Mathf.Sin(Time.time * shakeFrequency) * shakeAmplitude;

        // Verschiebt das Sprite basierend auf der Schwingung
        transform.position = new Vector3(initialPosition.x + offset, initialPosition.y, initialPosition.z);
    }*/

    void AddSnakeEffect(Transform sprite, int index)
    {
        // Berechne die horizontale Verschiebung mit einer Phasenverschiebung
        float offset = Mathf.Sin(Time.time * shakeFrequency + index * phaseOffset) * shakeAmplitude;

        // Aktualisiere die Position des Sprites
        sprite.position = new Vector3(
            initialPositions[index].x + offset,
            initialPositions[index].y,
            initialPositions[index].z
        );
    }
}
