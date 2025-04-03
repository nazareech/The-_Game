using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class SanitySystem : MonoBehaviour
{
    public static SanitySystem Instance;    // Екземпляр класу для виклику функцій

    [Header("Sanity Settings")]
    public Slider sanitySlider;
    public float maxSanity = 100f;      // максимальна Розсудливість
    public float sanityDecreasePerKill = 5f; // ЗМеншення за одне вбивство
    public float minSanityForEffects = 10f; // мінімальна Розсудливість заради ефекту

    [Header("Low Sanity Effects")]
    public PostProcessVolume lowSanityEffect; // Ефект пост-обробки
    public GameObject ghostPrefab;
    public float ghostSpawnInterval = 10f;  // інтервал спавну примар
    public float ghostSpawnDistance = 15f;  // дистанція спавну примар 
    public float ghostUniformScale = 1.0f; // Новий параметр для контролю розміру

    private float currentSanity;        // теперішній стан розсудку
    private float nextGhostSpawnTime;   // наступний час спавну привидів
    private int enemiesKilled = 0;      // Вбито ворогів


    private void Awake()
    {
        // Виправлення для DontDestroyOnLoad
        if (Instance == null)
        {
            Instance = this;
            transform.SetParent(null); // Від'єднуємо від батьківського об'єкта
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        currentSanity = maxSanity;
        UpdateSanityUI();
    }

    private void Update()
    {
        if (currentSanity < minSanityForEffects)
        {
            HandleLowSanityEffects();
        }
    }

    public void EnemyKilled()
    {
        enemiesKilled++;
        currentSanity -= sanityDecreasePerKill;
        currentSanity = Mathf.Clamp(currentSanity, 0, maxSanity);
        UpdateSanityUI();
    }

    private void UpdateSanityUI()
    {
        if (sanitySlider != null)
        {
            sanitySlider.value = currentSanity / maxSanity * 100;
        }
    }

    private void HandleLowSanityEffects()
    {
        // Включити ефекти пост-обробки
        if (lowSanityEffect != null && !lowSanityEffect.enabled)
        {
            lowSanityEffect.enabled = true;
        }

        // Спавнити привидів
        if (Time.time >= nextGhostSpawnTime && ghostPrefab != null)
        {
            SpawnGhost();
            nextGhostSpawnTime = Time.time + ghostSpawnInterval;
        }
    }

    private void SpawnGhost()
    {
        // 1. Генеруємо позицію
        Vector2 spawnDirection2D = Random.insideUnitCircle.normalized;
        Vector3 spawnPosition = Camera.main.transform.position +
                              new Vector3(spawnDirection2D.x, spawnDirection2D.y, 0) * ghostSpawnDistance;
        spawnPosition.z = 0; // Фіксуємо Z для 2D

        // 2. Створюємо привида
        if (ghostPrefab == null)
        {
            Debug.LogError("Ghost prefab is not assigned!");
            return;
        }

        GameObject ghost = Instantiate(ghostPrefab, spawnPosition, Quaternion.identity);
        Debug.Log("Ghost spawned at: " + spawnPosition);

        // 3. Налаштування рендерера
        SpriteRenderer spriteRenderer = ghost.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = true;
            spriteRenderer.sortingLayerName = "Ghosts"; // Назва вашого шару
            spriteRenderer.sortingOrder = 1;
        }
        else
        {
            Debug.LogError("No SpriteRenderer found on ghost prefab!");
        }
    }

    public float GetCurrentSanityPercent()
    {
        return currentSanity / maxSanity * 100f;
    }
}