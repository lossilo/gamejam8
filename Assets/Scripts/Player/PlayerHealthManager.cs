using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    [SerializeField] int playerHealth = 3;

    [SerializeField] DeathScreen deathScreen;
    

    public void TakeDamage(int damageTaken)
    {
        playerHealth -= damageTaken;
    }

    private void Update()
    {
        if (playerHealth <= 0)
        {
            PlayerDie();
        }
    }

    void PlayerDie()
    {
        deathScreen.Die();
        Destroy(this);
    }

}
