using UnityEngine;

public class ChildBehavior : MonoBehaviour
{
    public string[] possibleRequests = { "Toy", "Food", "Drink" }; // Mo�liwe ��dania
    public string requestedItem; // Aktualne ��danie dziecka
    public float patienceTime = 10f; // Czas, przez kt�ry dziecko czeka na zas�b
    private float timer;
    public SpriteRenderer spriteRenderer;
    public Color impatientColor = Color.red;
    public GameObject[] resourcePrefabs; // Prefaby zasob�w
    private bool isImpatient = false; // Flaga niecierpliwo�ci
    private float requestChangeDelay = 5f; // Czas po kt�rym dziecko zmieni ��danie po otrzymaniu zasobu
    public float impatientTimeout = 5f; // Maksymalny czas niecierpliwo�ci

    void Start()
    {
        AssignRandomRequest(); // Przypisanie losowego ��dania przy starcie
        timer = patienceTime;
        UpdateChildSprite();
    }

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            BecomeImpatient();
        }

        // Je�li dziecko jest niecierpliwe, zwi�kszamy licznik niecierpliwo�ci
        if (isImpatient)
        {
            impatientTimeout -= Time.deltaTime;
            if (impatientTimeout <= 0)
            {
                Debug.LogError($"Dziecko {name} przesta�o si� kontrolowa�! Przegra�e�!");
                GameOver();
            }
        }
    }

    public void ReceiveItem(string itemName)
    {
        if (itemName == requestedItem)
        {
            Debug.Log($"Dziecko {name} dosta�o poprawny zas�b!");

            // Reset cierpliwo�ci i zmiana ��dania
            ResetPatience();
            Invoke("AssignRandomRequestDelayed", requestChangeDelay); // Zmiana ��dania po op�nieniu
        }
        else
        {
            Debug.Log("Z�y zas�b! Dziecko nie jest zadowolone.");
        }
    }

    void AssignRandomRequestDelayed()
    {
        AssignRandomRequest(); // Nowe losowe ��danie
        UpdateChildSprite(); // Aktualizacja sprita dziecka
    }

    void BecomeImpatient()
    {
        if (!isImpatient)
        {
            spriteRenderer.color = impatientColor;
            Debug.LogWarning($"Dziecko {name} jest niecierpliwe!");
            isImpatient = true;
            impatientTimeout = 5f; // Reset limitu czasu niecierpliwo�ci
        }
    }

    void ResetPatience()
    {
        timer = patienceTime; // Reset licznika cierpliwo�ci
        spriteRenderer.color = Color.white; // Przywr�cenie normalnego koloru sprita
        isImpatient = false; // Wyzeruj flag� niecierpliwo�ci
        impatientTimeout = 5f; // Reset limitu czasu niecierpliwo�ci
    }

    void AssignRandomRequest()
    {
        requestedItem = possibleRequests[Random.Range(0, possibleRequests.Length)];
        Debug.Log($"Dziecko {name} teraz chce: {requestedItem}");
    }

    void UpdateChildSprite()
    {
        foreach (var prefab in resourcePrefabs)
        {
            if (prefab.name == requestedItem)
            {
                spriteRenderer.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                break;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerControllerKids player = other.GetComponent<PlayerControllerKids>();
            if (player != null && player.heldItem != null)
            {
                string itemName = player.heldItem.name; // Pobranie nazwy trzymanego zasobu
                ReceiveItem(itemName); // Sprawdzenie, czy zas�b pasuje
                player.DropItem(); // Usuni�cie zasobu z r�k ojca
            }
        }
    }

    void GameOver()
    {
        SystemManager systemManager = Object.FindFirstObjectByType<SystemManager>();
        if (systemManager != null)
        {
            systemManager.GameOver(); // Wywo�anie przegranej
        }
        else
        {
            Debug.LogError("SystemManager nie zosta� znaleziony!");
        }
    }
}