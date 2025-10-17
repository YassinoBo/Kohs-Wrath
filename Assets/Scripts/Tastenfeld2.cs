using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tastenfeld2 : MonoBehaviour
{
    public static Tastenfeld2 Instance;
    
    public string[] code1 = { "2", "4", "5", "7" }; // Erster Code
    public string[] code2 = { "1", "0", "3", "5" }; // Zweiter Code
    
    private List<string> eingegebenerCode = new List<string>();
    public GameObject[] tasten;
    public GameObject bestaetigungsTaste;
    public GameObject rueckschrittTaste;
    public Material korrektMaterial;
    public Material falschMaterial;
    public GameObject lampe;
    public TextMeshPro display;
    public HouseToTutorial houseToTutorial;
    private SoundScript sound;
    private bool codeGeprueft = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        if (display != null) display.text = "";
        sound = SoundScript.Instance;
    }

    void Update()
    {
        if (codeGeprueft && eingegebenerCode.Count == code1.Length) return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject gedrueckteTaste = hit.collider.gameObject;
                if (eingegebenerCode.Count == code1.Length)
                {
                    if (gedrueckteTaste == rueckschrittTaste) EntferneLetzteZahl();
                    else if (gedrueckteTaste == bestaetigungsTaste) PruefeCode();
                    sound.KeyPadSound(gedrueckteTaste);
                    return;
                }
                for (int i = 0; i < tasten.Length; i++)
                {
                    if (gedrueckteTaste == tasten[i])
                    {
                        DrueckeTaste(i.ToString());
                        sound.KeyPadSound(gedrueckteTaste);
                        return;
                    }
                }
                if (gedrueckteTaste == rueckschrittTaste && eingegebenerCode.Count > 0) EntferneLetzteZahl();
            }
        }
    }

    void DrueckeTaste(string taste)
    {
        eingegebenerCode.Add(taste);
        AktualisiereDisplay();
    }

    void AktualisiereDisplay()
    {
        if (display != null) display.text = string.Join("   ", eingegebenerCode);
    }

    void EntferneLetzteZahl()
    {
        if (eingegebenerCode.Count > 0)
        {
            eingegebenerCode.RemoveAt(eingegebenerCode.Count - 1);
            AktualisiereDisplay();
        }
    }

    void PruefeCode()
    {
        if (VergleicheCode(eingegebenerCode, code1))
        {
            ErfolgreicherCode(true);
            houseToTutorial.canTriggerKoh = true;
        }
        else if (VergleicheCode(eingegebenerCode, code2))
        {
            ErfolgreicherCode(true);
            houseToTutorial.doorOpen = true;
            Debug.Log("Tür offen!");
        }
        else
        {
            ErfolgreicherCode(false);
        }
        eingegebenerCode.Clear();
        AktualisiereDisplay();
        codeGeprueft = false;
    }

    bool VergleicheCode(List<string> eingabe, string[] richtigerCode)
    {
        if (eingabe.Count != richtigerCode.Length) return false;
        for (int i = 0; i < richtigerCode.Length; i++)
        {
            if (eingabe[i] != richtigerCode[i]) return false;
        }
        return true;
    }

    void ErfolgreicherCode(bool korrekt)
    {
        lampe.GetComponent<Renderer>().material = korrekt ? korrektMaterial : falschMaterial;
        // Feedback durch die Lampe (grün oder rot)
        if (korrekt)
        {
            sound.KeyPadIncorrectSound(lampe);
            Debug.Log("Code korrekt!");
        }
        else
        {
            sound.KeyPadSound(lampe);
            Debug.Log("Code falsch!");
        }
    }
}
