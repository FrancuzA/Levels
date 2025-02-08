using UnityEngine;
using UnityEngine.UI; // Dodajemy przestrze� nazw dla Image
using UnityEngine.SceneManagement;
using TMPro; // Dodajemy przestrze� nazw dla TextMeshPro

public class Child : MonoBehaviour
{
    public float stressLevel = 0f; // Poziom zdenerwowania
    public float maxStressLevel = 100f; // Maksymalny poziom zdenerwowania
    public float stressIncreaseRate = 1f; // Tempo wzrostu zdenerwowania
    public bool hasReceivedCake = false; // Czy dziecko otrzyma�o tort

    private SpriteRenderer spriteRenderer; // Komponent Sprite Renderer dziecka

    // Referencje do UI
    public Image uiChildImage; // Obrazek dziecka w UI
    public TMP_Text uiStressText; // Tekst procentowy wkurzenia w UI
    public TMP_Text loseMessageText; // Tekst przegranej

    void Start()
    {
        // Pobierz komponent SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer nie zosta� znaleziony na obiekcie dziecka!");
        }

        // Upewnij si�, �e tekst przegranej jest ukryty na pocz�tku
        if (loseMessageText != null)
        {
            loseMessageText.enabled = false;
        }
    }

    void Update()
    {
        if (!hasReceivedCake)
        {
            // Zwi�kszanie poziomu zdenerwowania z czasem
            stressLevel += stressIncreaseRate * Time.deltaTime;

            // Ograniczenie maksymalnego poziomu zdenerwowania
            stressLevel = Mathf.Min(stressLevel, maxStressLevel);

            // Zmiana koloru sprita dziecka w grze
            if (spriteRenderer != null)
            {
                Color newColor = Color.Lerp(Color.white, Color.red, stressLevel / maxStressLevel);
                spriteRenderer.color = newColor;
            }

            // Aktualizacja UI
            UpdateUI();

            // Je�li dziecko osi�gnie maksymalny poziom zdenerwowania
            if (stressLevel >= maxStressLevel)
            {
                Debug.LogError("Dziecko osi�gn�o maksymalny poziom zdenerwowania!");
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

            // Zresetuj kolor sprita dziecka w grze
            if (spriteRenderer != null)
            {
                spriteRenderer.color = Color.white;
            }

            // Zresetuj UI
            UpdateUI();
        }
    }

    // Koniec gry - Przegrana
    private void GameOver()
    {
        Debug.LogError("Dziecko jest zdenerwowane! Przegra�e�!");

        // Wy��cz skrypt dziecka
        enabled = false;

        // Wy�wietl komunikat przegranej
        ShowLoseMessage();

        // Resetuj scen� po kilku sekundach
        Invoke("ReloadScene", 2f); // Za�aduj scen� ponownie po 2 sekundach
    }

    // Aktualizacja UI (sprity i teksty)
    private void UpdateUI()
    {
        if (uiChildImage != null)
        {
            // Zmie� kolor sprita dziecka w UI
            Color newColor = Color.Lerp(Color.white, Color.red, stressLevel / maxStressLevel);
            uiChildImage.color = newColor;
        }

        if (uiStressText != null)
        {
            // Aktualizuj tekst procentowy wkurzenia
            uiStressText.text = "Wkurzenie: " + Mathf.RoundToInt(stressLevel) + "%";
        }
    }

    // Wy�wietlenie komunikatu przegranej za pomoc� TextMeshPro
    private void ShowLoseMessage()
    {
        if (loseMessageText != null)
        {
            loseMessageText.enabled = true; // Poka� tekst
            loseMessageText.text = "PRZEGRA�E�! Dzieci umar�y"; // Ustaw tre��
        }
    }

    // Resetowanie sceny
    private void ReloadScene()
    {
        // Ukryj komunikat przegranej przed resetem sceny
        if (loseMessageText != null)
        {
            loseMessageText.enabled = false;
        }

        // Za�aduj bie��c� scen� ponownie
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}