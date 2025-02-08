using UnityEngine;

public class StoolDetection : MonoBehaviour
{
    public GameObject ArmChair;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Stool"))
        {
            ObjectPush.TurnStoolColliderOff();
            ArmChair.GetComponent<PolygonCollider2D>().enabled = false;
        }
    }
}
