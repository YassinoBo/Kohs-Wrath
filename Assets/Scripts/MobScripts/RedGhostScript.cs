using System.Collections;
using UnityEngine;

public class RedGhostScript : MonoBehaviour
{
    public Transform player;
    public float scareDistance = 6.0f;
    
    private bool BasementEvent = false;
    
    public bool HeadEvent = false;
    public GameObject Head;
    private PushDown pushDown;

    private LampScript lamp;
    private SoundScript sound;
    private GhostScript ghost;
    
    public static RedGhostScript Instance {get; private set;}
    // Start is called once before the first execution of Update after the MonoBehaviour is created

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
        if (player == null && GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        if (Head == null && GameObject.FindGameObjectWithTag("Head") != null)
        {
            Head = GameObject.FindGameObjectWithTag("Head");
        }
        pushDown = Head.GetComponent<PushDown>();
        if (pushDown == null)
        {
            Debug.LogError("PushDown component not found");
        }
        lamp = LampScript.Instance;
        sound = SoundScript.Instance;
        ghost = GhostScript.Instance;
        
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (BasementEvent && Vector3.Distance(player.position, transform.position) <= scareDistance)
        {
            BasementEvent = false;
            lamp.setStatus(false);
            sound.HeartbeatSound();
            gameObject.transform.position = new Vector3(22.5310001f,9.11499977f,-8.92099953f);
            sound.OilLampOffSound();
            sound.RedGhostVoiceSound();
            HeadEvent = false;
            ghost.SetAllowAttack(true);
            gameObject.SetActive(false);
        }

        pushDown.activate();
    }
    

    public void setBasementEvent(bool value)
    {
        BasementEvent = value;
    }

    public void activate()
    {
        gameObject.SetActive(true);
        HeadEvent = true;

    }

    public bool getBasementEvent()
    {
        return BasementEvent;
    }
}
