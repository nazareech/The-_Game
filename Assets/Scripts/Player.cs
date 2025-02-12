using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] float speed;
    Rigidbody2D rb;
    Vector2 moveVelocity;
    private Quaternion rotation;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical"));

        moveVelocity = moveInput.normalized * speed;

        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }
}
