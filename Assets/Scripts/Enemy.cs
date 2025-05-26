using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class EnemyScript : MonoBehaviour
{

    [Header("Enemy Stats")]
    [SerializeField] float enemySpeed;

    [Header("Patrol")]
    [SerializeField] Vector2 enemyPos;
    [SerializeField] Transform patrolPoint;
    [SerializeField] int patrolAmount;

    [Header("Other")]
    [SerializeField] Rigidbody2D enemyRb;

    [Header("Temp Debug")]
    [SerializeField] Vector2 newPatrolPos;
    [SerializeField] bool facingRight = true;
    [SerializeField] bool patrolled = false;
    [SerializeField] bool stop = false;

    private void Update()
    {
        EnemyMove();
    }

    private void Awake()
    {
        Patrol();
    }

    public void Patrol()
    {
        stop = false;

        if(patrolled == false)
        {
            newPatrolPos.x = transform.position.x + patrolAmount;
        }
        if(patrolled == true)
        {
            newPatrolPos.x = transform.position.x - patrolAmount;
        }

        patrolled = !patrolled;

        newPatrolPos.y = transform.position.y;
        patrolPoint.position = newPatrolPos;


        enemyPos = (Vector2)transform.position;


        if (newPatrolPos.x > enemyPos.x)
        {
            stop = true;
            ArrivedToPatrolPos();
        }
    }
    void EnemyMove()
    {
        enemyPos = (Vector2)transform.position;

        if (stop == true)
        {
            enemyRb.linearVelocityX = 0;
        }

        if (stop == false)
        {
            if (newPatrolPos.x > enemyPos.x)
            {
                transform.localEulerAngles = new Vector3(0, 0, 0);
                facingRight = true;
                enemyRb.linearVelocityX = enemySpeed;
            }
            if (newPatrolPos.x < enemyPos.x)
            {
                transform.localEulerAngles = new Vector3(0, 180, 0);
                facingRight = false;
                enemyRb.linearVelocityX = -enemySpeed;
            }
        }
    }

    void ArrivedToPatrolPos()
    {
        Debug.Log("Patrolled");
        Patrol();
    }
}
