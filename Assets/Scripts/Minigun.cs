using UnityEngine;

public class Minigun : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 5f;  // Швидкість повороту
    [SerializeField] float spreadAngle = 5f;    // Кут розбігу патронів у градусах

    [SerializeField] GameObject bullet;
    [SerializeField] Transform shootPosition;   // Позиція виліту куль

    [SerializeField] GameObject Fire;           // Об'єкт для анімації вогню
    private Animator fireAnim;
    private Animator gunAnim;                   // Animator для мінігана
    private SpriteRenderer fireRender;

    public float fireRate = 0.2f;              // Затримка між пострілами
    private float nextFireTime = 0f;

    public float offset;                       // Додатковий кут для корекції повороту

    private SpriteRenderer gunRender;          // Для перевертання спрайту

    void Start()
    {
        // Отримуємо компонент Animator з поточного об'єкта
        gunAnim = GetComponent<Animator>();
        if (Fire != null)
        {
            fireAnim = Fire.GetComponent<Animator>();
            fireRender = Fire.GetComponent<SpriteRenderer>();
        }
        else
        {
            Debug.LogError("Fire object is not assigned!");
        }

        if (gunAnim == null)
        {
            Debug.LogError("Animator component is not assigned!");
        }

        gunRender = GetComponent<SpriteRenderer>();
        if (gunRender == null)
        {
            Debug.LogError("SpriteRenderer component is not found on the gun object!");
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
            gunAnim.SetTrigger("Shoot");
            fireAnim.SetTrigger("Shoot");
        }

        GunRotation();
    }

    void GunRotation()
    {
        // Отримуємо різницю між позицією курсора та позицією мінігана
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize(); // Нормалізуємо вектор

        // Обчислюємо кут повороту
        float rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        // Цільовий поворот з урахуванням offset
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, rotateZ + offset);

        // Плавний поворот
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);


        // Перевертання спрайту
        if (difference.x < 0)
        {
            gunRender.flipY = true;
        }
        else
        {
            gunRender.flipY = false;
        }
    }

    void Shoot()
    {
        // Отримуємо поточний кут стрільби
        float currentAngle = Mathf.Atan2(shootPosition.up.y, shootPosition.up.x) * Mathf.Rad2Deg;

        // Додаємо випадкове відхилення в межах розбігу
        float randomSpread = Random.Range(-spreadAngle, spreadAngle);
        float newAngle = currentAngle + randomSpread;

        // Створюємо новий кут стрільби
        Quaternion spreadRotation = Quaternion.Euler(0, 0, newAngle);

        // Створюємо патрон з новим кутом
        Instantiate(bullet, shootPosition.position, spreadRotation);
        Debug.Log("'Press BaBah!'");
    }
}