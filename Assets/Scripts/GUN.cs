using UnityEngine;

public class GUN : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    [SerializeField] float spreadAngle = 5f;    // ��� ������ ������� � ��������

    [SerializeField] GameObject bullet;
    [SerializeField] Transform shootpos;

    [SerializeField] GameObject gunObject; // ��'���, �� ����� ����'������ Animator
    private Animator anim;

    [SerializeField] GameObject Fire;
    private Animator fireAnim;
    private SpriteRenderer fireRender;

    public float fireRate = 0.2f; // �������� �� ���������
    private float nextFireTime = 0f;

    private SpriteRenderer gunRender;// ��� ������������ �������

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fireAnim = Fire.GetComponent<Animator>();
        fireRender = Fire.GetComponent<SpriteRenderer>();

        anim = gunObject.GetComponent<Animator>(); // �������� ��������� Animator
        gunRender = gunObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate; // ��������� ��� ���������� �������
            Shoot();
            anim.SetTrigger("Shoot"); // ��������� ������� �������
            fireAnim.SetTrigger("Shoot");
        } 

        GunRotation();
    }

    void GunRotation()
    {
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

        // ������������ ��������
        if (dir.x < 0 ) // ���� ������ ������ �� ��������
        {
            gunRender.flipY = true; // ����������� ������ �� ��������
        }
        else if (dir.x > 0) // ���� ������ �������� �� ��������
        {
             gunRender.flipY = false; // ��������� ������ � ���������� ����
        }
    }

    void Shoot()
    {
        // �������� �������� ��� �������
        float currentAngle = Mathf.Atan2(shootpos.up.y, shootpos.up.x) * Mathf.Rad2Deg;

        // ������ ��������� ��������� � ����� ������
        float randomSpread = Random.Range(-spreadAngle, spreadAngle);
        float newAngle = currentAngle + randomSpread;

        // ��������� ����� ��� �������
        Quaternion spreadRotation = Quaternion.Euler(0, 0, newAngle);

        // ��������� ������ � ����� �����
        Instantiate(bullet, shootpos.position, spreadRotation);
        Debug.Log("'Press BaBah!'");      
        
    }
}