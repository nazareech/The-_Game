using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Minigun : _Weapon
{
    [Header("Minigun Specific Settings")]
    [SerializeField] private float spreadAngle = 5f;          // Кут розбігу патронів у градусах
    [SerializeField] private float maxHeat = 100f;            // Максимальна температура
    [SerializeField] private float heatPerShoot = 10f;        // Температура за постріл
    [SerializeField] private float coolingRate = 5f;          // Швидкість охолодження
    [SerializeField] private float overheatCooldown = 3f;     // Час охолодження після перегріву

    [SerializeField] private Slider heatSlider;               // Слайдер перегріву
    [SerializeField] private Image heatSliderFill;            // Компонент Image для заповнення слайдера
    [SerializeField] private Color normalColor = Color.red;   // Звичайний колір слайдера
    [SerializeField] private Color coolingColor = Color.blue; // Колір слайдера під час охолодження

    [SerializeField] GameObject temperatureSensor;
    [SerializeField] GameObject arrowSensor;
    [SerializeField] GameObject warningSensor;


    private new Animator gunAnim;

    private float currentHeat = 0f;         // Поточна температура
    private bool isOverheated = false;      // Чи перегрітий мініган
    private float overheatTimer = 0f;       // Таймер для охолодження

    protected override void Start()
    {
        base.Start();   
        gunAnim = GetComponent<Animator>();    
    }
    protected override void Update()
    {
        base.Update(); // Викликаємо базовий метод Update

        Sensors();  // Датчики температурии на шкалі перегріву

        gunAnim.SetBool("Overheat", isOverheated);    // Запускаємо анімацію перегріву

        if (heatSlider != null)
        {
            heatSlider.value = currentHeat / maxHeat; // Оновлюємо значення слайдера
        }

        if (isOverheated)
        {
            // Якщо мініган перегрітий, очікуємо, поки він охолоне
            overheatTimer -= Time.deltaTime;

            // Змінюємо колір слайдера на синій
            if (heatSliderFill != null)
            {
                heatSliderFill.color = coolingColor;
            }

            // Поступове зменшення слайдера
            if (heatSlider != null)
            {
                heatSlider.value = overheatTimer / overheatCooldown;
            }

            if (overheatTimer <= 0f)
            {
                isOverheated = false;
                currentHeat = 0f; // Скидаємо температуру після охолодження

                // Повертаємо звичайний колір слайдера
                if (heatSliderFill != null)
                {
                    heatSliderFill.color = normalColor;
                }
            }
        }
        else
        {
            // Охолодження, якщо не стріляємо
            if (!Input.GetMouseButton(0))
            {
                currentHeat -= coolingRate * Time.deltaTime;
                currentHeat = Mathf.Max(currentHeat, 0f); // Не даємо температурі опуститися нижче 0
            }

            // Повертаємо звичайний колір слайдера, якщо не перегріто
            if (heatSliderFill != null)
            {
                heatSliderFill.color = normalColor;
            }
        }
    }


    protected override void Shoot()
    {
        if (currentHeat != maxHeat) 
        {
            base.ShootAnim();
        }

        if (isOverheated) return; // Якщо перегріто, не стріляємо

        // Отримуємо поточний кут стрільби
        float currentAngle = Mathf.Atan2(shootPosition.up.y, shootPosition.up.x) * Mathf.Rad2Deg;

        // Додаємо випадкове відхилення в межах розбігу
        float randomSpread = Random.Range(-spreadAngle, spreadAngle);
        float newAngle = currentAngle + randomSpread;

        // Створюємо новий кут стрільби
        Quaternion spreadRotation = Quaternion.Euler(0, 0, newAngle);

        // Створюємо патрон з новим кутом
        Instantiate(bullet, shootPosition.position, spreadRotation);
        Debug.Log("'Press BaBah! Minigun shot with spread!'");

        // Збільшуємо температуру
        currentHeat += heatPerShoot;
        if (currentHeat >= maxHeat)
        {
            Overheat();
        }
        
    }

    private void Overheat()
    {
        isOverheated = true;
        overheatTimer = overheatCooldown;
        Debug.Log("Minigun overheated! Cooling down...");

        // Змінюємо колір слайдера на синій
        if (heatSliderFill != null)
        {
            heatSliderFill.color = coolingColor;
            
        }

       
    }

    // ДАтчики на шкалі перегріву
    private void Sensors()
    {
        if (currentHeat < 80f && !isOverheated) 
        {
            temperatureSensor.SetActive(true);
            arrowSensor.SetActive(false);
            warningSensor.SetActive(false);
        }
        else if(currentHeat >= 80f && !isOverheated)
        {
            temperatureSensor.SetActive(false);
            arrowSensor.SetActive(false);
            warningSensor.SetActive(true);

        }
        else if (currentHeat >= maxHeat)
        {
            temperatureSensor.SetActive(false);
            arrowSensor.SetActive(true);
            warningSensor.SetActive(false);
        }


    }
}

