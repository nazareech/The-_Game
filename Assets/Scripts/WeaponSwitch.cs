using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{

    private Animator anim;          // Animator для активної зброї
    public int weaponSwitch = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        int currentWeapon = weaponSwitch;

        if(Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (weaponSwitch >= transform.childCount - 1)
            {
                weaponSwitch = 0;
            }
            else { weaponSwitch++; }
        }
        
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (weaponSwitch <= 0)
            {
                weaponSwitch = transform.childCount - 1;
            }
            else { weaponSwitch--; 
            }
        }

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            weaponSwitch = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            weaponSwitch = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
        {
            weaponSwitch = 2;
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
            }
            else
            {
                // Вимикаємо інші зброї
                weapon.gameObject.SetActive(false);
            }
            i++;
        }

    }
}
