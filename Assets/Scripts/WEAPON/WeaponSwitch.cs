using UnityEngine;
using UnityEngine.UI;

public class WeaponSwitch : MonoBehaviour
{
    [Header("Weapon Switch Settings")]
    public int weaponSwitch = 0;
    public int weaponOpen = 2;
    public bool minigunPickedUp = false;

    [Header("Time to Switch")]
    [SerializeField] float switchCooldown = 1f; // Час затримки для перемикання зброї
    private float lastSwitchTime = 0f; // Час останнього перемикання

    [Header("Overheat Slider")]
    [SerializeField] Slider heatSlider; // Слайдер перегріву мінігану

    private Animator anim;          // Animator для активної зброї

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        int currentWeapon = weaponSwitch;

        // Перевіряємо, чи минув час затримки
        if (Time.time >= lastSwitchTime + switchCooldown)
        {
            // Колесо мишки
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                if (weaponSwitch >= transform.childCount - weaponOpen)
                {
                    weaponSwitch = 0;
                }
                else
                {
                    weaponSwitch++;
                }

                lastSwitchTime = Time.time; // Оновлюємо час останнього перемикання
            }

            if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                if (weaponSwitch <= 0)
                {
                    weaponSwitch = transform.childCount - weaponOpen;
                }
                else
                {
                    weaponSwitch--;
                }

                lastSwitchTime = Time.time; // Оновлюємо час останнього перемикання
            }

            // Клавіатура
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                weaponSwitch = 0;
                lastSwitchTime = Time.time; // Оновлюємо час останнього перемикання
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
            {
                weaponSwitch = 1;
                lastSwitchTime = Time.time; // Оновлюємо час останнього перемикання
            }
            if (Input.GetKeyDown(KeyCode.Alpha3) && minigunPickedUp == true)
            {
                weaponSwitch = 2;
                lastSwitchTime = Time.time; // Оновлюємо час останнього перемикання
            }
        }

        if (currentWeapon != weaponSwitch)
        {
            SelectWeapon();
        }

    }

    void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == weaponSwitch)
            {
                // Отримуємо Animator для активної зброї
                anim = weapon.GetComponent<Animator>();

                // Активуємо обрану зброю
                weapon.gameObject.SetActive(true);

                // Запускаємо анімацію діставання
                if (anim != null)
                {
                    anim.SetTrigger("TakeOut");
                }
                else
                {
                    Debug.LogWarning("Animator component not found on the selected weapon!");
                }

                // Включаємо або вимикаємо слайдер перегріву в залежності від активної зброї
                if (heatSlider != null)
                {
                    heatSlider.gameObject.SetActive(weapon.CompareTag("Minigun")); // Припустимо, що мініган має тег "Minigun"
                }
            }
            else
            {
                // Вимикаємо інші зброї
                weapon.gameObject.SetActive(false);
            }
            i++;
        }

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "MinigunPickedUp")
        {
            weaponOpen -= 1;
            minigunPickedUp = true;

            Destroy(collision.gameObject);
            weaponSwitch = 2;
            SelectWeapon();
        }
    }
}
