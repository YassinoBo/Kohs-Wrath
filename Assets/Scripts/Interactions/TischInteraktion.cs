using UnityEngine;

public class TischInteraktion : MonoBehaviour
{
    [System.Serializable]
    public class TischItem
    {
        public GameObject itemAufTisch; // Das Objekt, das auf dem Tisch erscheint
        public string itemName;        // Der Name des Items im Inventar
    }

    public TischItem[] itemsAufTisch = new TischItem[5]; // Array für bis zu 5 Items
    public float interaktionsEntfernung = 3f; // Entfernung, in der interagiert werden kann

    private bool inInteraktionsbereich = false; // Ob der Spieler nahe genug ist

    void Update()
    {
        // Überprüfen, ob der Spieler E drückt und sich in Reichweite befindet
        if (inInteraktionsbereich && Input.GetMouseButton(0))
        {
            Interagiere();
        }
    }

    void Interagiere()
    {
        foreach (TischItem tischItem in itemsAufTisch)
        {
            if (tischItem.itemAufTisch != null && !tischItem.itemAufTisch.activeSelf)
            {
                // Überprüfen, ob der Spieler das Item im Inventar hat
                if (SpielerInventar.Instance.HatObjekt(tischItem.itemName))
                {
                    tischItem.itemAufTisch.SetActive(true); // Das Objekt auf dem Tisch sichtbar machen
                    SpielerInventar.Instance.EntferneObjekt(tischItem.itemName); // Item aus dem Inventar entfernen
                    Debug.Log("Item " + tischItem.itemName + " wurde auf den Tisch gelegt!");
                }
                else
                {
                    Debug.Log("Das benötigte Item " + tischItem.itemName + " ist nicht im Inventar!");
                }
            }
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Spieler muss das Tag "MainCamera" haben
        {
            inInteraktionsbereich = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inInteraktionsbereich = false;
        }
    }

    void OnDrawGizmos()
    {
        // Optional: Zeige die Interaktionsreichweite im Editor als Gizmo
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interaktionsEntfernung);
    }
}
