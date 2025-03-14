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


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;

    [SerializeField]
    private float _rotationSpeed = 300f;

    [SerializeField]
    private GameObject _bulletPrefab;

    [SerializeField]
    private Transform _firePoint;

    [SerializeField]
    private float _fireRate = 1f;

    private Rigidbody2D _rigidbody;
    private PlayerAwarenessController _playerAwarenessController;
    private Vector2 _targetDirection;
    private float _nextFireTime;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerAwarenessController = GetComponent<PlayerAwarenessController>();
    }

    void FixedUpdate()
    {
        UpdateTargetDirection();
        RotateTowardTarget();
        SetVelocity();

        if (_playerAwarenessController.AwareOfPlayer)
        {
            TryShoot();
        }
    }

    private void UpdateTargetDirection()
    {
        if (_playerAwarenessController.AwareOfPlayer)
        {
            _targetDirection = _playerAwarenessController.DirectionToPlayer;
        }
        else
        {
            _targetDirection = Vector2.zero;
        }
    }

    private void RotateTowardTarget()
    {
        if (_targetDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(_targetDirection.y, _targetDirection.x) * Mathf.Rad2Deg - 90f;
            Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.fixedDeltaTime);
        }
    }

    private void SetVelocity()
    {
        if (_targetDirection != Vector2.zero)
        {
            _rigidbody.velocity = transform.up * _speed;
        }
        else
        {
            _rigidbody.velocity = Vector2.zero;
        }
    }

    private void TryShoot()
    {
        if (Time.time >= _nextFireTime)
        {
            _nextFireTime = Time.time + 1f / _fireRate;
            Shoot();
        }
    }

    private void Shoot()
    {
        if (_bulletPrefab != null && _firePoint != null)
        {
            Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
        }
    }
}
