using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D playerRigidbody;
    private BoxCollider2D playerCollider;
    private Vector2 movementDirection;
    [SerializeField] private float movementSpeed = 1f;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        ProcessInputs();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    public void ProcessInputs()
    {
        float MoveX = Input.GetAxisRaw("Horizontal");
        float MoveY = Input.GetAxisRaw("Vertical");

        movementDirection = new Vector2(MoveX, MoveY).normalized;
    }

    public void MovePlayer()
    {
        playerRigidbody.linearVelocity = new Vector2(movementDirection.x * movementSpeed, movementDirection.y * movementSpeed);
    }

}
