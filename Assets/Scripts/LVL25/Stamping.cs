using TMPro;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System.Collections;
using UnityEngine.SceneManagement;

public class Stamping : MonoBehaviour
{
    public GameObject StampPrefab;
    public GameObject Paper;
    public Transform hand; // Reference to the hand object
    public Transform stampPlace; // Reference to the stamp place object
    public TMP_Text scoreText; // Reference to the TextMeshPro UI text
    public float tolerance = 0.5f; // Tolerance for alignment
    private int score = 0;
    private float Distance;
    public EventReference StampReferance;
    private EventInstance StampInstance;
    public Animator Anim;
    private bool stamping = false;

    private void Start()
    {
        Anim = GetComponent<Animator>();
        StampInstance = RuntimeManager.CreateInstance(StampReferance);
    }
    void Update()
    {
        Distance = Mathf.Abs(hand.position.x - stampPlace.position.x);
        if (Input.GetKeyDown(KeyCode.Space) && !stamping)
        {
            Anim.SetTrigger("Stamp");
            HandleStamp();
            StampInstance.start();
        }
        if (score == 10)
        {
            SceneManager.LoadScene(5);
        }
    }

    void HandleStamp()
    {
        StartCoroutine(StampStamp());
        if (Distance<=tolerance)
        {
            score++;
            UpdateScoreText();
        }
        else
        {
            score--;
            UpdateScoreText();
        }
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    IEnumerator StampStamp()
    {
        stamping = true;
        yield return new WaitForSeconds(0.5f);
        GameObject newstamp = Instantiate(StampPrefab,new Vector3(hand.transform.position.x + 0.25f,stampPlace.transform.position.y,-0.2f),stampPlace.rotation);
        newstamp.transform.SetParent(Paper.transform);
        yield return new WaitForSeconds(1.5f);
        NewStampPlacement.Instance.GenerateRandomStampPlace();
        Destroy(newstamp);
        stamping = false;   
        yield return null;
    }
}
