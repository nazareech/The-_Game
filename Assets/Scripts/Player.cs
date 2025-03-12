using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] float speed;
    Rigidbody2D rb;
    Vector2 moveVelocity;
    private Quaternion rotation;
    Animator anim;
    SpriteRenderer spr;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() 
    { 
        rb = GetComponent<Rigidbody2D>(); 
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();  
    }

    // Update is called once per frame
    void Update() {}

    private void FixedUpdate() { Move(); }

    void Move()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical"));

        PlayerRotation();

        if (moveInput != Vector2.zero)
        {
            anim.SetBool("run", true);
        }
        else 
        {
            anim.SetBool("run", false);
        }

        //ScalePalyer(moveInput.x); // Зміна напрямку руху

        moveVelocity = moveInput.normalized * speed;

        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }

    void PlayerRotation()
    {
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        // Перевертання пістолета
        if (dir.x < 0) // Якщо курсор ліворуч від пістолета
        {
            spr.flipX = true; // Перевертаємо спрайт по вертикалі
        }
        else if (dir.x > 0) // Якщо курсор праворуч від пістолета
        {
            spr.flipX = false; // Повертаємо спрайт у нормальний стан
        }
    }
    
}
