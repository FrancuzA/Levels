using UnityEngine;

public class DziadController : MonoBehaviour
{
    private Rigidbody2D dziadRigidbody;
    private BoxCollider2D dziadCollider;
    private Vector2 movementDirection;

    public float initialMovementSpeed = 1f;
    public float minMovementSpeed = 0.3f;
    [SerializeField] private float speedDecayRate = 0.01f;
    private bool isMoving;

    void Start()
    {
        dziadRigidbody = GetComponent<Rigidbody2D>();
        dziadCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        ProcessInputs();
        if (initialMovementSpeed > minMovementSpeed)
        {
            initialMovementSpeed -= speedDecayRate * Time.deltaTime;
            initialMovementSpeed = Mathf.Max(initialMovementSpeed, minMovementSpeed); // Upewniamy się, że nie spadnie poniżej minMovementSpeed
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    public void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        movementDirection = new Vector2(moveX, moveY).normalized;
    }

    public void MovePlayer()
    {
        // Jeśli initialMovementSpeed wynosi 0, nie ustawiamy velocity
        if (initialMovementSpeed <= 0f)
        {
            dziadRigidbody.linearVelocity = Vector2.zero; // Zatrzymaj gracza
            return;
        }

        // W przeciwnym razie ustaw velocity zgodnie z movementSpeed
        dziadRigidbody.linearVelocity = new Vector2(movementDirection.x * initialMovementSpeed, movementDirection.y * initialMovementSpeed);
    }
}