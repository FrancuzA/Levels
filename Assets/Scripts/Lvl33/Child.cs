using UnityEngine;
using TMPro;

public class Child : MonoBehaviour
{
    public float stressLevel = 0f; // Poziom zdenerwowania
    public float maxStressLevel = 100f; // Maksymalny poziom zdenerwowania
    public float stressIncreaseRate = 1f; // Tempo wzrostu zdenerwowania

    public bool hasReceivedCake = false; // Czy dziecko otrzyma�o tort
    public TMP_Text stressText; // Tekst TextMeshPro wy�wietlaj�cy poziom zdenerwowania

    void Update()
    {
        if (!hasReceivedCake)
        {
            // Zwi�kszanie poziomu zdenerwowania z czasem
            stressLevel += stressIncreaseRate * Time.deltaTime;

            // Ograniczenie maksymalnego poziomu zdenerwowania
            stressLevel = Mathf.Min(stressLevel, maxStressLevel);

            // Wy�wietlanie poziomu zdenerwowania za pomoc� TextMeshPro
            if (stressText != null)
            {
                stressText.text = "Stress: " + Mathf.RoundToInt(stressLevel) + "%";
            }

            // Je�li dziecko osi�gnie maksymalny poziom zdenerwowania
            if (stressLevel >= maxStressLevel)
            {
                Debug.Log("Dziecko jest zdenerwowane! Przegra�e�!");
                GameOver();
            }
        }
    }

    // Metoda wywo�ywana, gdy gracz dostarczy tort
    public void DeliverCake()
    {
        if (!hasReceivedCake)
        {
            hasReceivedCake = true;
            Debug.Log("Tort dostarczony! Dziecko jest szcz�liwe!");
            stressLevel = 0f; // Reset poziomu zdenerwowania
        }
    }

    // Koniec gry
    private void GameOver()
    {
        // Tutaj mo�esz doda� logik� ko�cow�, np. wy�wietlenie ekranu przegranej
        enabled = false; // Wy��cz skrypt dziecka
    }
}