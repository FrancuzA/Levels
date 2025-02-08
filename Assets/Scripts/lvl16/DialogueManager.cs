using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FMODUnity;
using FMOD.Studio;

public class DialogueManager : MonoBehaviour
{
    
    public GameObject dialoguePanel; 
    public TMP_Text dialogueText; 
    public Button[] optionButtons; 
    public Slider loveMeter; 
    public GameController gameController; 

    
    private Dialogue currentDialogue;
    private int currentLineIndex = 0;

    public EventReference AngryReferance;
    private EventInstance AngryInstance;
    public EventReference NeutralReferance;
    private EventInstance NeutralInstance;
    public EventReference HappyReferance;
    private EventInstance HappyInstance;

    [SerializeField] private float maxLove = 100f;
    [SerializeField] private float minLove = 0f;

    private float savedLoveValue; 
    private void Start()
    {

        AngryInstance = RuntimeManager.CreateInstance(AngryReferance);
        NeutralInstance = RuntimeManager.CreateInstance(NeutralReferance);
        HappyInstance = RuntimeManager.CreateInstance(HappyReferance);
        if (dialoguePanel == null || dialogueText == null || optionButtons == null || loveMeter == null)
        {
            
            return;
        }

        foreach (var button in optionButtons)
        {
          
            button.gameObject.SetActive(false); 
        }

        dialoguePanel.SetActive(false); 
        loveMeter.gameObject.SetActive(false); 
    }

    
    public void StartDialogue(Dialogue dialogue)
    {
        if (dialogue == null) return;

        currentDialogue = dialogue;
        currentLineIndex = 0;

        dialoguePanel.SetActive(true); 

        if (loveMeter != null)
        {
            loveMeter.gameObject.SetActive(true); 
            loveMeter.value = 0; 
        }

        ShowNextLine();
    }

    
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

                optionButtons[i].gameObject.SetActive(true); 
                optionButtons[i].GetComponentInChildren<TMP_Text>().text = line.options[i].text;
                optionButtons[i].onClick.RemoveAllListeners();
                optionButtons[i].onClick.AddListener(() => SelectOption(line.options[optionIndex]));
            }

            
            for (int i = optionCount; i < optionButtons.Length; i++)
            {
                optionButtons[i].gameObject.SetActive(false); 
            }
        }
        else
        {
            EndDialogue();
        }
    }

    
    private void SelectOption(DialogueOption option)
    {
        if (loveMeter != null && loveMeter.gameObject.activeSelf)
        {
            float oldValue = loveMeter.value;
            loveMeter.value += option.loveChange;
            loveMeter.value = Mathf.Clamp(loveMeter.value, minLove, maxLove);
            switch (option.loveChange)
            {
                case -25:
                    AngryInstance.start();
                      break;
                case 0:
                    NeutralInstance.start();
                    break;
                case 25:
                    HappyInstance.start();
                    break;
            }
               
            
        }
       

        currentLineIndex++;
        ShowNextLine();
    }

    
    private void EndDialogue()
    {
     
        foreach (var button in optionButtons)
        {
            button.gameObject.SetActive(false); 
        }

        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false); 
        }

        
        if (loveMeter != null)
        {
            savedLoveValue = loveMeter.value;
            loveMeter.gameObject.SetActive(false); 
        }

        if (gameController != null)
        {
            gameController.CheckLoveMeterResult();
        }
    }
}