using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public DialogueManager dialogueManager; 
    public Dialogue flirtDialogue;
    public Slider loveMeter; 
    public TMP_Text resultText; 

    private void Start()
    {
        if (dialogueManager == null || flirtDialogue == null || loveMeter == null || resultText == null)
        {
            
            return;
        }

        
        resultText.alpha = 0.0f;
        resultText.gameObject.SetActive(false);
    }

    
    public void StartFlirtGame()
    {
        if (dialogueManager != null && flirtDialogue != null)
        {
            dialogueManager.StartDialogue(flirtDialogue);
            dialogueManager.dialoguePanel.SetActive(true); 
        }

    }

   
    public void CheckLoveMeterResult()
    {
        if (loveMeter == null || resultText == null) return;

        float loveValue = loveMeter.value;

        
        resultText.alpha = 1.0f;
        resultText.gameObject.SetActive(true);

        if (loveValue >= 100f)
        {
            resultText.text = "Brawo! Alice siê z Tob¹ umówi³a!";
            Invoke("LoadNextScene", 5f); 
        }
        else
        {
            resultText.text = "Niestety, Alice nie chce siê z Tob¹ umawiaæ. Umrzesz w samotnoœci.";
            Invoke("ResetLevel", 3f); 
        }
    }

    
    private void LoadNextScene()
    {
        int currentSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;

        
        if (currentSceneIndex + 1 < UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(currentSceneIndex + 1); 
        }
        else
        {
           
            ResetLevel();
        }
    }

    
    private void ResetLevel()
    {
       
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex >= 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }
     
    }
}