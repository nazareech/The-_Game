using UnityEngine;



public class Sniper : _Weapon
{
    //[Header("Sniper Specific Settings")]
    //[SerializeField] private float angeleSleeveRunout = 5f;   // Кут розбігу гільз у градусах
    //[SerializeField] private GameObject sleeve;               // Префаб гільзи
    //[SerializeField] private Transform sleevePosition;        // Позиція виліту гільз

    protected override void Shoot()
    {
        base.Shoot(); // Викликаємо базовий метод Shoot
        
    }
}

