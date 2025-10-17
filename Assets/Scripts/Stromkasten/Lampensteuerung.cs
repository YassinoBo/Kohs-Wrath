using UnityEngine;

public class Lampensteuerung : MonoBehaviour
{
    // Referenz auf die Lampe (Renderer), um das Material zu ändern
    public Renderer lampeRenderer;

    // Materialien für die Lampe (grün und rot)
    public Material gruenMaterial;
    public Material rotMaterial;

    // Bool-Wert, der den Zustand der Lampe verfolgt
    public bool isGreen = false;

    // Setze die Lampe auf grün (richtige Kombination)
    public void SetLampeGruen()
    {
        if (lampeRenderer != null)
        {
            lampeRenderer.material = gruenMaterial;
            isGreen = true; // Zustand auf grün setzen
        }
    }

    // Setze die Lampe auf rot (falsche Kombination)
    public void SetLampeRot()
    {
        if (lampeRenderer != null)
        {
            lampeRenderer.material = rotMaterial;
            isGreen = false; // Zustand auf rot setzen
        }
    }

    // Setze die Lampe auf rot beim Start
    public void SetLampeRotOnStart()
    {
        if (lampeRenderer != null)
        {
            lampeRenderer.material = rotMaterial;
            isGreen = false; // Zustand auf rot setzen
        }
    }
}