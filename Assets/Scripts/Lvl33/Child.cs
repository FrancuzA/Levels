using UnityEngine;
using UnityEngine.UI; // Dodajemy przestrzeñ nazw dla Image
using UnityEngine.SceneManagement;
using TMPro; // Dodajemy przestrzeñ nazw dla TextMeshPro

public class Child : MonoBehaviour
{
    public float stressLevel = 0f; // Poziom zdenerwowania
    public float maxStressLevel = 100f; // Maksymalny poziom zdenerwowania
    public float stressIncreaseRate = 1f; // Tempo wzrostu zdenerwowania
    public bool hasReceivedCake = false; // Czy dziecko otrzyma³o tort

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
            Debug.LogError("SpriteRenderer nie zosta³ znaleziony na obiekcie dziecka!");
        }

        // Upewnij siê, ¿e tekst przegranej jest ukryty na pocz¹tku
        if (loseMessageText != null)
        {
            loseMessageText.enabled = false;
        }
    }

    void Update()
    {
        if (!hasReceivedCake)
        {
            // Zwiêkszanie poziomu zdenerwowania z czasem
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

            // Jeœli dziecko osi¹gnie maksymalny poziom zdenerwowania
            if (stressLevel >= maxStressLevel)
            {
                Debug.LogError("Dziecko osi¹gnê³o maksymalny poziom zdenerwowania!");
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
        Debug.LogError("Dziecko jest zdenerwowane! Przegra³eœ!");

        // Wy³¹cz skrypt dziecka
        enabled = false;

        // Wyœwietl komunikat przegranej
        ShowLoseMessage();

        // Resetuj scenê po kilku sekundach
        Invoke("ReloadScene", 2f); // Za³aduj scenê ponownie po 2 sekundach
    }

    // Aktualizacja UI (sprity i teksty)
    private void UpdateUI()
    {
        if (uiChildImage != null)
        {
            // Zmieñ kolor sprita dziecka w UI
            Color newColor = Color.Lerp(Color.white, Color.red, stressLevel / maxStressLevel);
            uiChildImage.color = newColor;
        }

        if (uiStressText != null)
        {
            // Aktualizuj tekst procentowy wkurzenia
            uiStressText.text = "Wkurzenie: " + Mathf.RoundToInt(stressLevel) + "%";
        }
    }

    // Wyœwietlenie komunikatu przegranej za pomoc¹ TextMeshPro
    private void ShowLoseMessage()
    {
        if (loseMessageText != null)
        {
            loseMessageText.enabled = true; // Poka¿ tekst
            loseMessageText.text = "PRZEGRA£EŒ! Dzieci umar³y"; // Ustaw treœæ
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

        // Za³aduj bie¿¹c¹ scenê ponownie
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}