using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bullets : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float deathTime;

    public GameObject effect;

    public int damage = 5;  // Урон який наносить пуля

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke(nameof(Death), deathTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(effect, transform.position, Quaternion.identity);
        if (collision.gameObject.tag == "Wall")
        {
            Death();
        }
        
        
        // Влучання пулі в противника
        Enemy enemy = collision.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        Death();
    }

    void Death()
    {
        Destroy(gameObject);
    }

}
