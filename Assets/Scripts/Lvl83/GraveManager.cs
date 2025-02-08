using UnityEngine;

public class GraveManager : MonoBehaviour
{
    public GameObject F; // Obiekt wskaŸnika "Naciœnij F"
    public static bool isInRange; // Czy gracz jest w zasiêgu
    private DziadController movePlayer; // Skrypt kontroluj¹cy ruch gracza
    public GameObject Flowers; // Obiekt kwiatów do aktywacji
    private bool hasInteracted = false; // Flaga, czy ju¿ dokonano interakcji

    private void Start()
    {
        // ZnajdŸ gracza i jego skrypt ruchu
        GameObject player = GameObject.FindGameObjectWithTag("Player"); // ZnajdŸ gracza po tagu
        if (player != null)
        {
            movePlayer = player.GetComponent<DziadController>(); // Pobierz skrypt ruchu gracza
        }
        else
        {
            Debug.LogError("Gracz o tagu 'Player' nie zosta³ znaleziony!");
        }

        // Wy³¹cz pocz¹tkowo kwiaty
        if (Flowers != null)
        {
            Flowers.SetActive(false);
        }
        else
        {
            Debug.LogError("Obiekt 'Flowers' nie jest przypisany!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            F.SetActive(true); // Wyœwietl wskaŸnik "Naciœnij F"
        }
        isInRange = true; // Gracz jest w zasiêgu
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            F.SetActive(false); // Ukryj wskaŸnik "Naciœnij F"
        }
        isInRange = false; // Gracz opuszcza zasiêg
        hasInteracted = false; // Reset flagi po opuszczeniu zasiêgu
    }

    private void Update()
    {
        // Jeœli gracz naciska F, jest w zasiêgu i jeszcze nie dokona³ interakcji
        if (Input.GetKeyDown(KeyCode.F) && isInRange && !hasInteracted)
        {
            if (movePlayer != null)
            {
                movePlayer.initialMovementSpeed = 0f; // Zatrzymaj gracza
            }
            else
            {
                Debug.LogError("Skrypt 'DziadController' nie zosta³ znalezione na graczu!");
            }

            if (Flowers != null)
            {
                Flowers.SetActive(true); // Aktywuj kwiaty
            }
            else
            {
                Debug.LogError("Obiekt 'Flowers' nie jest przypisany!");
            }

            // Ustaw flage, ¿e interakcja zosta³a wykonana
            hasInteracted = true;
        }
    }
}