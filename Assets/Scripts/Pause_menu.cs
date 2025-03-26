using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause_menu : MonoBehaviour
{

    [Header("Music Settings")]
    public GameObject pauseMusic;
    public static bool isPaused = false;
    public static bool Music_is_Play = true;

    [Header("UI Settings")]
    public GameObject pauseMenuUI;
    public GameObject sliderHP;
    public GameObject interactionTip;
 
    //public GameObject sliderOverheat;


    private AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = pauseMusic.GetComponent<AudioSource>();
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
        audioSource.UnPause();
        // pauseMusic.SetActive(true);

        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        // Music_is_Play = false;

        // Включаємо повзунок здоров'я
        sliderHP.SetActive(true);
    }
    public void Pause()
    {
        interactionTip.SetActive(false);
        audioSource.Pause();
        // pauseMusic.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        // Music_is_Play = true;

        // Виключаємо повзунок здоров'я
        sliderHP.SetActive(false);
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
        //Resume();
        Time.timeScale = 1f; // Відновлюємо нормальний час
        SceneManager.LoadScene("_Menu");
    }
}