using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public DialogueManager dialogueManager; // Referencja do DialogueManager
    public Dialogue flirtDialogue; // Referencja do danych dialogowych
    public Slider loveMeter; // Referencja do love meter
    public TMP_Text resultText; // Tekst wyœwietlaj¹cy wynik (TMP)

    private void Start()
    {
        if (dialogueManager == null || flirtDialogue == null || loveMeter == null || resultText == null)
        {
            Debug.LogError("Brakuj¹ce referencje w inspektorze!");
            return;
        }

        // Ustawienie alpha na 0, aby tekst by³ niewidoczny na pocz¹tku
        resultText.alpha = 0.0f;
        resultText.gameObject.SetActive(false);
    }

    // Metoda uruchamiaj¹ca minigrê
    public void StartFlirtGame()
    {
        if (dialogueManager != null && flirtDialogue != null)
        {
            dialogueManager.StartDialogue(flirtDialogue);
            dialogueManager.dialoguePanel.SetActive(true); // Poka¿ panel dialogowy
        }
        else
        {
            Debug.LogError("Nie mo¿na rozpocz¹æ dialogu - brakuj¹ce referencje!");
        }
    }

    // SprawdŸ wynik love meter po zakoñczeniu dialogu
    public void CheckLoveMeterResult()
    {
        if (loveMeter == null || resultText == null) return;

        float loveValue = loveMeter.value;

        // Ustawienie alpha na 1, aby tekst by³ widoczny
        resultText.alpha = 1.0f;
        resultText.gameObject.SetActive(true);

        if (loveValue >= 100f)
        {
            resultText.text = "Brawo! Alice siê z Tob¹ umówi³a!";
            Invoke("LoadNextScene", 5f); // Za³aduj nastêpn¹ scenê po 5 sekundach
        }
        else
        {
            resultText.text = "Niestety, Alice nie chce siê z Tob¹ umawiaæ. Umrzesz w samotnoœci.";
            Invoke("ResetLevel", 3f); // Resetuj scenê po 3 sekundach
        }
    }

    // Metoda ³adowania nastêpnej sceny
    private void LoadNextScene()
    {
        int currentSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;

        // SprawdŸ, czy istnieje nastêpna scena
        if (currentSceneIndex + 1 < UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(currentSceneIndex + 1); // Za³aduj nastêpn¹ scenê
        }
        else
        {
            Debug.LogError("Brak kolejnej sceny do za³adowania!");
            // Mo¿esz dodaæ alternatywn¹ logikê, np. resetowanie pierwszej sceny
            ResetLevel();
        }
    }

    // Metoda resetuj¹ca scenê
    private void ResetLevel()
    {
        Debug.Log("Resetowanie sceny...");
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex >= 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }
        else
        {
            Debug.LogError("Nie mo¿na za³adowaæ sceny!");
        }
    }
}