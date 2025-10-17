using UnityEngine;

public class ItemActivator : MonoBehaviour
{
    [Header("Item Activation Settings")]
    public string requiredItemName = "Lockpick"; // Name des Items, das im Inventar sein muss
    public string itemToActivateName = "Left Arm"; // Name des Items, das die Entfernung triggert
    public MonoBehaviour scriptToActivate; // Das zu aktivierende Skript auf dem Objekt
    public GameObject box; // Das Objekt, das die Box enthält
    public Transform klappeTransform; // Der Transform der Klappe (hier wird Position und Rotation geändert)
    public float interaktionsRadius = 2f; // Der Radius, in dem der Spieler mit der Klappe interagieren kann

    private SpielerInventar spielerInventar;
    private bool klappeGeöffnet = false; // Boolesche Variable, die sicherstellt, dass die Klappe nur einmal geöffnet wird
    private bool kannInteragieren = false; // Flag, das festlegt, ob der Spieler mit der Klappe interagieren kann

    void Start()
    {
        // Sicherstellen, dass das Skript zu Beginn deaktiviert ist
        if (scriptToActivate != null)
        {
            scriptToActivate.enabled = false;
        }

        // Referenz auf SpielerInventar holen
        spielerInventar = SpielerInventar.Instance;

        // Falls klappeTransform noch nicht manuell zugewiesen wurde, versuche es programmgesteuert zu finden
        if (klappeTransform == null && box != null)
        {
            klappeTransform = box.transform.Find("Klappe"); // Stelle sicher, dass der Name korrekt ist
        }

        if (klappeTransform == null)
        {
            Debug.LogError("Klappe nicht gefunden! Überprüfe, ob das Transform der Klappe korrekt zugewiesen ist.");
        }
    }

    void Update()
    {
        // Überprüfen, ob der Spieler das Lockpick hat
        if (spielerInventar.HatObjekt(requiredItemName))
        {
            kannInteragieren = true; // Spieler kann mit der Klappe interagieren, wenn er das Lockpick hat
        }
        else
        {
            kannInteragieren = false; // Wenn der Spieler das Lockpick nicht hat, kann er nicht interagieren
        }

        // Überprüfen, ob der Spieler in der Nähe der Klappe ist und die "E"-Taste drückt
        if (kannInteragieren && !klappeGeöffnet && Vector3.Distance(transform.position, klappeTransform.position) < interaktionsRadius)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                ÖffneKlappe();
            }
        }

        // Falls die Klappe geöffnet wurde, überprüfe, ob der Arm aufgehoben werden kann
        if (klappeGeöffnet)
        {
            CheckForItemUsage();
        }
    }

    void ÖffneKlappe()
    {
        // Position und Rotation der Klappe ändern
        klappeTransform.position = new Vector3(0f,0.0491999984f,-10.2186003f); //schreib diff hin zwischen hier und in Unity
        klappeTransform.rotation = Quaternion.Euler(-180, 0, 0);
        Debug.Log("Die Klappe wurde geöffnet.");

        // Setze die Klappe auf geöffnet
        klappeGeöffnet = true;
    }

    void CheckForItemUsage()
    {
        if (spielerInventar == null) return;

        // Überprüfen, ob das Ziel-Item (z. B. "Left Arm") im Inventar ist
        if (spielerInventar.HatObjekt(itemToActivateName))
        {
            // Entferne das benötigte Item (z. B. "Lockpick") aus dem Inventar
            if (spielerInventar.HatObjekt(requiredItemName))
            {
                // Entfernen des Items 'Lockpick' aus dem Inventar
                spielerInventar.EntferneObjekt(requiredItemName);
                Debug.Log($"Das Item '{requiredItemName}' wurde entfernt, nachdem '{itemToActivateName}' aufgenommen wurde.");
            }
        }
    }
}
