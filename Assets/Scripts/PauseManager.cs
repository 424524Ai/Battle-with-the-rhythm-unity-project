using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static bool isPaused = false;

    [Header("UI & Audio")]
    public GameObject pauseMenu; // PauseMenu Panel
    public AudioSource musicAudioSource; // music playing

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;      // pause game time
        musicAudioSource.Pause();  // pause the music
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;      // continue game time
        musicAudioSource.Play();   // continue music
        isPaused = false;
    }
}
