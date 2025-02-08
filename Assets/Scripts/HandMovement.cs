using UnityEngine;

public class HandMovement : MonoBehaviour
{
    public float speed = 2f; // Speed of the hand movement
    public float minX = -3.5f; // Left boundary of the hand's movement
    public float maxX = 3.5f;  // Right boundary of the hand's movement
    private bool movingRight = true;

    void Update()
    {
        MoveHand();
    }

    void MoveHand()
    {
        if (movingRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            if (transform.position.x >= maxX)
            {
                movingRight = false;
            }
        }
        else
        {
            transform.Translate(-Vector2.right * speed * Time.deltaTime);
            if (transform.position.x <= minX)
            {
                movingRight = true;
            }
        }
    }
}
