using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using FMODUnity;
using UnityEngine.SceneManagement;
using FMOD.Studio;

public class Teacher : MonoBehaviour
{
    public static Teacher Instance; 

    [Header("Teacher Settings")]
    public float checkIntervalMin = 3f; 
    public float checkIntervalMax = 7f; 
    public float signalDuration = 1f; 

    [Header("Cheating System")]
    public Image cheatingProgressSlider; 
    public Sprite[] warningSprites; 
    public TextMeshProUGUI gameOverText; 

    private bool isChecking = false; 
    private int warnings = 0; 
    private bool playerIsCheating = false; 
    private float timeUntilNextCheck = 0f; 
    private Animator teacherAnim; 
    [SerializeField] private GameObject wykrzynik;

    public EventReference CoughtReferance;
    private EventInstance CoughtInstance;
    private void Awake()
    {
        CoughtInstance = RuntimeManager.CreateInstance(CoughtReferance);
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
            cheatingProgressSlider.fillAmount += Time.deltaTime * 0.04f; 
            cheatingProgressSlider.fillAmount = Mathf.Clamp(cheatingProgressSlider.fillAmount, 0f, 1); 
        }

        
        if (cheatingProgressSlider.fillAmount >= 1)
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
            CoughtInstance.start();
            Debug.Log($"Wykryto �ci�ganie! Ostrze�enie #{warnings}");
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
    }

   
    private void GameOver()
    {
        if (gameOverText != null)
        {
            gameOverText.enabled = true; 
            gameOverText.text = "PRZEGRAŁEŚ!"; 
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
            gameOverText.text = "WYGRAŁEŚ!";
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