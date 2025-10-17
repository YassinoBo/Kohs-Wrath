
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private Transform player;
    private EnemyScript spawner;
    private Transform vent;
    private NavMeshAgent agent;

    private bool isReturning = false;
    private bool hasDamagedPlayer = false;

    public float sanityLossValue = 10f; // Sanity-Wert, der abgezogen wird
    private SoundScript sound;
    

    private void Start()
    {
        sound = SoundScript.Instance;
    }


    public void Initialize(Transform playerTransform, EnemyScript spawnerScript, Transform ventTransform)
    {
        player = playerTransform;
        spawner = spawnerScript;
        vent = ventTransform;
        agent = GetComponent<NavMeshAgent>();
        agent.isStopped = false;
    }

    private void Update()
    {
        if (isReturning || player == null) return;
        
        if (spawner.LastLocation != spawner.doorInteraction.getLocation())
        {
            spawner.LastLocation = spawner.doorInteraction.getLocation();
            Debug.Log("Player hat den Raum gewechselt. Enemy wird despawnt.");
            Despawn();
            return;
        }
        
        if (agent.isOnNavMesh)
        {
            agent.destination = player.position;
            sound.CrawlingSound();

            // Pr�fen, ob der Spieler den Arm nutzt, um den Gegner abzuwehren
            if (DefenceAnimation.Instance != null && DefenceAnimation.Instance.getDefend())
            {
                CheckArmInteraction();
            }

            // Pr�fen, ob der Spieler in Reichweite ist
            CheckPlayerInteraction();
        }
        else
        {
            Debug.LogError("Enemy ist nicht auf dem NavMesh!");
        }
    }
    
    private void Despawn()
    {
        spawner.EnemyReturnedToVent(); // Informiere den Spawner
        Destroy(gameObject); // Entferne den Gegner
    }




    //Funktiniert 
    /*
    private void CheckPlayerInteraction()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= 1.5f && !hasDamagedPlayer) // Reichweite f�r Interaktion
        {
            Debug.Log("Enemy hat den Spieler ber�hrt und Sanity reduziert.");
            DamagePlayer();
            ReturnToVent();
        }
    }

   
    
    private void DamagePlayer()
    {
        // Zugriff auf das SanityTestScript des Spielers
        SanityTestScript sanityTestScript = player.GetComponent<SanityTestScript>();

        if (sanityTestScript != null)
        {
            sanityTestScript.sanity -= sanityLossValue; // Sanity-Wert verringern
            if (sanityTestScript.sanity < 0) sanityTestScript.sanity = 0; // Sicherheit, dass Sanity nicht negativ wird
            hasDamagedPlayer = true; // Verhindert mehrfachen Schaden
        }
        else
        {
            Debug.LogError("SanityTestScript wurde beim Spieler nicht gefunden!");
        }
    }
    */




    private void CheckPlayerInteraction()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= 1.5f && !hasDamagedPlayer) // Reduzieren Sie die Distanz für die Interaktion
        {
            StartCoroutine(AttackPlayer());
        }
    }

    private IEnumerator AttackPlayer()
    {
        yield return new WaitForSeconds(0); // Warten für 2 Sekunden
        DamagePlayer();
        ReturnToVent();
    }


    private void DamagePlayer()
    {
        // Kamera rütteln
        CameraShake cameraShake = Camera.main.GetComponent<CameraShake>();
        if (cameraShake != null)
        {
            cameraShake.StartCoroutine(cameraShake.Shake(0.5f, 0.2f));
        }
        else
        {
            Debug.LogError("CameraShake script not found on main camera!");
        }

        // Reduzierung des Sanity-Werts
        SanityTestScript sanityTestScript = player.GetComponent<SanityTestScript>();
        if (sanityTestScript != null)
        {
            sanityTestScript.sanity -= sanityLossValue;
            hasDamagedPlayer = true;
        }
        else
        {
            Debug.LogError("SanityTestScript was not found on the player!");
        }
    }

   


    private void CheckArmInteraction()
    {
        if (isReturning) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= spawner.armReach) // Reichweite f�r die Abwehr durch den Arm
        {
            Debug.Log("Enemy wurde durch den ausgestreckten Arm abgewehrt und zieht sich zur�ck.");
            ReturnToVent();
        }
    }

    public void ReturnToVent()
    {
        Debug.Log("AUfruf");
        if (vent == null || !agent.isOnNavMesh) return;

        isReturning = true;
        agent.SetDestination(vent.position);
        StartCoroutine(WaitUntilAtVent());
    }

    private IEnumerator WaitUntilAtVent()
    {
        while (Vector3.Distance(transform.position, vent.position) > 0.1f)
        {
            yield return null;
        }

        spawner.EnemyReturnedToVent(); // Informiere den Spawner
        Destroy(gameObject); // Entferne den Enemy
    }
}



