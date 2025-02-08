using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    // Referencje do UI
    public GameObject dialoguePanel; // Panel dialogowy
    public TMP_Text dialogueText; // Tekst dialogowy (TMP)
    public Button[] optionButtons; // Przyciski opcji odpowiedzi
    public Slider loveMeter; // Love meter
    public GameController gameController; // Referencja do GameController

    // Dane dialogowe
    private Dialogue currentDialogue;
    private int currentLineIndex = 0;

    // Min i max warto�ci love meter
    [SerializeField] private float maxLove = 100f;
    [SerializeField] private float minLove = 0f;

    private float savedLoveValue; // Zmienna do przechowywania warto�ci love meter

    private void Start()
    {
        if (dialoguePanel == null || dialogueText == null || optionButtons == null || loveMeter == null)
        {
            Debug.LogError("Brakuj�ce referencje w inspektorze!");
            return;
        }

        foreach (var button in optionButtons)
        {
            if (button == null)
            {
                Debug.LogError("Jeden z przycisk�w w tablicy optionButtons jest pusty!");
            }
            button.gameObject.SetActive(false); // Wy��cz przyciski na pocz�tku
        }

        dialoguePanel.SetActive(false); // Ukryj panel dialogowy na pocz�tku
        loveMeter.gameObject.SetActive(false); // Wy��cz Slider na pocz�tku
    }

    // Start dialogu
    public void StartDialogue(Dialogue dialogue)
    {
        if (dialogue == null) return;

        currentDialogue = dialogue;
        currentLineIndex = 0;

        dialoguePanel.SetActive(true); // Poka� panel dialogowy

        if (loveMeter != null)
        {
            loveMeter.gameObject.SetActive(true); // W��cz Slider
            loveMeter.value = 0; // Zresetuj Love Meter na 0
        }

        ShowNextLine();
    }

    // Poka� nast�pn� lini� dialogu
    private void ShowNextLine()
    {
        if (currentLineIndex < currentDialogue.lines.Length)
        {
            DialogueLine line = currentDialogue.lines[currentLineIndex];
            dialogueText.text = line.text;

            int optionCount = Mathf.Min(optionButtons.Length, line.options.Length);

            for (int i = 0; i < optionCount; i++)
            {
                int optionIndex = i;

                optionButtons[i].gameObject.SetActive(true); // W��cz przycisk
                optionButtons[i].GetComponentInChildren<TMP_Text>().text = line.options[i].text;
                optionButtons[i].onClick.RemoveAllListeners();
                optionButtons[i].onClick.AddListener(() => SelectOption(line.options[optionIndex]));
            }

            // Wy��cz pozosta�e przyciski
            for (int i = optionCount; i < optionButtons.Length; i++)
            {
                optionButtons[i].gameObject.SetActive(false); // Wy��cz przyciski, kt�re nie s� potrzebne
            }
        }
        else
        {
            EndDialogue();
        }
    }

    // Wybierz opcj� odpowiedzi
    private void SelectOption(DialogueOption option)
    {
        if (loveMeter != null && loveMeter.gameObject.activeSelf)
        {
            float oldValue = loveMeter.value;
            loveMeter.value += option.loveChange;
            loveMeter.value = Mathf.Clamp(loveMeter.value, minLove, maxLove);

            Debug.Log($"Love Meter: Stara warto�� = {oldValue}, Nowa warto�� = {loveMeter.value}, Zmiana = {option.loveChange}");
        }
        else
        {
            Debug.LogError("Love Meter jest nieaktywny lub niezainicjowany!");
        }

        currentLineIndex++;
        ShowNextLine();
    }

    // Ko�czy dialog
    private void EndDialogue()
    {
        if (dialogueText != null)
        {
            dialogueText.text = "Koniec dialogu!";
        }

        foreach (var button in optionButtons)
        {
            button.gameObject.SetActive(false); // Wy��cz wszystkie przyciski
        }

        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false); // Ukryj panel dialogowy
        }

        // Zachowaj bie��c� warto�� love meter przed wy��czeniem
        if (loveMeter != null)
        {
            savedLoveValue = loveMeter.value;
            loveMeter.gameObject.SetActive(false); // Wy��cz Slider
        }

        if (gameController != null)
        {
            gameController.CheckLoveMeterResult();
        }
    }
}