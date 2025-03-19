using UnityEngine;

public class Fists : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 5f; // Швидкість повороту
    private SpriteRenderer fistsRender; // Для перевертання спрайту

    void Start()
    {
        fistsRender = GetComponent<SpriteRenderer>();
    }

    void Update()
    {        
        FistsRotation();
    }

    void FistsRotation()
    {
        // Отримуємо різницю між позицією курсора та позицією зброї
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        // Обчислюємо кут повороту
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        // Створюємо кватерніон для повороту
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Плавний поворот
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

        // Перевертання спрайту
        if (dir.x < 0) // Якщо курсор ліворуч від зброї
        {
            fistsRender.flipY = true; // Перевертаємо спрайт по вертикалі
        }
        else // Якщо курсор праворуч від зброї
        {
            fistsRender.flipY = false; // Повертаємо спрайт у нормальний стан
        }
    }
}
