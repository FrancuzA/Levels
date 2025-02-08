using UnityEngine;

public class ChildBehavior : MonoBehaviour
{
    public string[] possibleRequests = { "Toy", "Food", "Drink" }; // Mo¿liwe ¿¹dania
    public string requestedItem; // Aktualne ¿¹danie dziecka
    public float patienceTime = 10f; // Czas, przez który dziecko czeka na zasób
    private float timer;
    public SpriteRenderer spriteRenderer;
    public Color impatientColor = Color.red;
    public GameObject[] resourcePrefabs; // Prefaby zasobów
    private bool isImpatient = false; // Flaga niecierpliwoœci
    private float requestChangeDelay = 5f; // Czas po którym dziecko zmieni ¿¹danie po otrzymaniu zasobu
    public float impatientTimeout = 5f; // Maksymalny czas niecierpliwoœci

    void Start()
    {
        AssignRandomRequest(); // Przypisanie losowego ¿¹dania przy starcie
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

        // Jeœli dziecko jest niecierpliwe, zwiêkszamy licznik niecierpliwoœci
        if (isImpatient)
        {
            impatientTimeout -= Time.deltaTime;
            if (impatientTimeout <= 0)
            {
                Debug.LogError($"Dziecko {name} przesta³o siê kontrolowaæ! Przegra³eœ!");
                GameOver();
            }
        }
    }

    public void ReceiveItem(string itemName)
    {
        if (itemName == requestedItem)
        {
            Debug.Log($"Dziecko {name} dosta³o poprawny zasób!");

            // Reset cierpliwoœci i zmiana ¿¹dania
            ResetPatience();
            Invoke("AssignRandomRequestDelayed", requestChangeDelay); // Zmiana ¿¹dania po opóŸnieniu
        }
        else
        {
            Debug.Log("Z³y zasób! Dziecko nie jest zadowolone.");
        }
    }

    void AssignRandomRequestDelayed()
    {
        AssignRandomRequest(); // Nowe losowe ¿¹danie
        UpdateChildSprite(); // Aktualizacja sprita dziecka
    }

    void BecomeImpatient()
    {
        if (!isImpatient)
        {
            spriteRenderer.color = impatientColor;
            Debug.LogWarning($"Dziecko {name} jest niecierpliwe!");
            isImpatient = true;
            impatientTimeout = 5f; // Reset limitu czasu niecierpliwoœci
        }
    }

    void ResetPatience()
    {
        timer = patienceTime; // Reset licznika cierpliwoœci
        spriteRenderer.color = Color.white; // Przywrócenie normalnego koloru sprita
        isImpatient = false; // Wyzeruj flagê niecierpliwoœci
        impatientTimeout = 5f; // Reset limitu czasu niecierpliwoœci
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
                ReceiveItem(itemName); // Sprawdzenie, czy zasób pasuje
                player.DropItem(); // Usuniêcie zasobu z r¹k ojca
            }
        }
    }

    void GameOver()
    {
        SystemManager systemManager = Object.FindFirstObjectByType<SystemManager>();
        if (systemManager != null)
        {
            systemManager.GameOver(); // Wywo³anie przegranej
        }
        else
        {
            Debug.LogError("SystemManager nie zosta³ znaleziony!");
        }
    }
}