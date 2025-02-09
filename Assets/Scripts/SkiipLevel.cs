using UnityEngine;
using UnityEngine.SceneManagement;

public class SkiipLevel : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
             

        LoadNextScene();
        }
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

