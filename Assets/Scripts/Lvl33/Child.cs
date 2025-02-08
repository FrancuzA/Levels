using UnityEngine;
using TMPro;

public class Child : MonoBehaviour
{
    public float stressLevel = 0f; // Poziom zdenerwowania
    public float maxStressLevel = 100f; // Maksymalny poziom zdenerwowania
    public float stressIncreaseRate = 1f; // Tempo wzrostu zdenerwowania

    public bool hasReceivedCake = false; // Czy dziecko otrzyma³o tort
    public TMP_Text stressText; // Tekst TextMeshPro wyœwietlaj¹cy poziom zdenerwowania

    void Update()
    {
        if (!hasReceivedCake)
        {
            // Zwiêkszanie poziomu zdenerwowania z czasem
            stressLevel += stressIncreaseRate * Time.deltaTime;

            // Ograniczenie maksymalnego poziomu zdenerwowania
            stressLevel = Mathf.Min(stressLevel, maxStressLevel);

            // Wyœwietlanie poziomu zdenerwowania za pomoc¹ TextMeshPro
            if (stressText != null)
            {
                stressText.text = "Stress: " + Mathf.RoundToInt(stressLevel) + "%";
            }

            // Jeœli dziecko osi¹gnie maksymalny poziom zdenerwowania
            if (stressLevel >= maxStressLevel)
            {
                Debug.Log("Dziecko jest zdenerwowane! Przegra³eœ!");
                GameOver();
            }
        }
    }

    // Metoda wywo³ywana, gdy gracz dostarczy tort
    public void DeliverCake()
    {
        if (!hasReceivedCake)
        {
            hasReceivedCake = true;
            Debug.Log("Tort dostarczony! Dziecko jest szczêœliwe!");
            stressLevel = 0f; // Reset poziomu zdenerwowania
        }
    }

    // Koniec gry
    private void GameOver()
    {
        // Tutaj mo¿esz dodaæ logikê koñcow¹, np. wyœwietlenie ekranu przegranej
        enabled = false; // Wy³¹cz skrypt dziecka
    }
}