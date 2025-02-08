using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D playerRigidbody;
    private BoxCollider2D playerCollider;
    private Vector2 movementDirection;
    [SerializeField] private float movementSpeed = 1f;
    public EventReference CrawlingReferance;
    private EventInstance CrawlingInstance;
    private bool isMoving;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();

        // Initialize the FMOD event instance
        CrawlingInstance = RuntimeManager.CreateInstance(CrawlingReferance);
    }

    private void Update()
    {
        ProcessInputs();
        CheckMovementState(); // Check if the player is moving and handle sound accordingly
    }

    private void FixedUpdate()
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

    private void CheckMovementState()
    {
        // Determine if the player is moving
        bool currentlyMoving = movementDirection != Vector2.zero;

        // Start the sound if the player starts moving
        if (currentlyMoving && !isMoving)
        {
            CrawlingInstance.start(); // Start the FMOD event
            isMoving = true;
        }
        // Stop the sound if the player stops moving
        else if (!currentlyMoving && isMoving)
        {
            CrawlingInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT); // Stop the FMOD event with fade-out
            isMoving = false;
        }
    }
}
