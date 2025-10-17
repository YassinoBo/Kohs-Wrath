using Unity.VisualScripting;
using UnityEngine;

public class DefenceAnimation : MonoBehaviour
{
    public Light DefenceLight;
    
    private Animator animator;
    private LampScript lampScript;
    private bool defend = false;
    private float slowness;
    private float speed;

    private float defenceTimer = 0f;
    public float MaxDefenceTimer = 4f;
    
    private MovePlayer movePlayer;
    
    public static DefenceAnimation Instance { get; private set; }
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
        animator = GetComponent<Animator>();
        lampScript = LampScript.Instance;

        movePlayer = MovePlayer.Instance;

        slowness = movePlayer.moveSpeed / 2;
        speed = movePlayer.moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (animator != null)
        {
            if (defend)
            {
                HandleDefenceWalkingAnimation();
            }
            else
            {
                HandleWalkingAnimation();
            }
            
            HandleDefenceAnimation();
        }
    }
    
    void HandleWalkingAnimation()
    {
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            animator.SetTrigger("Walk");
            animator.SetBool("IsWalking", true);
        }
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            animator.SetTrigger("Walk");
            animator.SetBool("IsWalking", true);
        }
        if(Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            animator.SetTrigger("Walk");
            animator.SetBool("IsWalking", false);
        }
    }
    
    void HandleDefenceWalkingAnimation()
    {
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            animator.SetTrigger("Walk");
            animator.SetBool("IsWalking", true);
        }
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            animator.SetTrigger("Walk");
            animator.SetBool("IsWalking", true);
        }
        if(Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            animator.SetTrigger("Walk");
            animator.SetBool("IsWalking", false);
        }
    }

    void HandleDefenceAnimation()
    {
        if (Input.GetMouseButtonDown(1)) // Rechter Mausklick
        { 
            if (!defend)
            {
                if (lampScript != null && lampScript.getStatus())
                {
                    animator.SetTrigger("Defence");
                    movePlayer.moveSpeed = slowness;
                    defend = true;
                    defenceTimer = 0f;
                    ChangeToColor(255f, 91f, 0f);
                }
            }
            else
            {
                disableDefence();
            }
        }

        if (defend)
        {
            defenceTimer += Time.deltaTime; // Timer erhöhen

            if (defenceTimer >= MaxDefenceTimer)
            {
                lampScript.setStatus(false);
            }

            if (lampScript != null && !lampScript.getStatus())
            {
                disableDefence();
            }
        }
    }

    void disableDefence()
    {
        animator.SetTrigger("Normal");
        movePlayer.moveSpeed = speed;
        defend = false;
        ChangeToColor(255f, 147f, 0f);
    }

    public void ChangeToColor(float r, float g, float b)
    {
        if (DefenceLight != null)
        {
            DefenceLight.color = new Color(r / 255f, g / 255f, b / 255f);
        }
    }

    public bool getDefend()
    {
        return defend;
    }
}
