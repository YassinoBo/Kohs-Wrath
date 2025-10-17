using UnityEngine;

public class ToggleCapsuleVisibility : MonoBehaviour
{
    public GameObject capsule; // Das Objekt, das du ein- oder ausblenden möchtest

    void Start()
    {
        // Beispiel: Die Kapsel gleich beim Start ausblenden
        SetCapsuleVisibility(false);
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Wenn die Leertaste gedrückt wird
        {
            // Umschalten der Sichtbarkeit
            bool currentVisibility = capsule.GetComponent<MeshRenderer>().enabled;
            SetCapsuleVisibility(!currentVisibility); // Sichtbarkeit umkehren
        }
    }


    public void SetCapsuleVisibility(bool isVisible)
    {
        // Prüft, ob die Kapsel ein MeshRenderer-Komponente hat und setzt deren Sichtbarkeit
        if (capsule != null)
        {
            MeshRenderer meshRenderer = capsule.GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                meshRenderer.enabled = isVisible; // True = sichtbar, False = unsichtbar
            }
        }
    }
}