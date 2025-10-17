/*
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class RespawnManager : MonoBehaviour
{
    public Transform vent1; // Position des ersten Lüftungsschachts
    public Transform vent2; // Position des zweiten Lüftungsschachts
    public GameObject enemyPrefab; // Prefab des Enemies
    public Transform player; // Transform des Spielers
    public float spawnInterval = 5f; // Zeitintervall für Enemy-Spawn
    public float armReach = 2f; // Reichweite des ausgestreckten Arms

    private GameObject currentEnemy; // Referenz zum aktuellen Enemy
    private Transform currentVent; // Das Schacht, aus dem der Enemy kommt
    private NavMeshAgent enemyAgent; // NavMeshAgent für die Verfolgung

    private bool isEnemyActive = false; // Kontrolliert, ob ein Enemy aktiv ist

    void Start()
    {
        StartCoroutine(EnemySpawnRoutine());
    }

    void Update()
    {
        if (isEnemyActive && currentEnemy != null && enemyAgent != null)
        {
            // Verfolge den Spieler
            enemyAgent.SetDestination(player.position);
        }

        // Prüfe, ob der Arm ausgestreckt ist und interagiere mit dem Enemy
        if (DefenceAnimation.Instance != null && DefenceAnimation.Instance.getDefend())
        {
            CheckArmInteraction();
        }
    }

    IEnumerator EnemySpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            if (!isEnemyActive)
            {
                SpawnEnemy();
            }
        }
    }

    void SpawnEnemy()
    {
        if (isEnemyActive) return;

        // Berechne die Spawn-Position
        Vector3 toVent1 = vent1.position - player.position;
        Vector3 toVent2 = vent2.position - player.position;

        float angleToVent1 = Vector3.Angle(player.forward, toVent1);
        float angleToVent2 = Vector3.Angle(player.forward, toVent2);

        currentVent = angleToVent1 > angleToVent2 ? vent1 : vent2;

        // Spawne den Enemy
        currentEnemy = Instantiate(enemyPrefab, currentVent.position, Quaternion.identity);
        enemyAgent = currentEnemy.GetComponent<NavMeshAgent>();

        if (enemyAgent == null)
        {
            Debug.LogError("Enemy Prefab hat keinen NavMeshAgent!");
            Destroy(currentEnemy);
            currentEnemy = null;
            return;
        }

        if (!enemyAgent.isOnNavMesh)
        {
            Debug.LogError("Enemy ist nicht korrekt auf dem NavMesh gespawnt!");
        }
        else
        {
            Debug.Log("Enemy korrekt auf NavMesh gespawnt.");
        }

        isEnemyActive = true;
    }


    void CheckArmInteraction()
    {
        if (currentEnemy == null) return;

        float distanceToEnemy = Vector3.Distance(player.position, currentEnemy.transform.position);
        Debug.Log("Abstand zum Enemy: " + distanceToEnemy);

        if (distanceToEnemy <= armReach)
        {
            Debug.Log("Enemy wird durch Arm zurückgedrängt.");
            RetreatEnemy();
        }
        else
        {
            Debug.Log("Enemy außerhalb der Arm-Reichweite.");
        }
    }


    void RetreatEnemy()
    {
        if (currentEnemy == null || enemyAgent == null) return;

        // Setze das Ziel auf das ursprüngliche Schacht
        enemyAgent.SetDestination(currentVent.position);
        StartCoroutine(EnemyRetreatRoutine());
    }

    IEnumerator EnemyRetreatRoutine()
    {
        while (Vector3.Distance(currentEnemy.transform.position, currentVent.position) > 0.1f)
        {
            Debug.Log("Enemy bewegt sich zurück zum Lüftungsschacht.");
            yield return null;
        }

        Debug.Log("Enemy hat das Lüftungsschacht erreicht und wird zerstört.");
        Destroy(currentEnemy);
        currentEnemy = null;
        isEnemyActive = false;
    }

}
*/
