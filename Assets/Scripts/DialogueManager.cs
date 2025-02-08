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

    // Min i max wartoœci love meter
    [SerializeField] private float maxLove = 100f;
    [SerializeField] private float minLove = 0f;

    private float savedLoveValue; // Zmienna do przechowywania wartoœci love meter

    private void Start()
    {
        if (dialoguePanel == null || dialogueText == null || optionButtons == null || loveMeter == null)
        {
            Debug.LogError("Brakuj¹ce referencje w inspektorze!");
            return;
        }

        foreach (var button in optionButtons)
        {
            if (button == null)
            {
                Debug.LogError("Jeden z przycisków w tablicy optionButtons jest pusty!");
            }
            button.gameObject.SetActive(false); // Wy³¹cz przyciski na pocz¹tku
        }

        dialoguePanel.SetActive(false); // Ukryj panel dialogowy na pocz¹tku
        loveMeter.gameObject.SetActive(false); // Wy³¹cz Slider na pocz¹tku
    }

    // Start dialogu
    public void StartDialogue(Dialogue dialogue)
    {
        if (dialogue == null) return;

        currentDialogue = dialogue;
        currentLineIndex = 0;

        dialoguePanel.SetActive(true); // Poka¿ panel dialogowy

        if (loveMeter != null)
        {
            loveMeter.gameObject.SetActive(true); // W³¹cz Slider
            loveMeter.value = 0; // Zresetuj Love Meter na 0
        }

        ShowNextLine();
    }

    // Poka¿ nastêpn¹ liniê dialogu
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

                optionButtons[i].gameObject.SetActive(true); // W³¹cz przycisk
                optionButtons[i].GetComponentInChildren<TMP_Text>().text = line.options[i].text;
                optionButtons[i].onClick.RemoveAllListeners();
                optionButtons[i].onClick.AddListener(() => SelectOption(line.options[optionIndex]));
            }

            // Wy³¹cz pozosta³e przyciski
            for (int i = optionCount; i < optionButtons.Length; i++)
            {
                optionButtons[i].gameObject.SetActive(false); // Wy³¹cz przyciski, które nie s¹ potrzebne
            }
        }
        else
        {
            EndDialogue();
        }
    }

    // Wybierz opcjê odpowiedzi
    private void SelectOption(DialogueOption option)
    {
        if (loveMeter != null && loveMeter.gameObject.activeSelf)
        {
            float oldValue = loveMeter.value;
            loveMeter.value += option.loveChange;
            loveMeter.value = Mathf.Clamp(loveMeter.value, minLove, maxLove);

            Debug.Log($"Love Meter: Stara wartoœæ = {oldValue}, Nowa wartoœæ = {loveMeter.value}, Zmiana = {option.loveChange}");
        }
        else
        {
            Debug.LogError("Love Meter jest nieaktywny lub niezainicjowany!");
        }

        currentLineIndex++;
        ShowNextLine();
    }

    // Koñczy dialog
    private void EndDialogue()
    {
        if (dialogueText != null)
        {
            dialogueText.text = "Koniec dialogu!";
        }

        foreach (var button in optionButtons)
        {
            button.gameObject.SetActive(false); // Wy³¹cz wszystkie przyciski
        }

        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false); // Ukryj panel dialogowy
        }

        // Zachowaj bie¿¹c¹ wartoœæ love meter przed wy³¹czeniem
        if (loveMeter != null)
        {
            savedLoveValue = loveMeter.value;
            loveMeter.gameObject.SetActive(false); // Wy³¹cz Slider
        }

        if (gameController != null)
        {
            gameController.CheckLoveMeterResult();
        }
    }
}