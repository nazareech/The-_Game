using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [Header("Налаштування предмета")]
    public Item itemData;
    public int amount = 1;
    public KeyCode pickupKey = KeyCode.F;

    [Header("Візуальні ефекти")]
    public GameObject pickupPrompt;
    public AudioClip pickupSound;

    private bool playerInRange;
    private AudioSource audioSource;
    private Inventory inventoryCache;

    private void Start()
    {
        // Шукаємо інвентар один раз при старті
        inventoryCache = FindFirstObjectByType<Inventory>();

        // Ініціалізація AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Вимкнути підказку на старті
        if (pickupPrompt != null)
        {
            pickupPrompt.SetActive(false);
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(pickupKey))
        {
            AttemptPickup();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            if (pickupPrompt != null)
            {
                pickupPrompt.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;

            if (pickupPrompt != null)
            {
                pickupPrompt.SetActive(false);
            }
        }
    }

    private void AttemptPickup()
    {
        if (inventoryCache == null)
        {
            Debug.LogError("Інвентар не знайдено!");
            return;
        }

        if (Inventory.IsInventoryOpen) return;

        // Додаємо предмет до інвентаря
        bool pickupSuccess = false;
        for (int i = 0; i < inventoryCache.maxCount; i++)
        {
            if (inventoryCache.items[i].id == 0) // Пустий слот
            {
                inventoryCache.AddItem(i, itemData, amount);
                pickupSuccess = true;
                break;
            }
            else if (inventoryCache.items[i].id == itemData.id &&
                    inventoryCache.items[i].count < inventoryCache.itemsInStack) // Стакування
            {
                inventoryCache.items[i].count += amount;
                pickupSuccess = true;
                break;
            }
        }

        if (pickupSuccess)
        {
            // Відтворюємо звук
            if (pickupSound != null)
            {
                audioSource.PlayOneShot(pickupSound);
            }

            // Вимкнути об'єкт
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;

            // Вимкнути підказку
            if (pickupPrompt != null)
            {
                pickupPrompt.SetActive(false);
            }

            // Знищити об'єкт після закінчення звуку
            Destroy(gameObject, pickupSound != null ? pickupSound.length : 0.1f);
        }
        else
        {
            Debug.Log("Інвентар заповнений!");
        }
    }
}