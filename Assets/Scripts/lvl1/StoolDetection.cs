using UnityEngine;

public class StoolDetection : MonoBehaviour
{
    public Transform climbPoint; // Point where the player climbs up
    private bool stoolInPlace = false;
    public GameObject ArmChair;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Stool"))
        {
            stoolInPlace = true;
            Debug.Log("Stool is in place!");
            ObjectPush.TurnStoolColliderOff();
            ArmChair.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Stool"))
        {
            stoolInPlace = false;
            Debug.Log("Stool is no longer in place.");
        }
    }

}
