using UnityEngine;
using UnityEngine.Playables; // Потрібно для Timeline

public class CutsceneTrigger : MonoBehaviour
{
    [SerializeField] private PlayableDirector cutscene; // Катсцена з Timeline
    //[SerializeField] private GameObject player; // Гравець, щоб вимкнути керування

    private bool hasPlayed = false; // Щоб не активувати кілька разів

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasPlayed)
        {
            hasPlayed = true;
            cutscene.Play(); // Запускаємо катсцену
           // player.SetActive(false); // Вимикаємо керування гравцем
           // cutscene.stopped += OnCutsceneEnd; // Додаємо подію на завершення катсцени
        }
    }

 /*   private void OnCutsceneEnd(PlayableDirector director)
    {
        player.SetActive(true); // Повертаємо керування гравцем
    }*/
}