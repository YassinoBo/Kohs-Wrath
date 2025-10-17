using UnityEngine;

public class DeaktivierbaresObjekt : MonoBehaviour
{
    public Spind zugehoerigerSpind; // Referenz auf den zugehörigen Spind
    private Collider objCollider;  // Collider des Objekts
    private MonoBehaviour objektSkript; // Optional: das Skript, das deaktiviert werden soll

    void Start()
    {
        if (zugehoerigerSpind == null)
        {
            Debug.LogError("Kein zugehöriger Spind zugewiesen!", this);
            return;
        }

        // Komponenten holen
        objCollider = GetComponent<Collider>();
        if (objCollider == null)
        {
            Debug.LogWarning("Kein Collider gefunden, das Objekt benötigt keinen Collider.", this);
        }

        objektSkript = GetComponent<MonoBehaviour>();
        if (objektSkript == null)
        {
            Debug.LogWarning("Kein zusätzliches Skript gefunden, das deaktiviert werden könnte.", this);
        }
    }

    void Update()
    {
        // Überprüfen, ob die Tür des zugehörigen Spinds offen ist
        if (zugehoerigerSpind.isOpen)
        {
            // Collider und Skript aktivieren
            if (objCollider != null) objCollider.enabled = true;
            if (objektSkript != null) objektSkript.enabled = true;
        }
        else
        {
            // Collider und Skript deaktivieren
            if (objCollider != null) objCollider.enabled = false;
            if (objektSkript != null) objektSkript.enabled = false;
        }
    }
}
