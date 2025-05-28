using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    [SerializeField] GameObject pauseScreen;

    [SerializeField] DeathScreen deathScreen;

    bool pauseScreenActive = false;

    public bool gamePaused = false;

    private void Start()
    {
        pauseScreen.SetActive(false);
        gamePaused = false;
    }

    private void Update()
    {
        ReadPlayerInputs();
        GamePauseCheck();
    }

    void ReadPlayerInputs()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && deathScreen.dead == false)
        {
            gamePaused = !gamePaused;
            pauseScreen.SetActive(!gamePaused);
        }
    }

    void GamePauseCheck()
    {
        if (gamePaused == false)
        {
            Time.timeScale = 1.0f;
        }
        if (gamePaused == true)
        {
            Time.timeScale = 0f;
        }
    }
}
