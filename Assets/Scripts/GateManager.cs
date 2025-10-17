using UnityEngine;

public class GateManager : MonoBehaviour
{
    public GameObject leftFence; // Der linke Zaun
    public GameObject rightFence; // Der rechte Zaun
    public float rotationAngle = 45f; // Drehwinkel

    void Start()
    {
        // Überprüfen, ob der Hebelstatus in Szene A gespeichert wurde
        if (PlayerPrefs.GetInt("HebelUmgelegt", 0) == 1) // 1 = umgelegt
        {
            OpenGate(); // Tor öffnen
        }
    }

    private void OpenGate()
    {
        // Linken Zaun zurückdrehen
        if (leftFence != null)
        {
            leftFence.transform.Rotate(0, 0, -rotationAngle);
        }

        // Rechten Zaun zurückdrehen
        if (rightFence != null)
        {
            rightFence.transform.Rotate(0, 0, rotationAngle);
        }

        Debug.Log("Das Tor wurde geöffnet, da der Hebel in Szene A umgelegt wurde.");
    }
}
