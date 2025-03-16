// using System.Collections;
// using  System.Collections.Generic;
// using UnityEngine;

// public class PlayerAwarenessController : MonoBehaviour
// {
//     public bool AwareOfPlayer{get ; private set;}
//     public Vector2 DirectionToPlayer{get; private set;}

//     [SerializeField]
//     private float _playerAwarenessDistance;

//     private Transform _player;

//      private void Awake()
//     {
//         _player = FindFirstObjectByType<Player>()?.transform;
//     }

//     void Update()
//     {
//         Vector2 enemyToPlayerVector = _player.position - transform.position;
//         DirectionToPlayer = enemyToPlayerVector.normalized;

//         if(enemyToPlayerVector.magnitude <= _playerAwarenessDistance){
//             AwareOfPlayer = true;
//         }
//         else{
//             AwareOfPlayer = false;
//         }
//     }
// }



// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class PlayerAwarenessController : MonoBehaviour
// {
//     public bool AwareOfPlayer { get; private set; }
//     public Vector2 DirectionToPlayer { get; private set; }

//     [SerializeField]
//     private float _playerAwarenessDistance;

//     private Transform _player;

//     private void Awake()
//     {
//         // Знаходимо гравця один раз на початку
//         _player = FindFirstObjectByType<Player>()?.transform;

//         // Якщо гравець не знайдений, виводимо попередження
//         if (_player == null)
//         {
//             Debug.LogWarning("Player not found!");
//         }
//     }

//     private void Update()
//     {
//         if (_player == null)
//         {
//             AwareOfPlayer = false;
//             return;
//         }

//         // Обчислюємо вектор до гравця
//         Vector2 enemyToPlayerVector = _player.position - transform.position;
//         DirectionToPlayer = enemyToPlayerVector.normalized;

//         // Перевіряємо, чи гравець у межах дистанції
//         AwareOfPlayer = enemyToPlayerVector.sqrMagnitude <= _playerAwarenessDistance * _playerAwarenessDistance;
//     }
// }




using System.Collections;
using UnityEngine;

public class PlayerAwarenessController : MonoBehaviour
{
    public bool AwareOfPlayer { get; private set; }
    public Vector2 DirectionToPlayer { get; private set; }

    [SerializeField]
    private float _playerAwarenessDistance;

    [SerializeField]
    private float _hysteresisThreshold = 0.1f;

    private Transform _player;

    private void Awake()
    {
        _player = FindFirstObjectByType<Player>()?.transform;
        if (_player == null)
        {
            Debug.LogWarning("Player not found!");
        }
    }

    private void Start()
    {
        StartCoroutine(CheckPlayerAwareness());
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
}