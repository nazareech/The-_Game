using UnityEngine;

public class GUN : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    [SerializeField] float spreadAngle = 5f;    // Кут розбігу патронів у градусах

    [SerializeField] GameObject bullet;
    [SerializeField] Transform shootpos;

    public float fireRate = 0.2f; // Затримка між пострілами
    private float nextFireTime = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }

        GunRotation();
    }

    void GunRotation()
    {
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

    }

    void Shoot()
    {

        // Отримуємо поточний кут стрільби
        float currentAngle = Mathf.Atan2(shootpos.up.y, shootpos.up.x) * Mathf.Rad2Deg;

        // Додаємо випадкове відхилення в межах розбігу
        float randomSpread = Random.Range(-spreadAngle, spreadAngle);
        float newAngle = currentAngle + randomSpread;

        // Створюємо новий кут стрільби
        Quaternion spreadRotation = Quaternion.Euler(0, 0, newAngle);

        // Створюємо патрон з новим кутом
        Instantiate(bullet, shootpos.position, spreadRotation);
        Debug.Log("'Press BaBah!'");
    }

}
