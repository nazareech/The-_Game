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

    private float currentHeat = 0f;         // Поточна температура
    private bool isOverheated = false;      // Чи перегрітий мініган
    private float overheatTimer = 0f;       // Таймер для охолодження

    protected override void Update()
    {
        base.Update(); // Викликаємо базовий метод Update

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
        if (isOverheated) return; // Якщо перегріто, не стріляємо

        base.Shoot(); // Викликаємо базовий метод Shoot

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
}

