using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float moveSpeed = 5f; // Prêdkoœæ gracza
    public GameObject cakePrefab; // Prefab tortu
    private GameObject currentCake; // Aktualny tort trzymany przez gracza

    // Lista dzieci
    public Child[] children;

    private void Update()
    {
        // Ruch gracza
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(horizontalInput, verticalInput);
        transform.Translate(movement.normalized * moveSpeed * Time.deltaTime);

        // Podniesienie tortu (np. naciœniêcie spacji)
        if (Input.GetKeyDown(KeyCode.Space) && currentCake == null)
        {
            SpawnCake();
        }

        // Dostarczenie tortu (np. naciœniêcie E)
        if (Input.GetKeyDown(KeyCode.E) && currentCake != null)
        {
            DeliverCake();
        }
    }

    // Metoda spawnuj¹ca tort
    private void SpawnCake()
    {
        if (cakePrefab != null)
        {
            currentCake = Instantiate(cakePrefab, transform.position, Quaternion.identity);
        }
    }

    // Metoda dostarczaj¹ca tort do dziecka
    private void DeliverCake()
    {
        if (currentCake != null)
        {
            Destroy(currentCake); // Usuñ tort z planszy
            currentCake = null;

            // SprawdŸ, czy jesteœmy blisko któregoœ z dzieci
            foreach (Child child in children)
            {
                if (Vector2.Distance(transform.position, child.transform.position) < 1f && !child.hasReceivedCake)
                {
                    child.DeliverCake(); // Dostarcz tort dziecku
                    CheckIfAllChildrenAreHappy(); // SprawdŸ, czy wszystkie dzieci s¹ szczêœliwe
                    break;
                }
            }
        }
    }

    // Sprawdzenie, czy wszystkie dzieci s¹ szczêœliwe
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
            Debug.Log("Wszystkie dzieci s¹ szczêœliwe! Wygra³eœ!");
            GameOver(true);
        }
    }

    // Koniec gry
    private void GameOver(bool isWin)
    {
        enabled = false; // Wy³¹cz sterowanie graczem
        if (isWin)
        {
            Debug.Log("Wygra³eœ!");
        }
        else
        {
            Debug.Log("Przegra³eœ!");
        }
    }
}
