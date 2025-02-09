using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;

public class PlayerManager : MonoBehaviour
{
    public float moveSpeed = 5f; // Prędkość gracza
    public GameObject cakePrefab; // Prefab tortu
    private GameObject currentCake; // Aktualny tort trzymany przez gracza

    // Lista dzieci
    public Child[] children;

    // Referencja do TextMeshPro dla komunikatu wygranej
    public TMP_Text winMessageText;

    private void Update()
    {
        // Ruch gracza
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(horizontalInput, verticalInput);
        transform.Translate(movement.normalized * moveSpeed * Time.deltaTime);

        // Podniesienie tortu (np. naciśnięcie spacji)
        if (Input.GetKeyDown(KeyCode.Space) && currentCake == null)
        {
            SpawnCake();
        }

        // Dostarczenie tortu (np. naciśnięcie E)
        if (Input.GetKeyDown(KeyCode.E) && currentCake != null)
        {
            DeliverCake();
        }
    }

    // Metoda spawnująca tort
    private void SpawnCake()
    {
        if (cakePrefab != null)
        {
            currentCake = Instantiate(cakePrefab, transform.position, Quaternion.identity);
        }
    }

    // Metoda dostarczająca tort do dziecka
    private void DeliverCake()
    {
        if (currentCake != null)
        {
            Destroy(currentCake); // Usuń tort z planszy
            currentCake = null;

            // Sprawdź, czy jesteśmy blisko któregoś z dzieci
            foreach (Child child in children)
            {
                if (Vector2.Distance(transform.position, child.transform.position) < 1f && !child.hasReceivedCake)
                {
                    child.DeliverCake(); // Dostarcz tort dziecku
                    CheckIfAllChildrenAreHappy(); // Sprawdź, czy wszystkie dzieci są szczęśliwe
                    break;
                }
            }
        }
    }

    // Sprawdzenie, czy wszystkie dzieci są szczęśliwe
    private void CheckIfAllChildrenAreHappy()
    {
        bool allHappy = true;
        foreach (Child child in children)
        {
            if (!child.hasReceivedCake)
            {
                allHappy = false;
                break;
            }
        }

        if (allHappy)
        {
            GameOver(true); // Wywołaj metodę wygranej
        }
    }

    // Koniec gry
    private void GameOver(bool isWin)
    {
        enabled = false; // Wyłącz sterowanie graczem

        if (isWin)
        {
            // Wyświetl komunikat wygranej
            ShowWinMessage();

            // Załaduj następną scenę po kilku sekundach
            Invoke("LoadNextScene", 3f); // Załaduj następną scenę po 3 sekundach
        }
        else
        {
            Debug.Log("Przegrałeś!");
        }
    }

    // Wyświetlenie komunikatu wygranej za pomocą TextMeshPro
    private void ShowWinMessage()
    {
        if (winMessageText != null)
        {
            winMessageText.enabled = true; // Pokaż tekst
            winMessageText.text = "WYGRAŁEŚ! Wszystkie dzieci są szczęśliwe!"; // Ustaw treść
        }
    }

    // Ładowanie następnej sceny
    private void LoadNextScene()
    {
       
        SceneManager.LoadScene(6);
    }
}