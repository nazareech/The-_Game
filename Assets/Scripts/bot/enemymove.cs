// using System.Collections;
// using  System.Collections.Generic;
// using UnityEngine;


// public class NewEmptyCSharpScript : MonoBehaviour
// {
//      [SerializeField]
//      private float _speed;

//       [SerializeField]
//       private float _rotationSpeed;

//       private Rigidbody2D _rigidbody;
//       private PlayerAwarenessController _playerAwarenessController;
//       private Vector2 _targetDirection;


//     private void Awake()
//     {
         
//         _rigidbody = GetComponent<Rigidbody2D>();
//         _playerAwarenessController = GetComponent<PlayerAwarenessController>();

//         GetComponent<Collider2D>().isTrigger = true;
//     }

//     void    FixedUpdate()
//     {
//         UpdateTargetDirection();
//         RotateTowardTarget();
//         SetVelocity();
//     }

//     private void UpdateTargetDirection()
//     {
//         if(_playerAwarenessController.AwareOfPlayer){
//             _targetDirection = _playerAwarenessController.DirectionToPlayer;
//         }
//         else{
//             _targetDirection = Vector2.zero;
//         }
//     }



//     private void RotateTowardTarget()
//     {
//         if(_targetDirection == Vector2.zero){
//             return;
//         }

//         Quaternion targetRotation = Quaternion.LookRotation(transform.forward , _targetDirection);
//         Quaternion rotation = Quaternion.RotateTowards(transform.rotation , targetRotation, _rotationSpeed * Time.deltaTime);


//         _rigidbody.SetRotation(rotation);
//     } 

//     private void SetVelocity()
//     {
//         if(_targetDirection == Vector2.zero){
//             _rigidbody.velocity = Vector2.zero;
//         }
//         else{
//             _rigidbody.velocity = transform.up * _speed;
//         }

//     }

//     private void OnTriggerEnter2D(Collider2D other)
// {
//     if (other.CompareTag("Texture"))
//     {
//         Debug.Log("Bot passed through texture!");
       
//     }
// }

// }


// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class NewEmptyCSharpScript : MonoBehaviour
// {
//     [SerializeField]
//     private float _speed;

//     [SerializeField]
//     private float _rotationSpeed;

//     private Rigidbody2D _rigidbody;
//     private PlayerAwarenessController _playerAwarenessController;
//     private Vector2 _targetDirection;

//     private void Awake()
//     {
//         _rigidbody = GetComponent<Rigidbody2D>();
//         _playerAwarenessController = GetComponent<PlayerAwarenessController>();
//     }

//     void FixedUpdate()
//     {
//         UpdateTargetDirection();
//         RotateTowardTarget();
//         SetVelocity();
//     }

//     private void UpdateTargetDirection()
//     {
//         if (_playerAwarenessController.AwareOfPlayer)
//         {
//             _targetDirection = _playerAwarenessController.DirectionToPlayer;
//         }
//         else
//         {
//             _targetDirection = Vector2.zero;
//         }
//     }

//     private void RotateTowardTarget()
//     {
//         if (_targetDirection == Vector2.zero)
//         {
//             return;
//         }

//         float angle = Mathf.Atan2(_targetDirection.y, _targetDirection.x) * Mathf.Rad2Deg - 90f;
//         Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
//         Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

//         _rigidbody.MoveRotation(rotation);
//     }

//     private void SetVelocity()
//     {
//         if (_targetDirection == Vector2.zero)
//         {
//             _rigidbody.velocity = Vector2.zero;
//         }
//         else
//         {
//             _rigidbody.velocity = transform.up * _speed;
//         }
//     }
// }





// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class NewEmptyCSharpScript : MonoBehaviour
// {
//     [SerializeField]
//     private float _speed;

//     [SerializeField]
//     private float _rotationSpeed;

//     private Rigidbody2D _rigidbody;
//     private PlayerAwarenessController _playerAwarenessController;
//     private Vector2 _targetDirection;

//     private void Awake()
//     {
//         _rigidbody = GetComponent<Rigidbody2D>();
//         _playerAwarenessController = GetComponent<PlayerAwarenessController>();

        
//         GetComponent<Collider2D>().isTrigger = true;
//     }

//     void FixedUpdate()
//     {
//         UpdateTargetDirection();
//         RotateTowardTarget();
//         SetVelocity();
//     }

//     private void UpdateTargetDirection()
//     {
//         if (_playerAwarenessController.AwareOfPlayer)
//         {
//             _targetDirection = _playerAwarenessController.DirectionToPlayer;
//         }
//         else
//         {
//             _targetDirection = Vector2.zero;
//         }
//     }

//     private void RotateTowardTarget()
//     {
//         if (_targetDirection == Vector2.zero) return;

//         // Розрахунок кута напрямку
//         float targetAngle = Mathf.Atan2(_targetDirection.y, _targetDirection.x) * Mathf.Rad2Deg - 90f;

//         // Плавний поворот до цілі
//         float angle = Mathf.LerpAngle(_rigidbody.rotation, targetAngle, _rotationSpeed * Time.fixedDeltaTime);
//         _rigidbody.MoveRotation(angle);
//     }

//     private void SetVelocity()
//     {
//         if (_targetDirection == Vector2.zero)
//         {
//             _rigidbody.linearVelocity = Vector2.zero;
//         }
//         else
//         {
//             // Рух у напрямку _targetDirection
//             _rigidbody.linearVelocity = _targetDirection.normalized * _speed;
//         }
//     }

//     private void OnTriggerEnter2D(Collider2D other)
//     {
//         if (other.CompareTag("Texture"))
//         {
//             Debug.Log("Bot passed through texture!");
//             // Якщо потрібно — можна додати інші дії
//         }
//     }
// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEmptyCSharpScript : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _rotationSpeed;
    
    private Rigidbody2D _rigidbody;
    private PlayerAwarenessController _playerAwarenessController;
    private Vector2 _targetDirection;
    private Vector2 _lastKnownDirection;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerAwarenessController = GetComponent<PlayerAwarenessController>();
        GetComponent<Collider2D>().isTrigger = true;
    }

    void FixedUpdate()
    {
        UpdateTargetDirection();
        RotateTowardTarget();
        SetVelocity();
    }

    private void UpdateTargetDirection()
    {
        if (_playerAwarenessController.AwareOfPlayer)
        {
            _targetDirection = _playerAwarenessController.DirectionToPlayer;
            _lastKnownDirection = _targetDirection;
        }
        else
        {
            _targetDirection = _lastKnownDirection; // або Vector2.zero, якщо потрібно зупинитися
        }
    }

    private void RotateTowardTarget()
{
    if (_targetDirection == Vector2.zero) return;

    // Розрахунок кута напрямку
    float targetAngle = Mathf.Atan2(_targetDirection.y, _targetDirection.x) * Mathf.Rad2Deg - 90f;

    // Поточний кут об'єкта
    float currentAngle = _rigidbody.rotation;

    // Плавний поворот до цілі
    float newAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, _rotationSpeed * Time.fixedDeltaTime);
    _rigidbody.MoveRotation(newAngle);
}

    private void SetVelocity()
    {
        if (_targetDirection == Vector2.zero)
        {
            _rigidbody.linearVelocity = Vector2.zero;
        }
        else
        {
            _rigidbody.linearVelocity = _targetDirection.normalized * _speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            Debug.Log("Bot passed through texture!");
        }
    }
}