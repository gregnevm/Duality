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
        // ќтримуЇмо Rigidbody гравц€
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
    }

    private void FixedUpdate()
    {
        // ќбчислюЇмо вектор руху з введенн€ гравц€
        movementDirection.x = Input.GetAxisRaw("Horizontal");
        movementDirection.y = Input.GetAxisRaw("Vertical");

        // ќбчислюЇмо вектор нормал≥зованого напр€мку руху
        Vector2 normalizedDirection = movementDirection.normalized;

        // ќбчислюЇмо кут м≥ж напр€мком руху та напр€мком вправо
        float angle = Mathf.Atan2(normalizedDirection.y, normalizedDirection.x) * Mathf.Rad2Deg;

        // ќбчислюЇмо вектор руху в залежност≥ в≥д кута повороту камери
        Vector3 movement = Quaternion.Euler(0f, Camera.main.transform.eulerAngles.y, 0f) * new Vector3(normalizedDirection.x, 0f, normalizedDirection.y);

        // «м≥щуЇмо Rigidbody гравц€ в напр€мку вектора руху з вказаною швидк≥стю
        rb.velocity = movement * movementSpeed;
    }

    public void Move(Vector2 direction)
    {
        // Ќе використовуЇтьс€, оск≥льки рух в≥дбуваЇтьс€ через Rigidbody
    }

    public void StopMoving()
    {
        // «упин€Їмо рух гравц€
        rb.velocity = Vector3.zero;
    }
}
