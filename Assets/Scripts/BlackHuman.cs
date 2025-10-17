using UnityEngine;

public class BlackHuman : MonoBehaviour
{
    private HumanTrigger trigger;
    public static BlackHuman Instance { get; private set; }

    private float targetRotationZ = 0f;
    private float rotationSpeed = 80f;
    private bool OneUse = true;
    private SoundScript sound;

    private void Awake()
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

    void Start()
    {
        trigger = HumanTrigger.Instance;
        sound = SoundScript.Instance;
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (trigger != null && trigger.IsInZone)
        {
            sound.HeartbeatSound();
            sound.MysterySound();
            trigger.deactivate();
            RotateToTarget();
        }
        
        
    }

    public void activate()
    {
        if (OneUse)
        {
            gameObject.SetActive(true);
            OneUse = false;
        }
    }

    public void deactivate()
    {
        gameObject.SetActive(false);
    }

    private void RotateToTarget()
    {
        Quaternion currentRotation = transform.rotation;
        
        Quaternion targetRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, targetRotationZ);
        
        transform.rotation = Quaternion.RotateTowards(currentRotation, targetRotation, rotationSpeed * Time.deltaTime);
        
        if (Quaternion.Angle(currentRotation, targetRotation) < 0.1f)
        {
            Debug.Log("Rotation abgeschlossen. Objekt wird deaktiviert.");
            deactivate(); // Deaktivieren, wenn Rotation fertig ist
        }
    }
}