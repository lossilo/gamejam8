using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] GameObject deathScreen;

    [SerializeField] PauseScreen pauseScreen;

    public bool dead = false;
    
    private void Start()
    {
        deathScreen.SetActive(false);
        dead = false;
    }

    private void Update()
    {
        if (dead == true)
        {
            pauseScreen.gamePaused = true;
        }
    }

    public void Die()
    {
        deathScreen.SetActive(true);
        Time.timeScale = 0;
        dead = true;
    }
}
