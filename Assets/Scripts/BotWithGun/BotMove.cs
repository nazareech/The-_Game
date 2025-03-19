using System.Collections;
using UnityEngine;

public class SideViewUnitWithAwareness : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField]
    private float _speed = 3f; // Швидкість руху юніта

    [Header("Player Awareness Settings")]
    [SerializeField]
    private float _playerAwarenessDistance = 5f; // Дальність виявлення гравця
    [SerializeField]
    private float _hysteresisThreshold = 0.1f; // Поріг гістерезису для плавного переходу
    [SerializeField]
    private float _minDistanceToPlayer = 1f; // Мінімальна дистанція до гравця

    private Rigidbody2D _rigidbody;
    private Transform _player;
    private Vector2 _targetDirection;
    private Vector2 _lastKnownDirection;

    public bool AwareOfPlayer { get; private set; } // Чи виявлено гравця
    public Vector2 DirectionToPlayer { get; private set; } // Напрямок до гравця

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        // Пошук гравця
        _player = FindFirstObjectByType<Player>()?.transform;
        if (_player == null)
        {
            Debug.LogWarning("Player not found!");
        }
    }

    private void Start()
    {
        StartCoroutine(CheckPlayerAwareness()); // Запуск корутини для виявлення гравця
    }

    void FixedUpdate()
    {
        UpdateTargetDirection();
        FlipTexture(); // Віддзеркалення текстури залежно від напрямку
        SetVelocity();
    }

    private IEnumerator CheckPlayerAwareness()
    {
        while (true)
        {
            if (_player == null)
            {
                _player = FindFirstObjectByType<Player>()?.transform;
                if (_player == null)
                {
                    AwareOfPlayer = false;
                    yield return null;
                    continue;
                }
            }

            Vector2 enemyToPlayerVector = _player.position - transform.position;
            float distanceSquared = enemyToPlayerVector.sqrMagnitude;
            float awarenessDistanceSquared = _playerAwarenessDistance * _playerAwarenessDistance;

            if (enemyToPlayerVector != Vector2.zero)
            {
                DirectionToPlayer = enemyToPlayerVector.normalized;
            }
            else
            {
                DirectionToPlayer = Vector2.zero;
            }

            // Використання гістерезису для плавного переходу
            if (distanceSquared <= awarenessDistanceSquared - _hysteresisThreshold)
            {
                AwareOfPlayer = true;
            }
            else if (distanceSquared >= awarenessDistanceSquared + _hysteresisThreshold)
            {
                AwareOfPlayer = false;
            }

            yield return new WaitForSeconds(0.1f); // Перевіряйте кожні 0.1 секунди
        }
    }

    private void UpdateTargetDirection()
    {
        if (AwareOfPlayer)
        {
            Vector2 enemyToPlayerVector = _player.position - transform.position;
            float distanceToPlayer = enemyToPlayerVector.magnitude;

            // Якщо дистанція менша за мінімальну, зупинитися
            if (distanceToPlayer <= _minDistanceToPlayer)
            {
                _targetDirection = Vector2.zero;
            }
            else
            {
                _targetDirection = DirectionToPlayer;
            }
            _lastKnownDirection = _targetDirection;
        }
        else
        {
            _targetDirection = Vector2.zero; // Зупинитися, якщо гравець не виявлений
        }
    }

    private void FlipTexture()
    {
        if (_targetDirection == Vector2.zero) return;

        // Віддзеркалення текстури залежно від напрямку руху
        if (_targetDirection.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // Дивиться вправо
        }
        else if (_targetDirection.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Дивиться вліво
        }
    }


    private void SetVelocity()
{
    if (_targetDirection == Vector2.zero)
    {
        _rigidbody.linearVelocity = Vector2.zero; // Зупинитися
    }
    else
    {
        // Використовуйте AddForce для плавного руху
        _rigidbody.AddForce(_targetDirection * _speed, ForceMode2D.Force);
    }
}


    // Візуалізація радіусу виявлення в редакторі Unity
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _playerAwarenessDistance);

        // Візуалізація мінімальної дистанції
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _minDistanceToPlayer);
    }
}