using UnityEngine;

public class GraveManager : MonoBehaviour
{
    public GameObject F; // Obiekt wska�nika "Naci�nij F"
    public static bool isInRange; // Czy gracz jest w zasi�gu
    private DziadController movePlayer; // Skrypt kontroluj�cy ruch gracza
    public GameObject Flowers; // Obiekt kwiat�w do aktywacji
    private bool hasInteracted = false; // Flaga, czy ju� dokonano interakcji

    private void Start()
    {
        // Znajd� gracza i jego skrypt ruchu
        GameObject player = GameObject.FindGameObjectWithTag("Player"); // Znajd� gracza po tagu
        if (player != null)
        {
            movePlayer = player.GetComponent<DziadController>(); // Pobierz skrypt ruchu gracza
        }
        else
        {
            Debug.LogError("Gracz o tagu 'Player' nie zosta� znaleziony!");
        }

        // Wy��cz pocz�tkowo kwiaty
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
            F.SetActive(true); // Wy�wietl wska�nik "Naci�nij F"
        }
        isInRange = true; // Gracz jest w zasi�gu
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            F.SetActive(false); // Ukryj wska�nik "Naci�nij F"
        }
        isInRange = false; // Gracz opuszcza zasi�g
        hasInteracted = false; // Reset flagi po opuszczeniu zasi�gu
    }

    private void Update()
    {
        // Je�li gracz naciska F, jest w zasi�gu i jeszcze nie dokona� interakcji
        if (Input.GetKeyDown(KeyCode.F) && isInRange && !hasInteracted)
        {
            if (movePlayer != null)
            {
                movePlayer.initialMovementSpeed = 0f; // Zatrzymaj gracza
            }
            else
            {
                Debug.LogError("Skrypt 'DziadController' nie zosta� znalezione na graczu!");
            }

            if (Flowers != null)
            {
                Flowers.SetActive(true); // Aktywuj kwiaty
            }
            else
            {
                Debug.LogError("Obiekt 'Flowers' nie jest przypisany!");
            }

            // Ustaw flage, �e interakcja zosta�a wykonana
            hasInteracted = true;
        }
    }
}