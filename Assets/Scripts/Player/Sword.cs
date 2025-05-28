using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;

public class Sword : MonoBehaviour
{
    [SerializeField] int damageToEnemy;

    [Space]

    [SerializeField] private float swordMoveDistance;
    [SerializeField] private float swordAttackTime;
    [SerializeField] private float swordRetractTime;
    [SerializeField] private LayerMask attackableLayer;
    [SerializeField] private AudioClip swordSound;

    private bool isMovingSword;
    private float setMoveTime;
    private float swordMoveDirection;

    private Collider2D swordCollider;
    private PlayerMovement playerMovement;
    private SoundEffectManager soundEffectManager;

    private void Start()
    {
        swordCollider = GetComponent<Collider2D>();
        playerMovement = FindFirstObjectByType<PlayerMovement>();
        soundEffectManager = FindFirstObjectByType<SoundEffectManager>();
    }

    private void Update()
    {
        if (isMovingSword)
        {
            transform.position += new Vector3(swordMoveDistance * swordMoveDirection / setMoveTime * Time.deltaTime, 0, 0);
        }
    }

    public void UseSword(float initialDirection)
    {
        if (isMovingSword) { return; }
        StartCoroutine(UseSwordRoutine(initialDirection));
    }

    private IEnumerator UseSwordRoutine(float initialDirection)
    {
        soundEffectManager.PlaySound(swordSound);
        playerMovement.MoveBlock = true;
        swordCollider.enabled = true;
        setMoveTime = swordAttackTime;
        isMovingSword = true;
        swordMoveDirection = initialDirection;

        yield return new WaitForSeconds(swordAttackTime);

        setMoveTime = swordRetractTime;
        swordMoveDirection = -initialDirection;

        yield return new WaitForSeconds(swordRetractTime);

        playerMovement.MoveBlock = false;
        isMovingSword = false;
        swordCollider.enabled = false;
        transform.localPosition = Vector3.zero;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & attackableLayer) != 0)
        {
            other.GetComponent<Enemy>().Damage(damageToEnemy);
        }
    }
}
