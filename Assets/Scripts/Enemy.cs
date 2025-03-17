using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 10; // Здоров'я ворога
    
    // Віднімання здоров'я після влучання 
    public void TakeDamage(int damage)
    {
        health -= damage;  

        if (health < 0)
        {
            Die();
        }
    }

    // Знищення ворога
    void Die()
    {
        Destroy(gameObject);
    }
}
