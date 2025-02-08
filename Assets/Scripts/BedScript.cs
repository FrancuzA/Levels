using UnityEngine;

public class BedScript : MonoBehaviour
{
    public GameObject BedHelp;
    public Collider2D sideCollider;
    public Collider2D HelpCollider;
    public int HitCounter;
    public static bool isInRange;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            BedHelp.SetActive(true);
        }
        isInRange = true;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            BedHelp.SetActive(false);
        }
        isInRange = false;
    }

    private void Update()
    {
        if (HitCounter < 10) {
         if (Input.GetKeyDown(KeyCode.Space) && isInRange)
         {
                HitCounter++;
         }
            
        }
        if (HitCounter == 10)
        {
            sideCollider.enabled = false;
            Destroy(BedHelp);
            HelpCollider.enabled = false;
        }
    }
}
