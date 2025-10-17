using System;
using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    public Transform playerTransform;
    private bool activeTransition = false;
    private Camera playerCamera;
    private SceneTransition transition;
    private MoveCam cam;
    private SoundScript soundScript;

    private DoorKeyCheck keyCheck; // Referenz auf das Schlüsselprüfungs-Skript
    
    private MovePlayer movePlayer;
    private bool oneUse = true;
    private GhostScript ghostScript;

    private String location = "Entry";
    
    public SanityTestScript sanityTestScript;
    public EnemyAI _enemyAI;
    public HouseToTutorial _houseToTutorial;

    void Start()
    {
        transition = FindObjectOfType<SceneTransition>();
        playerCamera = GetComponent<Camera>();
        cam = GetComponent<MoveCam>();
        soundScript = GetComponent<SoundScript>();

        keyCheck = GetComponent<DoorKeyCheck>(); // Schlüsselprüfungs-Skript finden
        
        GameObject player = transform.parent.gameObject;
        movePlayer = player.GetComponent<MovePlayer>();
        ghostScript = GhostScript.Instance;
        if (_enemyAI == null)
        {
            Debug.Log("Nicht gesetzt");
        }
    }

    void EnableMovement(bool state)
    {
        cam.canMoveCam = state;
        movePlayer.canMove = state;
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && !activeTransition)
        {
            activeTransition = true;
            //EnableMovement(false);
            

            RaycastHit hit;
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 1.0f))
            {
                if (PruefeZugang(hit)) // Zugang prüfen
                {
                    RayHitCheck(hit);
                }
                else
                {
                    activeTransition = false;
                    //EnableMovement(true);
                }
            }
            else
            {
                activeTransition = false;
               // EnableMovement(true);
            }
        }
    }

    bool PruefeZugang(RaycastHit hit)
    {
        var doorKeyCheck = hit.collider.GetComponent<DoorKeyCheck>();

        // Wenn keine Schlüsselprüfung notwendig, Zugang erlauben
        if (doorKeyCheck == null) return true;

        // Schlüsselprüfung ausführen
        return doorKeyCheck.IstZugangErlaubt();
    }

    void RayHitCheck(RaycastHit hit)
    {
        var comp = hit.collider.tag;
        
        switch (comp)
        {
            case "MainHallToEntry": NewPlayerPosition(new Vector3(-6, 1.275f, 0));
                location = "Entry"; break;
            case "EntryToMainHall": NewPlayerPosition(new Vector3(-9.075f, 1.275f, 0)); 
                location = "MainHall"; break;
            case "CorpseToMainHall": NewPlayerPosition(new Vector3(-11f, 1.275f, 4f)); 
                location = "MainHall"; break;
            case "MainHallToCorpse": NewPlayerPosition(new Vector3(-11f, 1.275f, 7));
                location = "Corpse"; break;
            case "MainHallToBasement": 
                if (sanityTestScript != null && sanityTestScript.sanity <= 300f && oneUse)
                {
                    oneUse = false;
                    ghostScript.SetAllowAttack(false);
                    NewPlayerPosition(new Vector3(-14.2119999f, 1.24399996f, -40.3209991f));
                    location = "Entry";
                }
                else
                {
                    NewPlayerPosition(new Vector3(-13.935f, 1.275f, -7.669f));
                    location = "Basement";
                }
                break;
            case "BasementToMainHall": NewPlayerPosition(new Vector3(-13.935f, 1.275f, -1.883f));
                location = "MainHall";break;
            case "Entry": SoundScript.Instance.LockedDoorSound(); activeTransition = false; EnableMovement(true);
                if (_houseToTutorial.canTriggerKoh)
                {
                    _houseToTutorial.doorOpen = true; 
                } 
                break;
            case "ParaDoor": SoundScript.Instance.LockedDoorSound(); activeTransition = false; EnableMovement(true); break;
            default: activeTransition = false;  break;
        }
    }

    void NewPlayerPosition(Vector3 playerposition)
    {
        EnableMovement(false);
        SpielerInventar.Instance.SetzeInventarSichtbarkeit(false); // Inventar ausblenden

        transition.FadeBlack(() =>
        {
            playerTransform.transform.position = playerposition;
            soundScript.DoorSound();
            transition.StayBlack(2.5f, () =>
            {
                transition.FadeClear(() =>
                {
                    SpielerInventar.Instance.SetzeInventarSichtbarkeit(true); // Inventar wieder einblenden
                    activeTransition = false;
                    EnableMovement(true);
                });
            });
        });
    }

    public String getLocation()
    {
        return location;
    }

    public void setLocation(String newLocation)
    {
        location = newLocation;
    }

}
