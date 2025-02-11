using System.Collections;
using UnityEngine;
using TMPro;

public class StartingSequence : MonoBehaviour
{
    public GameObject BlacGround;
    private void Awake()
    {
        StartCoroutine(StartingSeq());
    }

    IEnumerator StartingSeq()
    {
        Time.timeScale = 0;
        BlacGround.SetActive(true);
        yield return new WaitForSecondsRealtime(1);
        
        yield return new WaitForSecondsRealtime(3);
        BlacGround.SetActive(false);
        
        Time.timeScale = 1;
    }
}
