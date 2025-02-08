using TMPro;
using UnityEngine;

public class Stamping : MonoBehaviour
{
    public Transform hand; // Reference to the hand object
    public Transform stampPlace; // Reference to the stamp place object
    public TMP_Text scoreText; // Reference to the TextMeshPro UI text
    public float tolerance = 0.5f; // Tolerance for alignment
    private int score = 0;
    private float Distance;

    void Update()
    {
        Distance = Mathf.Abs(hand.position.x - stampPlace.position.x);
        Debug.Log(Distance);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            HandleStamp();
        }

    }

    void HandleStamp()
    {
        if (Distance<=tolerance)
        {
            Debug.Log("Correct Stamp! +1 Point");
            score++;
            UpdateScoreText();
        }
        else
        {
            Debug.Log("Missed Stamp! -1 Point");
            score--;
            UpdateScoreText();
        }

        // Generate a new paper with a new stamp place
        NewStampPlacement.Instance.GenerateRandomStampPlace();
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
}
