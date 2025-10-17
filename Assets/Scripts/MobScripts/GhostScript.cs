using UnityEngine;
using UnityEngine.AI;

public class GhostScript : MonoBehaviour
{
    public Transform player;
    public float attackRange = 2f;
    public float moveSpeed = 3.5f;
    public int damage = 10;
    public float ghostSanityValue = 20f;
    public float initialAppearanceDelay = 40f;

    private NavMeshAgent agent;
    private SoundScript sound;
    private LampScript lamp;
    private Vector3 pos;
    
    public float minAppearanceDelay = 30f;
    public float maxAppearanceDelay = 40f;

    private bool allowAttack = true;
    private bool isWaitingToAppear = false;

    private SanityLossScript sanityLoss;
    
    public SanityTestScript sanityTest;
    
    public static GhostScript Instance {get; private set;}

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

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.speed = moveSpeed;
        }

        sound = SoundScript.Instance;
        lamp = LampScript.Instance;

        sanityLoss = GetComponent<SanityLossScript>();
        sanityLoss.sanityLossValue = ghostSanityValue;

        // Geist zu Beginn deaktivieren
        gameObject.SetActive(false);

        // Timer starten, um den Geist nach initialAppearanceDelay zu aktivieren
        ScheduleNextAppearance();
    }

    private void Update()
    {
        
        if (player == null || !gameObject.activeSelf) return;

        float distance = Vector3.Distance(transform.position, player.position);

        // Den Geist zum Spieler ausrichten
        LookAtPlayer();

        if (distance > attackRange)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
        else
        {
            AttackPlayer();
        }
    }

    private void LookAtPlayer()
    {
        Vector3 lookDirection = player.position - transform.position;
        lookDirection.y = 0;
        if (lookDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(lookDirection);
        }
        transform.rotation = Quaternion.Euler(90f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }

    private void AttackPlayer()
    {
        // Berechne die Richtung, in die die Kamera schaut
        Vector3 cameraForward = Camera.main.transform.forward;

        // Setze die Y-Komponente auf 0, damit der Geist nur auf der X/Z-Ebene verschoben wird
        cameraForward.y = 0;

        // Normalisiere die Richtung, um eine konstante Bewegungsgeschwindigkeit zu gewährleisten
        Vector3 direction = cameraForward.normalized;

        // Bewege den Geist 30 Einheiten in der Richtung, in die die Kamera schaut
        transform.position += direction * 30f;

        // Deaktiviere den Geist nach der Bewegung
        gameObject.SetActive(false);

        // Spiele Sounds ab und führe weitere Aktionen aus
        sound.Ghost2Sound();
        sanityLoss.LoseSanity();
        sound.HeartbeatSound();
        lamp.ToggleLight();

        // Timer starten, um den Geist später wieder zu aktivieren
        ScheduleNextAppearance();
    }


    private void CheckAndAppear()
    {
        if (sanityTest.sanity <= 800)
        {
            if (allowAttack)
            {
                Appear();
            }
            else
            {
                isWaitingToAppear = true;
            }
        }
        else
        {
            ScheduleNextAppearance();
        }
    }


    private void Appear()
    {
        Debug.Log("Der Geist erscheint!");
        isWaitingToAppear = false;
        gameObject.SetActive(true);
    }

    public void SetAllowAttack(bool value)
    {
        allowAttack = value;
        
        if (allowAttack && isWaitingToAppear)
        {
            Appear();
        }
    }
    
    private void ScheduleNextAppearance()
    {
        float randomDelay = Random.Range(minAppearanceDelay, maxAppearanceDelay);
        Invoke(nameof(CheckAndAppear), randomDelay);
    }
}
