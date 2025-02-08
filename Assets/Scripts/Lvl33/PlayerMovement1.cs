using UnityEngine;

public class PlayerControllerKids : MonoBehaviour
{
    public float movementSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movementDirection;
    public GameObject heldItem; // Obecnie trzymany zasób

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        ProcessInputs();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        movementDirection = new Vector2(moveX, moveY).normalized;
    }

    void MovePlayer()
    {
        rb.linearVelocity = new Vector2(movementDirection.x * movementSpeed, movementDirection.y * movementSpeed);
    }

    public void PickUpItem(GameObject item)
    {
        if (heldItem == null)
        {
            heldItem = Instantiate(item, transform.position, Quaternion.identity);
        }
    }

    public void DropItem()
    {
        if (heldItem != null)
        {
            Destroy(heldItem);
            heldItem = null;
        }
    }
}