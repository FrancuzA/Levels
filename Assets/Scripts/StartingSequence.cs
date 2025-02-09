using System.Collections;
using UnityEngine;
using TMPro;
using UnityEditor.Experimental.GraphView;

public class StartingSequence : MonoBehaviour
{
    public GameObject BlacGround;
    public GameObject Tekst;
    public GameObject LvlText;
    private void Awake()
    {
        StartCoroutine(StartingSeq());
    }

    IEnumerator StartingSeq()
    {
        Time.timeScale = 0;
        BlacGround.SetActive(true);
        yield return new WaitForSecondsRealtime(1);
        LvlText.SetActive(true);
        yield return new WaitForSecondsRealtime(2);
        Tekst.SetActive(true);
        yield return new WaitForSecondsRealtime(3);
        BlacGround.SetActive(false);
        Tekst.SetActive(false);
        LvlText.SetActive(false);
        Time.timeScale = 1;
    }
}
