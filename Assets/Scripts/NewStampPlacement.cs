using UnityEngine;

public class NewStampPlacement : MonoBehaviour
{
    public float minX = -0.3f; // Left boundary of the paper
    public float maxX = 0.3f;  // Right boundary of the paper
    public Transform stampPlace; // Reference to the stamp place object
    public static NewStampPlacement Instance;

    void Start()
    {
        GenerateRandomStampPlace();
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public void GenerateRandomStampPlace()
    {
        if (stampPlace != null)
        {
            float randomX = Random.Range(minX, maxX);
            stampPlace.position = new Vector3(randomX, stampPlace.position.y, stampPlace.position.z);
        }
    }
}
