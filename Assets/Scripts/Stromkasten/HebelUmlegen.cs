using UnityEngine;

public class HebelUmlegen : MonoBehaviour
{
    // Referenzen zu den Lampensteuerungen
    public Lampensteuerung lampeSteuerung1;
    public Lampensteuerung lampeSteuerung2;
    public Lampensteuerung lampeSteuerung3;

    // Referenz zum Hebel (Bone.001)
    public Transform hebel;

    // Drehwinkel des Hebels
    public float drehwinkel = -18.519f;

    // Flag, um zu überprüfen, ob alle Lampen grün sind
    internal bool sindAlleLampenGruen = false;

    // Flag, um zu überprüfen, ob der Hebel schon umgelegt wurde
    public bool isHebelUmgelegt = false;

    void Update()
    {
        // Überprüfe, ob alle Lampen grün sind
        sindAlleLampenGruen = CheckIfAllLampsAreGreen();

        // Debugging-Log: Überprüfen, ob alle Lampen grün sind
        //Debug.Log("Alle Lampen grün? " + sindAlleLampenGruen);
    }

    void OnMouseDown()
    {
        // Spieler kann den Hebel nur umlegen, wenn alle Lampen grün sind und der Hebel noch nicht umgelegt wurde
        if (sindAlleLampenGruen && !isHebelUmgelegt)
        {
            // Hebel umlegen
            RotateHebel();
            SoundScript.Instance.LeverSound(gameObject);
        }
    }

    // Überprüfe, ob alle Lampen grün sind
    bool CheckIfAllLampsAreGreen()
    {
        // Überprüfe den Zustand jeder Lampe
        return lampeSteuerung1.isGreen && lampeSteuerung2.isGreen && lampeSteuerung3.isGreen;
    }

    // Hebel umlegen
    void RotateHebel()
    {
        if (!isHebelUmgelegt) // Sicherstellen, dass der Hebel nur einmal umgelegt wird
        {
            // Drehung des Hebels um den angegebenen Winkel
            hebel.Rotate(Vector3.right, drehwinkel);

            // Setze das Flag, dass der Hebel umgelegt wurde
            isHebelUmgelegt = true;

            // Deaktivieren Sie alle weiteren Interaktionen mit dem Hebel
            GetComponent<Collider>().enabled = false;

            // Debugging-Log: Bestätigung, dass der Hebel umgelegt wurde
            Debug.Log("Hebel umgelegt!");
        }
    }
}