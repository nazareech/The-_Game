using UnityEngine;
using UnityEngine.SceneManagement;

public class Mein_menu : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (PlayerPrefs.HasKey("Language") == false)
        {
            if (Application.systemLanguage == SystemLanguage.Ukrainian || Application.systemLanguage == SystemLanguage.Russian) PlayerPrefs.SetInt("Language", 1) ;
            else if (Application.systemLanguage == SystemLanguage.Polish) PlayerPrefs.SetInt("Language", 2);
            else if (Application.systemLanguage == SystemLanguage.ChineseSimplified || Application.systemLanguage == SystemLanguage.ChineseTraditional) PlayerPrefs.SetInt("Language", 3);
            else  PlayerPrefs.SetInt("Language", 0);
        }
        Translator.Select_language(PlayerPrefs.GetInt("Language"));
    }

    public void Language_change(int languageID)
    {
        PlayerPrefs.SetInt("Language", languageID);
        PlayerPrefs.Save(); // Зберігаємо налаштування
        Translator.Select_language(PlayerPrefs.GetInt("Language"));
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
