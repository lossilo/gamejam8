using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    [Header("Enemy Stats")]
    [SerializeField] float enemySpeed;
    [SerializeField] int enemyDamage;
    [SerializeField] int enemyHealth;

    [Header("Patrol")]
    [SerializeField] private Transform patrolPoint;
    [SerializeField] int patrolAmount;
    private Vector2 newPatrolPos;
    private Vector2 enemyPos;
    private bool patrolled = false;
    private bool stop = false;

    [Header("Other")]
    //[SerializeField] PlayerScript playerScript;
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

        if (stop == true)
        {
            enemyRb.linearVelocityX = 0;
        }

        if (stop == false)
        {
            if (newPatrolPos.x > enemyPos.x)
            {
                transform.localEulerAngles = new Vector3(0, 0, 0);
                enemyRb.linearVelocityX = enemySpeed;
            }
            if (newPatrolPos.x < enemyPos.x)
            {
                transform.localEulerAngles = new Vector3(0, 180, 0);
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
        //playerScript.health = playerScript.health - enemyDamage;
    }
    public void DamageEnemy(int damageTaken)
    {
        enemyHealth = enemyHealth - damageTaken;

        if (enemyHealth <= 0)
        {
            Destroy(enemyHolder);
        }
    }
}
