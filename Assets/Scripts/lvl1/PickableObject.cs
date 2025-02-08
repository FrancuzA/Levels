using UnityEngine;

public class PickableObject : MonoBehaviour
{
    public GameObject EtoPick;
    public bool CanPickUp;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("player entered ");
            EtoPick.SetActive(true);
            CanPickUp = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            EtoPick.SetActive(false);
            CanPickUp = true;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && CanPickUp)
        {
            gameObject.SetActive(false);
        }
    }
}
