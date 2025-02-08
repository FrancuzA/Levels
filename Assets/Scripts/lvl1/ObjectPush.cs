using System.Collections.Generic;
using UnityEngine;

public class ObjectPush : MonoBehaviour
{
    public float pushForce = 5f; // Force applied when pushing the stool
    private static Rigidbody2D rb;
    private bool isBeingPushed = false;
    public static Collider2D[] StoolCollider;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StoolCollider = GetComponents<Collider2D>();
        Debug.Log(StoolCollider[0].isTrigger);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isBeingPushed = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isBeingPushed = false;
            rb.linearVelocityX = 0;
        }
    }

    void Update()
    {
        if (isBeingPushed && Input.GetKey(KeyCode.D))
        {
            rb.AddForce(Vector2.right * pushForce, ForceMode2D.Force);
        }
        else if (isBeingPushed && Input.GetKey(KeyCode.A))
        {
            rb.AddForce(-Vector2.right * pushForce, ForceMode2D.Force);
        }
    }

    public static void TurnStoolColliderOff()
    {
        StoolCollider[0].enabled = false;
        rb.constraints = RigidbodyConstraints2D.FreezePositionX;
    }

}
