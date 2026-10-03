using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int poolSize = 20;

    private List<GameObject> enemyPool = new List<GameObject>();


    void Awake()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab);

            enemy.SetActive(false);

            enemyPool.Add(enemy);
        }
    }


    public GameObject GetEnemy()
    {
        for (int i = 0; i < enemyPool.Count; i++)
        {
            if (!enemyPool[i].activeInHierarchy)
            {
                enemyPool[i].SetActive(true);

                return enemyPool[i];
            }
        }

        return null;
    }
}
