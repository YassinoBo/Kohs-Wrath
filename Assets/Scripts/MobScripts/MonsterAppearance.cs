  using System.Collections;
using UnityEngine;

public class MonsterAppearance : MonoBehaviour
{
    public Transform spawnPoint;  // Zuweisen im Inspector
    public Transform endPoint;    // Zuweisen im Inspector
    public GameObject monsterPrefab; // Das Monster-Präfab zuweisen
    public float speed = 5.0f; // Geschwindigkeit der Bewegung

    private void Start()
    {
        StartCoroutine(SpawnAndMoveMonsters());
    }

    IEnumerator SpawnAndMoveMonsters()
    {
        while (true)
        {
            GameObject monster = Instantiate(monsterPrefab, spawnPoint.position, Quaternion.identity);
            float journey = 0f;
            while (journey <= 1f)
            {
                journey += Time.deltaTime * speed / Vector3.Distance(spawnPoint.position, endPoint.position);
                monster.transform.position = Vector3.Lerp(spawnPoint.position, endPoint.position, journey);
                yield return null;
            }
            Destroy(monster);
            yield return new WaitForSeconds(2.0f); // Wartezeit zwischen den Spawns
        }
    }
}
