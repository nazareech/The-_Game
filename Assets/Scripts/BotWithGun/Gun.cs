// using UnityEngine;

// public class Gun : MonoBehaviour
// {
//     public GunType gunType;
//     public GameObject bullet;
//     public Transform shotPoint;
//     public float startTimeBtwShots;
//     public float offset;

//     public enum GunType { Default, Enemy };

//     private float timeBtwShoots;
//     private float rotZ;
//     private Vector3 difference;
//     private Player player;

//     void Start()
//     {
//         player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
//         timeBtwShoots = startTimeBtwShots;
//     }

//     void Update()
//     {
//         // Расчёт направления и угла поворота
//         difference = player.transform.position - transform.position;
//         rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
//         transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

//         // Стрельба с задержкой
//         if (timeBtwShoots <= 0)
//         {
//             shot();
//         }
//         else
//         {
//             timeBtwShoots -= Time.deltaTime;
//         }
//     }

//     public void shot()
//     {
//         Instantiate(bullet, shotPoint.position, Quaternion.Euler(0f, 0f, rotZ));
//         timeBtwShoots = startTimeBtwShots;
//     }
// }


using UnityEngine;

public class Gun : MonoBehaviour
{
    public enum GunType { Default, Enemy }
    public GunType gunType;

    public GameObject bullet;
    public Transform shotPoint;
    public float startTimeBtwShots;
    public float offset;

    public Player player; // Призначте в інспекторі або через інший скрипт

    private float timeBtwShoots;
    private float rotZ;
    private Vector3 difference;
    private Vector2 _targetDirection;
    private SpriteRenderer spriteRenderer;

    private SpriteRenderer gunRender;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Player>();
            if (player == null)
            {
                Debug.LogError("Player reference not found!");
                enabled = false; // Вимикаємо скрипт, якщо гравець не знайдений
                return;
            }

            gunRender = GetComponent<SpriteRenderer>();
        if (gunRender == null)
        {
            Debug.LogError("SpriteRenderer component is not found on the gun object!");
        }
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
        timeBtwShoots = startTimeBtwShots;
    }

    void Update()
    {
        if (player == null) return;

        // Розрахунок напрямку до гравця
        difference = player.transform.position - transform.position;
        _targetDirection = difference.normalized;

        // Обертання пушки в сторону гравця
        rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

        // Віддзеркалення текстури
        // FlipTexture();

        // Логіка стрільби
        if (timeBtwShoots <= 0)
        {
            Shoot();
        }
        else
        {
            timeBtwShoots -= Time.deltaTime;
        }
    }

    public void Shoot()
    {
        Instantiate(bullet, shotPoint.position, Quaternion.Euler(0f, 0f, rotZ));
        timeBtwShoots = startTimeBtwShots;
    }

    private void FlipTexture()
    {
        if (_targetDirection == Vector2.zero) return;

        if (_targetDirection.x > 0)
        {
            spriteRenderer.flipY = false; // Дивиться вправо
        }
        else if (_targetDirection.x < 0)
        {
            spriteRenderer.flipY = true; // Дивиться вліво
        }

    }
}