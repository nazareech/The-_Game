using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause_menu : MonoBehaviour
{

    public static bool isPaused = false;
    public static bool Music_is_Play = true;
    public GameObject pauseMusic;
    public GameObject pauseMenuUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else 
            { 
                Pause(); 
            }
        }
    }
    public void Resume()
    {
        pauseMusic.SetActive(true);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Music_is_Play = false;
    }
    public void Pause()
    {

        pauseMusic.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        Music_is_Play = true;
    }
    public void ExitGame()
    {
        if (Application.isPlaying)
        {
            Debug.Log("The Game will close.");
            Application.Quit();
        }
    }

    public void Go_to_Menu()
    {
        SceneManager.LoadScene("_Menu");
    }
}
