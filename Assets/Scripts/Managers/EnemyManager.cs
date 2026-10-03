using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public EnemyPool enemyPool;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;


    void Start()
    {
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }


    void Spawn()
    {
        if (playerHealth.currentHealth <= 0f)
        {
            return;
        }

        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        GameObject enemy = enemyPool.GetEnemy();

        if (enemy != null)
        {
            enemy.transform.position = spawnPoints[spawnPointIndex].position;
            enemy.transform.rotation = spawnPoints[spawnPointIndex].rotation;
        }
    }
}
