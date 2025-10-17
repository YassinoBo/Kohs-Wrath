using System.Collections.Generic;
using UnityEngine;
using TMPro; // Namespace für TextMeshPro

public class TastaturInteraktion : MonoBehaviour
{
    public string[] richtigerCode = { "2", "4", "5", "7" }; // Der richtige Code
    private List<string> eingegebenerCode = new List<string>(); // Die vom Spieler eingegebene Zahlenfolge
    public GameObject[] tasten; // Referenzen zu den Tasten GameObjects
    public GameObject bestaetigungsTaste; // Der Button, der zur Bestätigung des Codes gedrückt werden muss
    public GameObject rueckschrittTaste; // Der Button, der zum Entfernen der letzten Zahl gedrückt wird
    public Material korrektMaterial; // Material für die grüne Lampe (korrekt)
    public Material falschMaterial;  // Material für die rote Lampe (falsch)
    public GameObject lampe; // Die Lampe (grün oder rot)
    public TextMeshPro display; // TextMeshPro-Objekt zur Anzeige des eingegebenen Codes

    private bool codeGeprueft = false; // Ob der Code bereits geprüft wurde
    private SoundScript sound;
    public HouseToTutorial houseToTutorial;

    void Start()
    {
        // Initialisiere das Display
        if (display != null)
        {
            display.text = ""; // Leerer Text zu Beginn
        }
        else
        {
            Debug.LogWarning("Display ist nicht zugewiesen!");
        }
        
        sound = SoundScript.Instance;
    }

    void Update()
    {
        // Wenn der Code bereits geprüft wurde, keine weiteren Eingaben mehr
        if (codeGeprueft && eingegebenerCode.Count == richtigerCode.Length) return;

        // Raycast überprüfen, um festzustellen, ob eine Taste mit dem Linksklick gedrückt wurde
        if (Input.GetMouseButtonDown(0)) // Linksklick (0) wird hier verwendet
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Ray vom Mauszeiger
            RaycastHit hit;

            // Überprüfen, ob der Ray eine der Tasten trifft
            if (Physics.Raycast(ray, out hit))
            {
                GameObject gedrueckteTaste = hit.collider.gameObject;

                // Eingabe blockieren, wenn bereits 4 Zahlen eingegeben wurden
                if (eingegebenerCode.Count == richtigerCode.Length)
                {
                    if (gedrueckteTaste == rueckschrittTaste)
                    {
                        EntferneLetzteZahl();
                    }
                    else if (gedrueckteTaste == bestaetigungsTaste)
                    {
                        PruefeCode();
                    }
                    sound.KeyPadSound(gedrueckteTaste);
                    return;
                }

                // Finde die Taste im Array der Tasten
                for (int i = 0; i < tasten.Length; i++)
                {
                    if (gedrueckteTaste == tasten[i])
                    {
                        DrueckeTaste(i.ToString());
                        sound.KeyPadSound(gedrueckteTaste);
                        return;
                    }
                }

                // Überprüfen, ob die Rückschritt-Taste gedrückt wurde
                if (gedrueckteTaste == rueckschrittTaste && eingegebenerCode.Count > 0)
                {
                    EntferneLetzteZahl();
                    sound.KeyPadSound(gedrueckteTaste);
                }
            }
        }
    }

    void DrueckeTaste(string taste)
    {
        // Code zur eingegebenen Liste hinzufügen
        eingegebenerCode.Add(taste);
        Debug.Log($"Taste {taste} gedrückt. Aktueller Code: {string.Join("", eingegebenerCode)}");

        // Display aktualisieren
        AktualisiereDisplay();
    }

    void AktualisiereDisplay()
    {
        if (display != null)
        {
            // Setze den Text des Displays auf die aktuelle Eingabe
            display.text = string.Join("   ", eingegebenerCode);
        }
        else
        {
            Debug.LogWarning("Display ist nicht zugewiesen!");
        }
    }

    void EntferneLetzteZahl()
    {
        if (eingegebenerCode.Count > 0)
        {
            // Entferne die letzte Zahl
            eingegebenerCode.RemoveAt(eingegebenerCode.Count - 1);
            Debug.Log("Letzte Zahl entfernt. Aktueller Code: " + string.Join("", eingegebenerCode));

            // Display aktualisieren
            AktualisiereDisplay();
        }
    }

    void PruefeCode()
    {
        // Überprüfe den eingegebenen Code mit dem richtigen Code
        bool korrekt = true;
        for (int i = 0; i < richtigerCode.Length; i++)
        {
            if (eingegebenerCode.Count <= i || eingegebenerCode[i] != richtigerCode[i])
            {
                korrekt = false;
                break;
            }
        }

        // Feedback durch die Lampe (grün oder rot)
        if (korrekt)
        {
            lampe.GetComponent<Renderer>().material = korrektMaterial;
            sound.KeyPadIncorrectSound(lampe);
            houseToTutorial.canTriggerKoh = true;
            Debug.Log("Code korrekt!");
        }
        else
        {
            lampe.GetComponent<Renderer>().material = falschMaterial;
            sound.KeyPadSound(lampe);
            Debug.Log("Code falsch!");
        }

        // Zahlen zurücksetzen
        eingegebenerCode.Clear();
        AktualisiereDisplay();
        codeGeprueft = false; // Eingabe wird erneut zugelassen
    }
}
