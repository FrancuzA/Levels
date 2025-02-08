using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro; // Dodajemy przestrze� nazw dla TextMeshPro
using UnityEngine.SceneManagement; // Dodajemy przestrze� nazw do zarz�dzania scenami

public class Teacher : MonoBehaviour
{
    public static Teacher Instance; // Singleton dla nauczycielki

    [Header("Teacher Settings")]
    public float checkIntervalMin = 3f; // Minimalny czas mi�dzy kontrolami
    public float checkIntervalMax = 7f; // Maksymalny czas mi�dzy kontrolami
    public float signalDuration = 1f; // Czas trwania sygna�u (np. mruganie oczami)

    [Header("Cheating System")]
    public Slider cheatingProgressSlider; // Pasek post�pu �ci�gania
    public Image warningCounter; // UI do wy�wietlania licznika ostrze�e�
    public Sprite[] warningSprites; // Tablica sprite'�w dla licznika ostrze�e�
    public TextMeshProUGUI gameOverText; // Tekst informuj�cy o przegranej (TextMeshPro)

    private bool isChecking = false; // Czy nauczycielka aktualnie sprawdza?
    private int warnings = 0; // Licznik ostrze�e�
    private bool playerIsCheating = false; // Czy gracz aktualnie �ci�ga
    private float timeUntilNextCheck = 0f; // Timer do nast�pnego sprawdzenia
    private Animator teacherAnim; // Animator dla nauczycielki

    private void Awake()
    {
        teacherAnim = GetComponent<Animator>();
        Instance = this; // Inicjalizacja singletona
        if (gameOverText != null)
        {
            gameOverText.enabled = false; // Wy��cz tekst przegranej na pocz�tku
        }
    }

    private void Update()
    {
        // Je�li nauczycielka nie sprawdza, powr�� do stanu "Back"
        if (!isChecking)
        {
            teacherAnim.SetTrigger("Back");
        }

        // Zmniejsz timer do nast�pnego sprawdzenia
        if (timeUntilNextCheck > 0)
        {
            timeUntilNextCheck -= Time.deltaTime;
        }
        else
        {
            // Wywo�aj sprawdzenie
            StartCoroutine(CheckForCheating());
            timeUntilNextCheck = Random.Range(checkIntervalMin, checkIntervalMax);
        }

        // Aktualizacja paska post�pu �ci�gania
        if (playerIsCheating && !isChecking)
        {
            cheatingProgressSlider.value += Time.deltaTime * 0.04f; // Nape�nianie Slider'a (wolno)
            cheatingProgressSlider.value = Mathf.Clamp(cheatingProgressSlider.value, 0f, cheatingProgressSlider.maxValue); // Ogranicz warto�� Slider'a
        }

        // Sprawd�, czy gracz osi�gn�� maksymalny poziom �ci�gania
        if (cheatingProgressSlider.value >= cheatingProgressSlider.maxValue)
        {
            WinGame(); // Wy�wietl napis "WYGRA�E�!" i przejd� do nast�pnej sceny
        }
    }

    IEnumerator CheckForCheating()
    {
        // Sygna� (np. mrugni�cie oczami lub inny efekt wizualny)
         // Uruchom animacj� sprawdzania
        GetComponent<SpriteRenderer>().color = Color.yellow; // Na przyk�ad: zmiana koloru na ��ty
        yield return new WaitForSeconds(signalDuration);
        GetComponent<SpriteRenderer>().color = Color.white;
        isChecking = true;
        teacherAnim.SetTrigger("Check");// Przywr�cenie oryginalnego koloru

        // Sprawd�, czy gracz �ci�ga
        if (playerIsCheating)
        {
            warnings++;
            Debug.Log($"Wykryto �ci�ganie! Ostrze�enie #{warnings}");
            UpdateWarningCounter();

            if (warnings >= 3)
            {
                GameOver(); // Wy�wietl napis "PRZEGRA�E�"
            }
        }

        // Wr�� do stanu pocz�tkowego
        isChecking = false;
    }

    public bool IsChecking => isChecking;

    public void SetPlayerIsCheating(bool isCheating)
    {
        playerIsCheating = isCheating;
    }

    // Aktualizacja licznika ostrze�e� w UI
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
            gameOverText.enabled = true; // Wy�wietl tekst przegranej
            gameOverText.text = "PRZEGRA�E�!"; // Ustaw tre�� tekstu
        }

        Debug.Log("Przegra�e�! Dosta�e� 3 ostrze�e�.");

        // Restartuj scen� po 3 sekundach
        Invoke("RestartScene", 3f);
    }

    // Metoda do restartowania sceny
    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Za�aduj ponownie aktualn� scen�
    }

    // Logika wygranej
    private void WinGame()
    {
        Debug.Log("Gracz zdoby� ca�� wiedz�!");

        // Wy�wietl napis "WYGRA�E�!"
        if (gameOverText != null)
        {
            gameOverText.enabled = true;
            gameOverText.text = "WYGRA�E�!";
        }

        // Przejd� do nast�pnej sceny po 2 sekundach
        Invoke("LoadNextScene", 2f);
    }

    // Metoda do �adowania nast�pnej sceny
    private void LoadNextScene()
    {
        // Pobierz indeks aktualnej sceny
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Je�li istnieje kolejna scena, za�aduj j�
        if (currentSceneIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(currentSceneIndex + 1); // Za�aduj nast�pn� scen�
        }
        else
        {
            Debug.LogWarning("Brak nast�pnej sceny! Koniec gry.");
        }
    }
}