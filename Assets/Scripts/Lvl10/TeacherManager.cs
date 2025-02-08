using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro; 
using UnityEngine.SceneManagement; 

public class Teacher : MonoBehaviour
{
    public static Teacher Instance; 

    [Header("Teacher Settings")]
    public float checkIntervalMin = 3f; 
    public float checkIntervalMax = 7f; 
    public float signalDuration = 1f; 

    [Header("Cheating System")]
    public Slider cheatingProgressSlider; 
    public Image warningCounter; 
    public Sprite[] warningSprites; 
    public TextMeshProUGUI gameOverText; 

    private bool isChecking = false; 
    private int warnings = 0; 
    private bool playerIsCheating = false; 
    private float timeUntilNextCheck = 0f; 
    private Animator teacherAnim; 
    [SerializeField] private GameObject wykrzynik;
    private void Awake()
    {
        teacherAnim = GetComponent<Animator>();
        Instance = this;
        if (gameOverText != null)
        {
            gameOverText.enabled = false; 
        }
    }

    private void Update()
    {
        
        if (!isChecking)
        {
            teacherAnim.SetTrigger("Back");
        }

        
        if (timeUntilNextCheck > 0)
        {
            timeUntilNextCheck -= Time.deltaTime;
        }
        else
        {
            
            StartCoroutine(CheckForCheating());
            timeUntilNextCheck = Random.Range(checkIntervalMin, checkIntervalMax);
        }

        
        if (playerIsCheating && !isChecking)
        {
            cheatingProgressSlider.value += Time.deltaTime * 0.04f; 
            cheatingProgressSlider.value = Mathf.Clamp(cheatingProgressSlider.value, 0f, cheatingProgressSlider.maxValue); 
        }

        
        if (cheatingProgressSlider.value >= cheatingProgressSlider.maxValue)
        {
            WinGame(); 
        }
    }

    IEnumerator CheckForCheating()
    {
        
        wykrzynik.SetActive(true);
        yield return new WaitForSeconds(signalDuration);
        GetComponent<SpriteRenderer>().color = Color.white;
        isChecking = true;
        teacherAnim.SetTrigger("Check");
        wykrzynik.SetActive(false);

       
        if (playerIsCheating)
        {
            warnings++;
            Debug.Log($"Wykryto œci¹ganie! Ostrze¿enie #{warnings}");
            UpdateWarningCounter();

            if (warnings >= 3)
            {
                GameOver(); 
            }
        }

        
        isChecking = false;
    }

    public bool IsChecking => isChecking;

    public void SetPlayerIsCheating(bool isCheating)
    {
        playerIsCheating = isCheating;
    }

    
    private void UpdateWarningCounter()
    {
        if (warningSprites.Length > warnings)
        {
            warningCounter.sprite = warningSprites[warnings];
        }
    }

   
    private void GameOver()
    {
        if (gameOverText != null)
        {
            gameOverText.enabled = true; 
            gameOverText.text = "PRZEGRA£EŒ!"; 
        }

       

        
        Invoke("RestartScene", 3f);
    }

    
    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }

   
    private void WinGame()
    {
        

        
        if (gameOverText != null)
        {
            gameOverText.enabled = true;
            gameOverText.text = "WYGRA£EŒ!";
        }

       
        Invoke("LoadNextScene", 2f);
    }

    
    private void LoadNextScene()
    {
        
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        
        if (currentSceneIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    
    }
}