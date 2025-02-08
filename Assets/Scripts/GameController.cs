using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public DialogueManager dialogueManager; // Referencja do DialogueManager
    public Dialogue flirtDialogue; // Referencja do danych dialogowych
    public Slider loveMeter; // Referencja do love meter
    public TMP_Text resultText; // Tekst wy�wietlaj�cy wynik (TMP)

    private void Start()
    {
        if (dialogueManager == null || flirtDialogue == null || loveMeter == null || resultText == null)
        {
            Debug.LogError("Brakuj�ce referencje w inspektorze!");
            return;
        }

        // Ustawienie alpha na 0, aby tekst by� niewidoczny na pocz�tku
        resultText.alpha = 0.0f;
        resultText.gameObject.SetActive(false);
    }

    // Metoda uruchamiaj�ca minigr�
    public void StartFlirtGame()
    {
        if (dialogueManager != null && flirtDialogue != null)
        {
            dialogueManager.StartDialogue(flirtDialogue);
            dialogueManager.dialoguePanel.SetActive(true); // Poka� panel dialogowy
        }
        else
        {
            Debug.LogError("Nie mo�na rozpocz�� dialogu - brakuj�ce referencje!");
        }
    }

    // Sprawd� wynik love meter po zako�czeniu dialogu
    public void CheckLoveMeterResult()
    {
        if (loveMeter == null || resultText == null) return;

        float loveValue = loveMeter.value;

        // Ustawienie alpha na 1, aby tekst by� widoczny
        resultText.alpha = 1.0f;
        resultText.gameObject.SetActive(true);

        if (loveValue >= 100f)
        {
            resultText.text = "Brawo! Alice si� z Tob� um�wi�a!";
            Invoke("LoadNextScene", 5f); // Za�aduj nast�pn� scen� po 5 sekundach
        }
        else
        {
            resultText.text = "Niestety, Alice nie chce si� z Tob� umawia�. Umrzesz w samotno�ci.";
            Invoke("ResetLevel", 3f); // Resetuj scen� po 3 sekundach
        }
    }

    // Metoda �adowania nast�pnej sceny
    private void LoadNextScene()
    {
        int currentSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;

        // Sprawd�, czy istnieje nast�pna scena
        if (currentSceneIndex + 1 < UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(currentSceneIndex + 1); // Za�aduj nast�pn� scen�
        }
        else
        {
            Debug.LogError("Brak kolejnej sceny do za�adowania!");
            // Mo�esz doda� alternatywn� logik�, np. resetowanie pierwszej sceny
            ResetLevel();
        }
    }

    // Metoda resetuj�ca scen�
    private void ResetLevel()
    {
        Debug.Log("Resetowanie sceny...");
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex >= 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }
        else
        {
            Debug.LogError("Nie mo�na za�adowa� sceny!");
        }
    }
}