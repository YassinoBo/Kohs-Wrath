using System;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public Transform chestDoor; // Der Deckel der Truhe
    public Transform lockObject; // Das Schloss der Truhe
    public Transform joint1; // Das Joint_1-Objekt, das rotiert werden soll

    public Vector3 lockGroundPosition = new Vector3(-0.04910151f, -0.4242f, 0.36f); // Position, wo das Schloss auf dem Boden liegen soll
    public Vector3 lockGroundRotation = new Vector3(-90f, 0f, -90f); // Rotation des Schlosses auf dem Boden

    public Vector3 openDoorPosition = new Vector3(0.0209109f, 0.5364f, -0.496f); // Zielposition des Deckels
    public Vector3 openDoorRotation = new Vector3(-90f, 0f, 0f); // Zielrotation des Deckels

    public Vector3 openJoint = new Vector3(-1.421085e-16f, -0.0233f, -0.05106705f); // Zielposition des unteren Schlosses

    public GameObject objectInChest; // Das deaktivierte Objekt in der Truhe, das aktiviert werden soll
    public TischInteraktion tischInteraktion; // Referenz zum TischInteraktion-Skript
    private bool isOpened = false; // Zustand, ob die Truhe bereits geöffnet wurde

    private SoundScript soundScript;

    private void Start()
    {
        soundScript = SoundScript.Instance;
    }

    void Update()
    {
        // Überprüfen, ob alle Items auf dem Tisch sind und die Truhe noch nicht geöffnet wurde
        if (!isOpened && AlleItemsAufTisch())
        {
            ÖffneTruhe();
        }
    }

    private bool AlleItemsAufTisch()
    {
        // Gehe alle Items im TischInteraktion-Skript durch
        foreach (var item in tischInteraktion.itemsAufTisch)
        {
            // Wenn ein Item nicht aktiviert ist, sind nicht alle Teile auf dem Tisch
            if (item.itemAufTisch == null || !item.itemAufTisch.activeSelf)
            {
                return false;
            }
        }

        // Alle Teile sind auf dem Tisch
        return true;
    }

    private void ÖffneTruhe()
    {
        if (chestDoor != null)
        {
            // Setze die Position und Rotation des Deckels
            chestDoor.position = openDoorPosition;
            chestDoor.rotation = Quaternion.Euler(openDoorRotation);
            soundScript.ChestSound();
            Debug.Log("Die Truhe wurde geöffnet!");
        }
        else
        {
            Debug.LogWarning("Kein Deckel für die Truhe zugewiesen!");
        }

        // Behandle das Schloss
        if (lockObject != null)
        {
            // Bewege das Schloss auf den Boden
            lockObject.position = lockGroundPosition;
            lockObject.rotation = Quaternion.Euler(lockGroundRotation);
            Debug.Log("Das Schloss wurde auf den Boden gelegt!");

            // Rotiere Joint_1, falls vorhanden
            if (joint1 != null)
            {
                joint1.Rotate(0f, 0f, 180f); // Rotiert um 180° in Z-Richtung
                joint1.position = openJoint;
                Debug.Log("Joint_1 wurde um 180° in Z-Richtung rotiert.");
            }
            else
            {
                Debug.LogWarning("Joint_1 ist nicht zugewiesen!");
            }
        }
        else
        {
            Debug.LogWarning("Kein Schloss zugewiesen!");
        }

        // Aktiviere das Objekt in der Truhe
        if (objectInChest != null)
        {
            objectInChest.SetActive(true);
            Debug.Log("Das Objekt in der Truhe wurde aktiviert!");
        }
        else
        {
            Debug.LogWarning("Kein Objekt in der Truhe zugewiesen!");
        }

        isOpened = true;
    }
}
