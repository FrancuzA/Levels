using UnityEngine;
using TMPro;

using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public float levelTime = 90f; // 1,5 minuty
    private float currentTime;
    public TextMeshProUGUI timerText; // U¿ywamy TextMeshPro
    public GameObject gameOverScreen;
    public SystemManager systemManager;

    void Start()
    {
        currentTime = levelTime;
    }

    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            timerText.text = "Czas: " + Mathf.Ceil(currentTime).ToString();
        }
        else
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Debug.LogError("Przegra³eœ! Up³yn¹³ limit czasu.");
        gameOverScreen.SetActive(true);
        Time.timeScale = 0f; // Zatrzymujemy grê
        systemManager.GameOver(); // Wywo³anie metody przegranej w GameManager
    }
}