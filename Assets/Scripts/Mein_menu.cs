using UnityEngine;
using UnityEngine.SceneManagement;

public class Mein_menu : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    void Update() { }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame()
    {
        if (Application.isPlaying)
        {
            Debug.Log("The Game will close.");
            Application.Quit();
        }
    }
}
