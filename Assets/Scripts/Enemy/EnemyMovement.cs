using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private Transform player;
    private PlayerHealth playerHealth;
    private EnemyHealth enemyHealth;
    private NavMeshAgent navMeshAgent;

    void Awake()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        player = playerObject.transform;
        playerHealth = playerObject.GetComponent<PlayerHealth>();

        enemyHealth = GetComponent<EnemyHealth>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void OnEnable()
    {
        if (navMeshAgent != null)
        {
            navMeshAgent.enabled = true;
        }
    }

    void Update()
    {
        if (enemyHealth.currentHealth > 0 &&
            playerHealth.currentHealth > 0 &&
            navMeshAgent.enabled &&
            navMeshAgent.isOnNavMesh)
        {
            navMeshAgent.SetDestination(player.position);
        }
        else
        {
            if (navMeshAgent.enabled && !navMeshAgent.isOnNavMesh)
            {
                navMeshAgent.enabled = false;
            }

            if (navMeshAgent.enabled && playerHealth.currentHealth <= 0)
            {
                navMeshAgent.enabled = false;
            }
        }
    }
}
