using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro; // Dodajemy przestrzeñ nazw dla TextMeshPro
using UnityEngine.SceneManagement; // Dodajemy przestrzeñ nazw do zarz¹dzania scenami

public class Teacher : MonoBehaviour
{
    public static Teacher Instance; // Singleton dla nauczycielki

    [Header("Teacher Settings")]
    public float checkIntervalMin = 3f; // Minimalny czas miêdzy kontrolami
    public float checkIntervalMax = 7f; // Maksymalny czas miêdzy kontrolami
    public float signalDuration = 1f; // Czas trwania sygna³u (np. mruganie oczami)

    [Header("Cheating System")]
    public Slider cheatingProgressSlider; // Pasek postêpu œci¹gania
    public Image warningCounter; // UI do wyœwietlania licznika ostrze¿eñ
    public Sprite[] warningSprites; // Tablica sprite'ów dla licznika ostrze¿eñ
    public TextMeshProUGUI gameOverText; // Tekst informuj¹cy o przegranej (TextMeshPro)

    private bool isChecking = false; // Czy nauczycielka aktualnie sprawdza?
    private int warnings = 0; // Licznik ostrze¿eñ
    private bool playerIsCheating = false; // Czy gracz aktualnie œci¹ga
    private float timeUntilNextCheck = 0f; // Timer do nastêpnego sprawdzenia
    private Animator teacherAnim; // Animator dla nauczycielki

    private void Awake()
    {
        teacherAnim = GetComponent<Animator>();
        Instance = this; // Inicjalizacja singletona
        if (gameOverText != null)
        {
            gameOverText.enabled = false; // Wy³¹cz tekst przegranej na pocz¹tku
        }
    }

    private void Update()
    {
        // Jeœli nauczycielka nie sprawdza, powróæ do stanu "Back"
        if (!isChecking)
        {
            teacherAnim.SetTrigger("Back");
        }

        // Zmniejsz timer do nastêpnego sprawdzenia
        if (timeUntilNextCheck > 0)
        {
            timeUntilNextCheck -= Time.deltaTime;
        }
        else
        {
            // Wywo³aj sprawdzenie
            StartCoroutine(CheckForCheating());
            timeUntilNextCheck = Random.Range(checkIntervalMin, checkIntervalMax);
        }

        // Aktualizacja paska postêpu œci¹gania
        if (playerIsCheating && !isChecking)
        {
            cheatingProgressSlider.value += Time.deltaTime * 0.04f; // Nape³nianie Slider'a (wolno)
            cheatingProgressSlider.value = Mathf.Clamp(cheatingProgressSlider.value, 0f, cheatingProgressSlider.maxValue); // Ogranicz wartoœæ Slider'a
        }

        // SprawdŸ, czy gracz osi¹gn¹³ maksymalny poziom œci¹gania
        if (cheatingProgressSlider.value >= cheatingProgressSlider.maxValue)
        {
            WinGame(); // Wyœwietl napis "WYGRA£EŒ!" i przejdŸ do nastêpnej sceny
        }
    }

    IEnumerator CheckForCheating()
    {
        // Sygna³ (np. mrugniêcie oczami lub inny efekt wizualny)
         // Uruchom animacjê sprawdzania
        GetComponent<SpriteRenderer>().color = Color.yellow; // Na przyk³ad: zmiana koloru na ¿ó³ty
        yield return new WaitForSeconds(signalDuration);
        GetComponent<SpriteRenderer>().color = Color.white;
        isChecking = true;
        teacherAnim.SetTrigger("Check");// Przywrócenie oryginalnego koloru

        // SprawdŸ, czy gracz œci¹ga
        if (playerIsCheating)
        {
            warnings++;
            Debug.Log($"Wykryto œci¹ganie! Ostrze¿enie #{warnings}");
            UpdateWarningCounter();

            if (warnings >= 3)
            {
                GameOver(); // Wyœwietl napis "PRZEGRA£EŒ"
            }
        }

        // Wróæ do stanu pocz¹tkowego
        isChecking = false;
    }

    public bool IsChecking => isChecking;

    public void SetPlayerIsCheating(bool isCheating)
    {
        playerIsCheating = isCheating;
    }

    // Aktualizacja licznika ostrze¿eñ w UI
    private void UpdateWarningCounter()
    {
        if (warningSprites.Length > warnings)
        {
            warningCounter.sprite = warningSprites[warnings];
        }
    }

    // Logika przegranej
    private void GameOver()
    {
        if (gameOverText != null)
        {
            gameOverText.enabled = true; // Wyœwietl tekst przegranej
            gameOverText.text = "PRZEGRA£EŒ!"; // Ustaw treœæ tekstu
        }

        Debug.Log("Przegra³eœ! Dosta³eœ 3 ostrze¿eñ.");

        // Restartuj scenê po 3 sekundach
        Invoke("RestartScene", 3f);
    }

    // Metoda do restartowania sceny
    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Za³aduj ponownie aktualn¹ scenê
    }

    // Logika wygranej
    private void WinGame()
    {
        Debug.Log("Gracz zdoby³ ca³¹ wiedzê!");

        // Wyœwietl napis "WYGRA£EŒ!"
        if (gameOverText != null)
        {
            gameOverText.enabled = true;
            gameOverText.text = "WYGRA£EŒ!";
        }

        // PrzejdŸ do nastêpnej sceny po 2 sekundach
        Invoke("LoadNextScene", 2f);
    }

    // Metoda do ³adowania nastêpnej sceny
    private void LoadNextScene()
    {
        // Pobierz indeks aktualnej sceny
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Jeœli istnieje kolejna scena, za³aduj j¹
        if (currentSceneIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(currentSceneIndex + 1); // Za³aduj nastêpn¹ scenê
        }
        else
        {
            Debug.LogWarning("Brak nastêpnej sceny! Koniec gry.");
        }
    }
}