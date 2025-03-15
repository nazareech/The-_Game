using UnityEngine;

public class Sniper : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 5f; // �������� ��������
    [SerializeField] GameObject bullet;        // ������ ���
    [SerializeField] Transform shootPosition;  // ������� ����� ����

    [SerializeField] GameObject Fire;          // ��'��� ��� ������� �����
    private Animator fireAnim;
    private Animator gunAnim;                 // Animator ��� ���� (����� private)

    public float fireRate = 0.2f;              // �������� �� ���������
    private float nextFireTime = 0f;

    private SpriteRenderer gunRender;          // ��� ������������ �������

    void Start()
    {
        // �������� ��������� Animator � ��������� ��'����
        gunAnim = GetComponent<Animator>();
        if (gunAnim == null)
        {
            Debug.LogError("Animator component not found on the gun object!");
        }

        if (Fire != null)
        {
            fireAnim = Fire.GetComponent<Animator>();
        }
        else
        {
            Debug.LogError("Fire object is not assigned!");
        }

        gunRender = GetComponent<SpriteRenderer>();
        if (gunRender == null)
        {
            Debug.LogError("SpriteRenderer component is not found on the gun object!");
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate; // ��������� ��� ���������� �������
            Shoot();
            gunAnim.SetTrigger("Shoot");    // ��������� ������� �������
            fireAnim.SetTrigger("Shoot");   // ��������� ������� �����
        }

        GunRotation();
    }

    void GunRotation()
    {
        // �������� ������ �� �������� ������� �� �������� ����
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        dir.Normalize(); // ���������� ������

        // ���������� ��� ��������
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        // ��������� ��������� ��� ��������
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);

        // ������� �������
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // ������������ �������
        if (dir.x < 0) // ���� ������ ������ �� ����
        {
            gunRender.flipY = true; // ����������� ������ �� ��������
        }
        else // ���� ������ �������� �� ����
        {
            gunRender.flipY = false; // ��������� ������ � ���������� ����
        }
    }

    void Shoot()
    {
        // �������� �������� ��� �������
        float currentAngle = Mathf.Atan2(shootPosition.up.y, shootPosition.up.x) * Mathf.Rad2Deg;

        // ��������� ��� ������� ��� ������
        Quaternion shootRotation = Quaternion.Euler(0, 0, currentAngle);

        // ��������� ������ � ������ �����
        Instantiate(bullet, shootPosition.position, shootRotation);
        Debug.Log("'BemB! Sniper shot!'");

        // ���������: ����� ������ ����� ������ ��� ���� �������
        // recoilAnim.SetTrigger("Shoot"); // ������� ������
        // audioSource.PlayOneShot(shootSound); // ���� �������
    }
}