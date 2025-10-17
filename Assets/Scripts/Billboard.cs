using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform target; // Der Spieler oder die Kamera, auf die das Sprite ausgerichtet werden soll
    public float shakeAmplitude = 0.025f; // Amplitude des Zitterns
    public float shakeFrequency = 30.0f; // Frequenz des Zitterns

    private Vector3 initialPosition;

    void Start()
    {
        if (target == null)
        {
            // Standardm‰ﬂig auf die Hauptkamera ausrichten
            target = Camera.main.transform;
        }

        initialPosition = transform.position;

    }

    void Update()
    {
        // Sprite horizontal auf das Ziel ausrichten
        Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, target.position.z);
        transform.LookAt(targetPosition);

        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);

        // AddShakeEffect();
    }

    /*void AddShakeEffect()
    {
        // Berechnet die horizontale Verschiebung mit einer Sinuswelle
        float offset = Mathf.Sin(Time.time * shakeFrequency) * shakeAmplitude;

        // Verschiebt das Sprite basierend auf der Schwingung
        transform.position = new Vector3(initialPosition.x + offset, initialPosition.y, initialPosition.z);
    }*/
}
