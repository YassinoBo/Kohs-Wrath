using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyScript : MonoBehaviour
{
    public Transform player; // Spieler Transform
    public Transform vent1; // Position des ersten Lüftungsschachts
    public Transform vent2; // Position des zweiten Lüftungsschachts
    public Transform vent3; // Position des dritten Lüftungsschachts
    public Transform vent4; // Position des vierten Lüftungsschachts
    public float spawnDelay = 20f; // Zeit zwischen Spawns
    public float armReach = 2f; // Reichweite des Arms

    public GameObject enemyPrefab; // Prefab des Enemies
    private GameObject currentEnemy;
    public String LastLocation = "";

    private Transform currentVent; // Das aktuelle Schacht, aus dem der Gegner kommt
    private bool isActive = false; // Gibt an, ob der Gegner aktiv ist

    public MinigameInteraction minigameInteraction;
    public DoorInteraction doorInteraction;

    private SoundScript soundScript;

    private void Start()
    {
        soundScript = SoundScript.Instance;
        StartCoroutine(SpawnEnemyRoutine());
    }

    private IEnumerator SpawnEnemyRoutine()
    {
        while (true)
        {
            if (!isActive) // Spawn nur, wenn kein Enemy aktiv ist
            {
                if (!minigameInteraction.getAttack())
                {
                    yield return new WaitForSeconds(1f);
                    continue;
                }

                yield return new WaitForSeconds(spawnDelay - 5f); // Warte bis 5 Sekunden vor dem Spawn

                if (enemyPrefab == null)
                {
                    Debug.LogError("EnemyPrefab ist null! Bitte im Inspector zuweisen.");
                    continue;
                }

                // Wähle eine zufällige Lüftung als Spawnpunkt
                switch (doorInteraction.getLocation())
                {
                    case "MainHall":
                        currentVent = Random.Range(0, 2) == 0 ? vent1 : vent2;
                        LastLocation = "MainHall";
                        if (currentVent == vent1)
                        {
                            soundScript.Vent1Sound();
                        }
                        else
                        {
                            soundScript.Vent2Sound();
                        }
                        break;
                    case "Corpse":
                        currentVent = vent3;
                        LastLocation = "Corpse";
                        soundScript.Vent3Sound(); // Spiele den Sound für Corpse
                        break;
                    case "Basement":
                        currentVent = vent4;
                        LastLocation = "Basement";
                        soundScript.Vent4Sound(); // Spiele den Sound für Basement
                        break;
                    default:
                        continue;
                }

                // Warte die restlichen 5 Sekunden
                yield return new WaitForSeconds(5f);

                // Spawne neuen Enemy
                currentEnemy = Instantiate(enemyPrefab, currentVent.position, currentVent.rotation);
                currentEnemy.GetComponent<EnemyAI>().Initialize(player, this, currentVent);
                isActive = true;

                Debug.Log($"Enemy wurde bei {currentVent.name} gespawnt.");
            }

            yield return null;
        }
    }

    public void EnemyReturnedToVent()
    {
        Debug.Log("Enemy hat die Lüftung erreicht und verschwindet.");
        Destroy(currentEnemy); // Enemy zerstören
        currentEnemy = null;
        isActive = false; // Markiere, dass kein Enemy mehr aktiv ist
    }
}
