using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [Header("Enemy Stats")]
    [SerializeField] float enemySpeed;
    [SerializeField] int enemyDamage = 1;
    [SerializeField] int enemyHealth;

    [Header("Patrol")]
    [SerializeField] private Transform patrolPoint;
    [SerializeField] int patrolAmount;
    private Vector2 newPatrolPos;
    private Vector2 enemyPos;
    private bool patrolled = false;
    private bool stop = false;

    [Header("Knockback")]
    [SerializeField] float knockBack;
    [SerializeField] float knockBackMultiplier;
    [SerializeField] float knockBackDuration;
    [SerializeField] bool gettingKnocked;

    [Header("Other")]
    [SerializeField] PlayerHealthManager playerHealth;
    [SerializeField] GameObject enemyHolder;
    [SerializeField] Rigidbody2D enemyRb;


    private void Awake()
    {
        Patrol();
    }
    private void Update()
    {
        EnemyMove();
    }
    void Patrol()
    {
        stop = false;

        if (patrolled == false)
        {
            newPatrolPos.x = transform.position.x + patrolAmount;
        }
        if (patrolled == true)
        {
            newPatrolPos.x = transform.position.x - patrolAmount;
        }

        patrolled = !patrolled;

        newPatrolPos.y = transform.position.y;
        patrolPoint.position = newPatrolPos;
    }
    void EnemyMove()
    {
        enemyPos = (Vector2)transform.position;


        if (patrolled == false)
        {
            if (newPatrolPos.x > transform.position.x)
            {
                stop = true;
                Patrol();
            }
        }
        if (patrolled == true)
        {
            if (newPatrolPos.x < transform.position.x)
            {
                stop = true;
                Patrol();
            }
        }

        if (stop == true && gettingKnocked == false)
        {
            enemyRb.linearVelocityX = 0;
        }

        if (stop == false)
        {
            if (newPatrolPos.x > enemyPos.x)
            {
                enemyRb.linearVelocityX = enemySpeed;
            }
            if (newPatrolPos.x < enemyPos.x)
            {
                enemyRb.linearVelocityX = -enemySpeed;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            DamagePlayer();
        }
    }
    void DamagePlayer()
    {
        playerHealth.TakeDamage(enemyDamage);
    }

    public void Damage(int damage)
    {
        StartCoroutine(DamageEnemy(damage));
    }

    public IEnumerator DamageEnemy(int damageTaken)
    {
        enemyHealth -= damageTaken;

        gettingKnocked = true;
        stop = true;
        enemyRb.linearVelocityY = knockBack * knockBackMultiplier;

        if(patrolled == true)
        {
            enemyRb.linearVelocityX = -knockBack;
        }
        if (patrolled == false)
        {
            enemyRb.linearVelocityX = knockBack;
        }

        yield return new WaitForSeconds(knockBackDuration);
        gettingKnocked = false;
        stop = false;

        if (enemyHealth <= 0)
        {
            Destroy(enemyHolder);
        }
    }
}