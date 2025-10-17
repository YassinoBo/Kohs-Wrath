using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SanityTestScript : MonoBehaviour
{
    public float sanity = 2000f;
    public Image sanityNeedle;
    public float maxSanity = 2000f;
    private float minAngle = -70f;
    private float maxAngle = 70f;

    public float decayRate = 2000f;
    public Transform playerTransform;                                   // Referenz auf das Spielerobjekt
    public Vector3 respawnPosition = new Vector3(-0.43f, 1.3f, -0.03f); // Respawn-Koordinaten
    
    public GameObject endScreenCanvas;
    
    private SoundScript soundScript;
    private bool oneUse = true;

    void Start()
    {
        SetSanity();
        decayRate = 2000f / (30f * 60f);
        soundScript = SoundScript.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (sanity > 0)
        {
            sanity -= decayRate * Time.deltaTime;
            sanity = Mathf.Max(sanity, 0);
            SetSanity();
        }else if (sanity == 0)
        {
            PlayerDeath();
            if (oneUse)
            {
                oneUse = false;
                soundScript.StopBackgroundSound();
                soundScript.HummingSound();
            }
        }
        else                                                            //hier neu angefügt
        {
           PlayerDeath();
           if (oneUse)
           {
               oneUse = false;
               soundScript.StopBackgroundSound();
               soundScript.HummingSound();
           }
        }
    }

    /*
     void FixedUpdate()
     {
    if (sanity > 0)
    {
        sanity -= decayRate * Time.fixedDeltaTime;
        sanity = Mathf.Max(sanity, 0);
    }
    SetSanity();
      }

     */

    void SetSanity()
    {
        //float angle = Mathf.Lerp(minAngle, maxAngle, sanity / maxSanity);
        float angle = (sanity / maxSanity) * 140 - 70;
        sanityNeedle.transform.rotation = Quaternion.Euler(0, 0, angle);
    }



    /*
     void SetSanity()
       {
    float targetAngle = (sanity / maxSanity) * 140 - 70;
    float currentAngle = sanityNeedle.transform.rotation.eulerAngles.z;
    currentAngle = FixAngle(currentAngle); // Eine Hilfsfunktion, um den Winkel anzupassen, falls notwendig
    float smoothedAngle = Mathf.LerpAngle(currentAngle, targetAngle, Time.deltaTime * 5);
    sanityNeedle.transform.rotation = Quaternion.Euler(0, 0, smoothedAngle);
         }

      float FixAngle(float angle)
     {
    while (angle > 180) angle -= 360;
    while (angle < -180) angle += 360;
    return angle;
     }

     */

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            sanity -= 5f;
            if (sanity < 0) sanity = 0; // Sicherheit, dass die Sanity nicht negativ wird
        }
    }

    
    void PlayerDeath()                                                            //hier neu angefügt
    {
        if (endScreenCanvas != null)
        {
            endScreenCanvas.SetActive(true);
            Time.timeScale = 0f; // Spiel pausie
            SetSanity(); // Anzeige aktualisieren
        }
        
    }
    
}