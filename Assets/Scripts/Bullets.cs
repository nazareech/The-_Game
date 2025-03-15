using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bullets : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float deathTime;

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
        if (collision.gameObject.tag == "Wall")
        {
            Death();
        }
    }

    void Death()
    {
        Destroy(gameObject);
    }
}
