using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpielerInventar : MonoBehaviour
{
    public static SpielerInventar Instance; // Singleton-Instanz

    private HashSet<string> objektSet = new HashSet<string>(); // Gesammelte Objekte
    public Image[] itemSlots;  // Die Slots für die Items (maximal 3 Slots für 3 Objekte)
    public Sprite leeresSlotSprite;  // Sprite für leere Slots
    public string[] itemNames = new string[5];  // Array für die Namen der Items (maximal 3 Items)
    private Sprite[] itemSprites = new Sprite[5];  // Array für die Sprites der Items

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Fügt ein Objekt hinzu
    public void FuegeObjektHinzu(string objektName, Sprite keySprite)
    {
        // Wenn das Inventar noch Platz hat
        for (int i = 0; i < itemNames.Length; i++)
        {
            if (string.IsNullOrEmpty(itemNames[i]))  // Finde das erste leere Slot
            {
                itemNames[i] = objektName;  // Schlüsselname im Array speichern
                itemSprites[i] = keySprite;  // Das Sprite für das Objekt im Array speichern
                itemSlots[i].sprite = keySprite;  // Slot mit dem Objekt-Sprite füllen
                itemSlots[i].enabled = true;  // Slot sichtbar machen
                objektSet.Add(objektName);  // Objekt im Set speichern
                break;
            }
        }
    }

    // Entfernt ein Objekt und setzt das leere Slot-Sprite
    public void EntferneObjekt(string objektName)
    {
        for (int i = 0; i < itemNames.Length; i++)
        {
            if (itemNames[i] == objektName)
            {
                itemNames[i] = null;
                itemSprites[i] = null;
                itemSlots[i].sprite = leeresSlotSprite;  // Slot wieder leeren
                itemSlots[i].enabled = true;  // Slot sichtbar lassen
                if (objektName != "Springer")
                {
                    objektSet.Remove(objektName);  // Entferne aus dem Set
                }
                break;
            }
        }
    }

    // Überprüfen, ob das Inventar voll ist
    public bool IstVoll()
    {
        foreach (var item in itemNames)
        {
            if (string.IsNullOrEmpty(item))
            {
                return false;  // Es gibt noch Platz
            }
        }
        return true;  // Kein Platz mehr
    }

    // Überprüft, ob der Spieler das Objekt besitzt
    public bool HatObjekt(string objektName)
    {
        return objektSet.Contains(objektName);
    }
    
    public void SetzeInventarSichtbarkeit(bool sichtbar)
    {
        foreach (var slot in itemSlots)
        {
            slot.enabled = sichtbar; // Sichtbarkeit jedes Slots setzen
        }
    }

}
