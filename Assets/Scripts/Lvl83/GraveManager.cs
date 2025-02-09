using UnityEngine;
using System.Collections;

public class GraveManager : MonoBehaviour
{
    public GameObject F; // Obiekt wskaźnika "Naciśnij F"
    public static bool isInRange; // Czy gracz jest w zasięgu
    private DziadController movePlayer; // Skrypt kontrolujący ruch gracza
    public GameObject Flowers; // Obiekt kwiatów do aktywacji
    public FadeManager fadeManager; // Manager odpowiedzialny za efekt fade
    public GameObject nextObject; // Nowy obiekt do aktywacji po interakcji
    public GameObject EndScreen;

    private bool hasInteracted = false; // Flaga, czy już dokonano interakcji

    private void Start()
    {
        // Znajdź gracza i jego skrypt ruchu
        GameObject player = GameObject.FindGameObjectWithTag("Player"); // Znajdź gracza po tagu
        if (player != null)
        {
            movePlayer = player.GetComponent<DziadController>(); // Pobierz skrypt ruchu gracza
        }
        else
        {
            Debug.LogError("Gracz o tagu 'Player' nie został znaleziony!");
        }

        // Wyłącz początkowo kwiaty
        if (Flowers != null)
        {
            Flowers.SetActive(false);
        }
        else
        {
            Debug.LogError("Obiekt 'Flowers' nie jest przypisany!");
        }

        // Upewnij się, że manager fade nie jest null
        if (fadeManager == null)
        {
            Debug.LogError("FadeManager nie jest przypisany!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            F.SetActive(true); // Wyświetl wskaźnik "Naciśnij F"
        }
        isInRange = true; // Gracz jest w zasięgu
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            F.SetActive(false); // Ukryj wskaźnik "Naciśnij F"
        }
        isInRange = false; // Gracz opuszcza zasięg
        hasInteracted = false; // Reset flagi po opuszczeniu zasięgu
    }

    private void Update()
    {
        // Jeśli gracz naciska F, jest w zasięgu i jeszcze nie dokonał interakcji
        if (Input.GetKeyDown(KeyCode.F) && isInRange && !hasInteracted)
        {
            if (movePlayer != null)
            {
                movePlayer.initialMovementSpeed = 0f; // Zatrzymaj gracza
            }
            else
            {
                Debug.LogError("Skrypt 'DziadController' nie został znalezione na graczu!");
            }

            if (Flowers != null)
            {
                Flowers.SetActive(true); // Aktywuj kwiaty
            }
            else
            {
                Debug.LogError("Obiekt 'Flowers' nie jest przypisany!");
            }

            // Rozpocznij proces fade-out
            StartCoroutine(FadeOutAndTransition());
            hasInteracted = true; // Ustaw flage, że interakcja została wykonana
        }
    }

    private IEnumerator FadeOutAndTransition()
    {
        // Wywołaj fade-out
        if (fadeManager != null)
        {
            fadeManager.FadeOut(); // Rozpocznij efekt fade-out
            yield return new WaitForSeconds(fadeManager.fadeDuration); // Poczekaj na zakończenie fade-out
        }

        // Wyłącz obiekt gracza
        if (movePlayer != null)
        {
            movePlayer.gameObject.SetActive(false);
        }

        // Poczekaj chwilę przed fade-in
        yield return new WaitForSeconds(2f);
        if (nextObject != null)
        {
            nextObject.SetActive(true);
        }
        // Wywołaj fade-in
        if (fadeManager != null)
        {
            fadeManager.FadeIn(); // Rozpocznij efekt fade-in
            yield return new WaitForSeconds(fadeManager.fadeDuration); // Poczekaj na zakończenie fade-in
        }

        yield return new WaitForSeconds(5);
        EndScreen.SetActive(true);

        yield return new WaitForSeconds(5);

        Application.Quit();
        // Aktywuj nowy obiekt

    }
}