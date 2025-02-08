using UnityEngine;

public class SystemManager : MonoBehaviour
{
    public ChildBehavior[] children; // Tablica referencji do dzieci
    public GameObject gameOverScreen;

    void Update()
    {
        CheckGameOver();
    }

    void CheckGameOver()
    {
        foreach (var child in children)
        {
            if (child.spriteRenderer.color == Color.red && child.impatientTimeout <= 0)
            {
                GameOver();
                return;
            }
        }
    }

    public void GameOver()
    {
        Debug.LogError("Przegra³eœ! Dzieci s¹ niecierpliwe.");
        gameOverScreen.SetActive(true);
        Time.timeScale = 0f; // Zatrzymujemy grê
    }
}