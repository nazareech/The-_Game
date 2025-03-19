using UnityEngine;

public class _Weapon : MonoBehaviour
{
    [Header("Common Weapon Settings")]
    [SerializeField] protected float rotationSpeed = 5f; // Швидкість повороту
    [SerializeField] protected GameObject bullet;        // Префаб кулі
    [SerializeField] protected Transform shootPosition;  // Позиція виліту куль
    [SerializeField] protected GameObject Fire;          // Об'єкт для анімації вогню
    [SerializeField] protected float fireRate = 0.2f;    // Затримка між пострілами

    [Header("Sleeve Settings")]
    [SerializeField] float angeleSleeveRunout = 5f;     // Кут розбігу гільз у градусах
    [SerializeField] GameObject sleeve;                 // Префаб гільзи
    [SerializeField] Transform sleevePosition;          // Позиція виліту гільз

    [Header("Shake-shoot Settings")]
    public float amplitude = 1f;
    public float frequency = 1f;
    public float duration = 0.2f;

    protected Animator fireAnim;
    protected Animator gunAnim;         // Animator для зброї (тепер private)

    protected SpriteRenderer gunRender; // Для перевертання спрайту

    protected float nextFireTime = 0f;  // Час наступного пострілу

    protected virtual void Start()
    {
        // Отримуємо компонент Animator з поточного об'єкта
        gunAnim = GetComponent<Animator>();
        if (gunAnim == null)
        {
            Debug.LogError("Animator component not found on the gun object!");
        }

        if (Fire != null)
        {
            fireAnim = Fire.GetComponent<Animator>();
        }
        else
        {
            Debug.LogError("Fire object is not assigned!");
        }

        gunRender = GetComponent<SpriteRenderer>();
        if (gunRender == null)
        {
            Debug.LogError("SpriteRenderer component is not found on the gun object!");
        }
    }

    protected virtual void Update()
    {
        GunRotation();

        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate; // Оновлюємо час наступного пострілу
            Shoot();
        }
    }

    protected virtual void GunRotation()
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
            gunRender.flipY = true; // Перевертаємо спрайт по вертикалі
        }
        else // Якщо курсор праворуч від зброї
        {
            gunRender.flipY = false; // Повертаємо спрайт у нормальний стан
        }
    }

    protected virtual void Shoot()
    {
        // Вмикаємо тряску
        CameraController.cameraShake?.Invoke(amplitude, frequency, duration);

        ShootAnim();
        // Отримуємо поточний кут стрільби
        float currentAngle = Mathf.Atan2(shootPosition.up.y, shootPosition.up.x) * Mathf.Rad2Deg;

        // Створюємо кут стрільби
        Quaternion shootRotation = Quaternion.Euler(0, 0, currentAngle);

        // Створюємо патрон з точним кутом
        Instantiate(bullet, shootPosition.position, shootRotation);
        Debug.Log("BemB! Weapon shot!");

        GetSpreadAngle();

        // Додатково: можна додати ефект віддачі або звук пострілу
        // recoilAnim.SetTrigger("Shoot"); // Анімація віддачі
        // audioSource.PlayOneShot(shootSound); // Звук пострілу
    }

    protected virtual void ShootAnim()
    {
        gunAnim.SetTrigger("Shoot");    // Запускаємо анімацію стрільби
        fireAnim.SetTrigger("Shoot");   // Запускаємо анімацію вогню
    }


    // Виліт гільз
    void GetSpreadAngle()
    {
        // Отримуємо поточний кут виліту гільз
        float currentAngle = Mathf.Atan2(shootPosition.up.y, shootPosition.up.x) * Mathf.Rad2Deg;

        // Додаємо випадкове відхилення в межах розбігу
        float randomSpread = Random.Range(-angeleSleeveRunout, angeleSleeveRunout);
        float newAngle = currentAngle + randomSpread;

        // Створюємо новий кут виліту гільз
        Quaternion spreadRotation = Quaternion.Euler(0, 0, newAngle);

        // Створюємо гільзу з новим кутом
        Instantiate(sleeve, sleevePosition.position, spreadRotation);
        Debug.Log("'Press BaBah!'");
    }
}
