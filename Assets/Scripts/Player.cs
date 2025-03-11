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

        if (moveInput != Vector2.zero)
        {
            anim.SetBool("run", true);
        }
        else 
        {
            anim.SetBool("run", false);
        }

        ScalePalyer(moveInput.x); // Зміна напрямку руху

        moveVelocity = moveInput.normalized * speed;

        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }

    void ScalePalyer(float x)
    {
        if (x == 1)
        {
           spr.flipX = false;
        }
        else if (x == -1)
        {
           spr.flipX = true;

        }
    }
}
