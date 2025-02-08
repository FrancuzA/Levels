using UnityEngine;

public class Stamping : MonoBehaviour
{
    public Transform hand; // Reference to the hand object
    public Transform stampPlace; // Reference to the stamp place object
    public float tolerance = 0.1f; // Tolerance for alignment
    private int score = 0;

    void Update()
    {
        if (IsHandAboveStampPlace() && Input.GetKeyDown(KeyCode.Space))
        {
            HandleStamp();
        }
    }

    bool IsHandAboveStampPlace()
    {
        if (hand != null && stampPlace != null)
        {
            float distance = Mathf.Abs(hand.position.x - stampPlace.position.x);
            return distance <= tolerance;
        }
        return false;
    }

    void HandleStamp()
    {
        if (IsHandAboveStampPlace())
        {
            Debug.Log("Correct Stamp! +1 Point");
            score++;
        }
        else
        {
            Debug.Log("Missed Stamp! -1 Point");
            score--;
        }

        // Generate a new paper with a new stamp place
        NewStampPlacement.Instance.GenerateRandomStampPlace();
    }

    void OnGUI()
    {
        GUILayout.Label("Score: " + score);
    }
}
