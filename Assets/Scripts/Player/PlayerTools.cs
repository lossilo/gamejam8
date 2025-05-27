using UnityEngine;

public class PlayerTools : MonoBehaviour
{
    [SerializeField] private Sword playerSword;

    public void SwordAttack()
    {
        playerSword.UseSword(transform.localScale.x);
    }
}
