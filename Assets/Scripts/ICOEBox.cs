using System;
using UnityEngine;

public class ICOEBox : MonoBehaviour
{
    public GameObject glas; // Das Glas der Box
    public GameObject glasscherben; // Die Glasscherben am Boden
    public GameObject[] itemsInBox; // Die Items in der Box
    public MonoBehaviour[] itemScripts; // Die Skripte der Items, die aktiviert werden sollen
    public string requiredItemName = "Hammer"; // Der Name des benötigten Objekts
    private bool isOpened = false; // Zustand, ob die Box geöffnet wurde

    private SoundScript sound;

    private void Start()
    {
        sound = SoundScript.Instance;
    }

    private void Update()
    {
        // Prüfe, ob der Spieler die Taste E drückt
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Überprüfen, ob der Spieler auf das Glas schaut
            if (Physics.Raycast(ray, out hit, 3.0f)) // Maximal 3 Meter Reichweite
            {
                if (hit.collider != null && hit.collider.gameObject == this.glas) // Glas getroffen?
                {
                    InteractWithBox();
                }
            }
        }
    }

    public void InteractWithBox()
    {
        if (isOpened)
        {
            Debug.Log("Die Box wurde bereits geöffnet.");
            return;
        }

        if (SpielerInventar.Instance.HatObjekt(requiredItemName)) // Prüfen, ob der Spieler den Hammer hat
        {
            Debug.Log("Hammer gefunden! Öffne die Box...");

            // Entferne den Hammer aus dem Inventar
            SpielerInventar.Instance.EntferneObjekt(requiredItemName);

            // Glas deaktivieren
            if (glas != null)
            {
                Debug.Log("Deaktiviere Glas...");
                glas.SetActive(false);
                sound.GlassSound();
            }

            // Glasscherben aktivieren
            if (glasscherben != null)
            {
                Debug.Log("Aktiviere Glasscherben...");
                glasscherben.SetActive(true);
            }

            // Item-Skripte und zugehörige BoxCollider aktivieren
            for (int i = 0; i < itemScripts.Length; i++)
            {
                var script = itemScripts[i];
                if (script != null)
                {
                    Debug.Log($"Aktiviere Skript: {script.GetType().Name}");
                    script.enabled = true;

                    // Falls das zugehörige GameObject einen BoxCollider hat, diesen aktivieren
                    var collider = script.gameObject.GetComponent<BoxCollider>();
                    if (collider != null)
                    {
                        Debug.Log("Aktiviere BoxCollider.");
                        collider.enabled = true;
                    }
                }
            }

            isOpened = true;
            Debug.Log("Die Box wurde erfolgreich geöffnet.");
        }
        else
        {
            Debug.Log("Du benötigst einen Hammer, um die Box zu öffnen.");
        }
    }
}