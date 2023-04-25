using UnityEngine;

public class IsometricCharacterController : MonoBehaviour, IMoveable
{
    [SerializeField] private float movementSpeed = 5f;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject _playerBody;
    private Vector2 movementDirection = Vector2.zero;

    public float MovementSpeed { get { return movementSpeed; } }

    private void Start()
    {
        // �������� Rigidbody ������
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
    }

    private void FixedUpdate()
    {
        // ���������� ������ ���� � �������� ������
        movementDirection.x = Input.GetAxisRaw("Horizontal");
        movementDirection.y = Input.GetAxisRaw("Vertical");

        // ���������� ������ �������������� �������� ����
        Vector2 normalizedDirection = movementDirection.normalized;

        // ���������� ��� �� ��������� ���� �� ��������� ������
        float angle = Mathf.Atan2(normalizedDirection.y, normalizedDirection.x) * Mathf.Rad2Deg;

        // ���������� ������ ���� � ��������� �� ���� �������� ������
        Vector3 movement = Quaternion.Euler(0f, Camera.main.transform.eulerAngles.y, 0f) * new Vector3(normalizedDirection.x, 0f, normalizedDirection.y);

        // ������ Rigidbody ������ � �������� ������� ���� � �������� ��������
        rb.velocity = movement * movementSpeed;
    }

    public void Move(Vector2 direction)
    {
        // �� ���������������, ������� ��� ���������� ����� Rigidbody
    }

    public void StopMoving()
    {
        // ��������� ��� ������
        rb.velocity = Vector3.zero;
    }
}
