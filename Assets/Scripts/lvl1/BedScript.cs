using UnityEngine;

public class BedScript : MonoBehaviour
{
    public GameObject BedHelp;
    public GameObject SideBars;
    public GameObject BrokenBars;
    public Collider2D sideCollider;
    public Collider2D HelpCollider;
    public int HitCounter;
    public static bool isInRange;
    public Animator Anim;
    private bool hasbroken = false;
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
        if (other.CompareTag("Player") && BedHelp!=null)
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
                Anim.SetTrigger("GetOut");
         }
            
        }
        if (HitCounter == 10 && !hasbroken)
        {
            hasbroken = true;
            HelpCollider.enabled = false;
             SideBars.SetActive(false);
            BrokenBars.SetActive(true);
            sideCollider.enabled = false;
            Destroy(BedHelp);
            Anim.SetTrigger("Broken");
        }
    }
}
