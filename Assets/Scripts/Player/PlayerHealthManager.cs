using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    [SerializeField] GameObject[] healthIndicators;
    [SerializeField] int playerHealth = 3;

    [SerializeField] DeathScreen deathScreen;
    

    public void TakeDamage(int damageTaken)
    {
        playerHealth -= damageTaken;
    }

    private void Update()
    {
        for (int i = 0; i < healthIndicators.Length; i++)
        {
            healthIndicators[i].SetActive(i < playerHealth);
        }

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
