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



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAwarenessController : MonoBehaviour
{
    public bool AwareOfPlayer { get; private set; }
    public Vector2 DirectionToPlayer { get; private set; }

    [SerializeField]
    private float _playerAwarenessDistance;

    private Transform _player;

    private void Awake()
    {
        // Знаходимо гравця один раз на початку
        _player = FindFirstObjectByType<Player>()?.transform;

        // Якщо гравець не знайдений, виводимо попередження
        if (_player == null)
        {
            Debug.LogWarning("Player not found!");
        }
    }

    private void Update()
    {
        if (_player == null)
        {
            AwareOfPlayer = false;
            return;
        }

        // Обчислюємо вектор до гравця
        Vector2 enemyToPlayerVector = _player.position - transform.position;
        DirectionToPlayer = enemyToPlayerVector.normalized;

        // Перевіряємо, чи гравець у межах дистанції
        AwareOfPlayer = enemyToPlayerVector.sqrMagnitude <= _playerAwarenessDistance * _playerAwarenessDistance;
    }
}