using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Schalter : MonoBehaviour
{
    private bool isOn = false; // Status des Schalters (ein/aus)
    public float toggleTranslation = 0.96f; // Die Translationseinheit, um die der Schalter bewegt werden soll

    private Vector3 offPosition; // Ursprüngliche Position
    private Vector3 onPosition; // Zielposition

    public Schalter[] obereHebel; // Referenz auf die oberen Hebel
    public bool[] untereHebelKombination; // Kombination der unteren Hebel
    public Schalter[] untereHebel; // Referenz auf die unteren Hebel

    private bool isAktivierbar = false; // Flag, ob der Hebel aktiviert werden kann
    private bool isCorrectCombination = false; // Flag, ob die richtige Kombination eingegeben wurde

    private bool[] kombiHebel1 = { true, false, true, false, true };
    private bool[] kombiHebel2 = { true, false, false, true, false };
    private bool[] kombiHebel3 = { false, true, true, false, true };

    public Lampensteuerung lampeSteuerung; // Referenz auf das Lampensteuerungs-Skript

    private bool hasWon2 = false; // Variable für Sieg des Spiels
    private bool hasWonMessageShown = false;
    private bool hasWrongCombinationMessageShown = false;
    private RedHuman redHuman;

    // Referenz auf das SanityLossScript
    public SanityLossScript sanityLossScript;
    
    public Spind zugehoerigerSpind; // Der Spind, der durch diesen Hebel geöffnet wird


    void Start()
    {
        offPosition = transform.localPosition; // Ausgangsposition speichern
        onPosition = offPosition + new Vector3(0, toggleTranslation, 0); // Zielposition berechnen

        if (lampeSteuerung != null)
        {
            lampeSteuerung.SetLampeRot(); // Lampe auf rot setzen
        }

        /*if (sanityLossScript == null)
        {
            Debug.Log("SanityLossScript ist nicht zugewiesen!");
        }*/
        
        redHuman = RedHuman.Instance;
    }

    void Update()
    {
        if (!isCorrectCombination)
        {
            isAktivierbar = PruefeUntereHebelKombination(); // Überprüfen, ob die unteren Hebel in der richtigen Kombination sind
        }

        if (lampeSteuerung != null)
        {
            if (isOn && isAktivierbar)
            {
                lampeSteuerung.SetLampeGruen();
                isCorrectCombination = true;
                hasWon2 = true;

                if (gameObject.CompareTag("Hebel oben 1"))
                {
                    redHuman.setVisible(false);
                }

                if (!hasWonMessageShown)
                {
                    Debug.Log("Strom für Hebel " + gameObject.name + " fließt! Leuchtende Lampe wird eingeschaltet.");
                    hasWonMessageShown = true;
                }

                // Hebel bleibt oben und ist nicht mehr interagierbar
                GetComponent<Collider>().enabled = false;

                // Öffne den zugehörigen Spind, wenn die Kombination korrekt ist
                if (zugehoerigerSpind != null)
                {
                    zugehoerigerSpind.OeffneTuere();
                }
            }
            else if (isOn && !isAktivierbar)
            {
                lampeSteuerung.SetLampeRot();
                    
                if (!hasWrongCombinationMessageShown)
                {
                    Debug.Log("Falsche Kombination! Alternative Lampe wird eingeschaltet.");
                    hasWrongCombinationMessageShown = true;
                }
                
                if (Input.GetMouseButtonDown(0))
                {
                    // Sanity-Verlust bei falscher Kombination auslösen
                    if (sanityLossScript != null)
                    {
                        sanityLossScript.LoseSanity();
                    }
                    else
                    {
                        Debug.Log("SanityLossScript ist nicht gesetzt!");
                    }
                }

                StartCoroutine(ResetHebelNachVerzoegerung());
            }
        }
    }

    void OnMouseDown()
    {
        if (!isCorrectCombination) // Interaktion nur, wenn die Kombination noch nicht korrekt ist
        {
            isOn = !isOn;

            if (isOn)
            {
                transform.localPosition = onPosition;
                SoundScript.Instance.ButtonSound(gameObject);
            }
            else
            {
                transform.localPosition = offPosition;
                SoundScript.Instance.ButtonSound(gameObject);
            }
        }
    }

    bool PruefeUntereHebelKombination()
    {
        if (gameObject.CompareTag("Hebel oben 1"))
        {
            return CheckCombination(kombiHebel1);
        }
        else if (gameObject.CompareTag("Hebel oben 2"))
        {
            return CheckCombination(kombiHebel2);
        }
        else if (gameObject.CompareTag("Hebel oben 3"))
        {
            return CheckCombination(kombiHebel3);
        }
        return false;
    }

    bool CheckCombination(bool[] kombin)
    {
        for (int i = 0; i < kombin.Length; i++)
        {
            if (untereHebel[i].isOn != kombin[i])
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator ResetHebelNachVerzoegerung()
    {
        yield return new WaitForSeconds(0.7f);

        foreach (var hebel in untereHebel)
        {
            hebel.isOn = false;
            hebel.transform.localPosition = hebel.offPosition;
        }

        isOn = false;
        transform.localPosition = offPosition;
        isCorrectCombination = false;
    }
}
