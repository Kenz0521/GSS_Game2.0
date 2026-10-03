using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public EnemyData enemyData;
    public int currentHealth;


    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;
    int deadHash;
    ScoreManager scoreManager;

    void Awake()
    {
        anim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        currentHealth = enemyData.startingHealth;

        deadHash = Animator.StringToHash("Dead");

        scoreManager = FindObjectOfType<ScoreManager>();
    }


    void OnEnable()
    {
        currentHealth = enemyData.startingHealth;
        isDead = false;
        isSinking = false;

        if (capsuleCollider != null)
        {
            capsuleCollider.isTrigger = false;
        }
    }


    void Update()
    {
        if (isSinking)
        {
            transform.Translate(-Vector3.up * enemyData.sinkSpeed * Time.deltaTime);
        }
    }


    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        if (isDead)
            return;

        enemyAudio.Play();

        currentHealth -= amount;

        hitParticles.transform.position = hitPoint;
        hitParticles.Play();

        if (currentHealth <= 0)
        {
            Death();
        }
    }


    void Death()
    {
        isDead = true;

        capsuleCollider.isTrigger = true;

        anim.SetTrigger(deadHash);

        enemyAudio.clip = enemyData.deathClip;
        enemyAudio.Play();
    }


    public void StartSinking()
    {
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;

        isSinking = true;

        scoreManager.AddScore(enemyData.scoreValue);

        Invoke("ReturnToPool", 2f);
    }


    void ReturnToPool()
    {
        gameObject.SetActive(false);
    }
}
