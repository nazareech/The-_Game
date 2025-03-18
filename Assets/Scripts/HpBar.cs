using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class HpBar : MonoBehaviour
{
    private float HP = 100f;
    public Image Bar;

    private void Start()
    {
        
    }
    private void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            HP -= 5f;
            Bar.fillAmount = HP / 100f;
        }

        if (HP <= 0)
        {
            GameOver();
        }
    }
    private void GameOver()
    {
        Debug.Log("Игра окончена!");
        // Можно перезагрузить сцену
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        // Или показать экран смерти (если есть соответствующая сцена)
        // SceneManager.LoadScene("GameOverScene");
    }
}
