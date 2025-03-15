using UnityEngine;

public class Minigun : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 5f;  // �������� ��������
    [SerializeField] float spreadAngle = 5f;    // ��� ������ ������� � ��������

    [SerializeField] GameObject bullet;
    [SerializeField] Transform shootPosition;   // ������� ����� ����

    [SerializeField] GameObject Fire;           // ��'��� ��� ������� �����
    private Animator fireAnim;
    private Animator gunAnim;                   // Animator ��� ������
    private SpriteRenderer fireRender;

    public float fireRate = 0.2f;              // �������� �� ���������
    private float nextFireTime = 0f;

    public float offset;                       // ���������� ��� ��� �������� ��������

    private SpriteRenderer gunRender;          // ��� ������������ �������

    void Start()
    {
        // �������� ��������� Animator � ��������� ��'����
        gunAnim = GetComponent<Animator>();
        if (Fire != null)
        {
            fireAnim = Fire.GetComponent<Animator>();
            fireRender = Fire.GetComponent<SpriteRenderer>();
        }
        else
        {
            Debug.LogError("Fire object is not assigned!");
        }

        if (gunAnim == null)
        {
            Debug.LogError("Animator component is not assigned!");
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
            nextFireTime = Time.time + fireRate;
            Shoot();
            gunAnim.SetTrigger("Shoot");
            fireAnim.SetTrigger("Shoot");
        }

        GunRotation();
    }

    void GunRotation()
    {
        // �������� ������ �� �������� ������� �� �������� ������
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize(); // ���������� ������

        // ���������� ��� ��������
        float rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        // ֳ������ ������� � ����������� offset
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, rotateZ + offset);

        // ������� �������
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);


        // ������������ �������
        if (difference.x < 0)
        {
            gunRender.flipY = true;
        }
        else
        {
            gunRender.flipY = false;
        }
    }

    void Shoot()
    {
        // �������� �������� ��� �������
        float currentAngle = Mathf.Atan2(shootPosition.up.y, shootPosition.up.x) * Mathf.Rad2Deg;

        // ������ ��������� ��������� � ����� ������
        float randomSpread = Random.Range(-spreadAngle, spreadAngle);
        float newAngle = currentAngle + randomSpread;

        // ��������� ����� ��� �������
        Quaternion spreadRotation = Quaternion.Euler(0, 0, newAngle);

        // ��������� ������ � ����� �����
        Instantiate(bullet, shootPosition.position, spreadRotation);
        Debug.Log("'Press BaBah!'");
    }
}