using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float moveSpeed = 5f; // Pr�dko�� gracza
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

        // Podniesienie tortu (np. naci�ni�cie spacji)
        if (Input.GetKeyDown(KeyCode.Space) && currentCake == null)
        {
            SpawnCake();
        }

        // Dostarczenie tortu (np. naci�ni�cie E)
        if (Input.GetKeyDown(KeyCode.E) && currentCake != null)
        {
            DeliverCake();
        }
    }

    // Metoda spawnuj�ca tort
    private void SpawnCake()
    {
        if (cakePrefab != null)
        {
            currentCake = Instantiate(cakePrefab, transform.position, Quaternion.identity);
        }
    }

    // Metoda dostarczaj�ca tort do dziecka
    private void DeliverCake()
    {
        if (currentCake != null)
        {
            Destroy(currentCake); // Usu� tort z planszy
            currentCake = null;

            // Sprawd�, czy jeste�my blisko kt�rego� z dzieci
            foreach (Child child in children)
            {
                if (Vector2.Distance(transform.position, child.transform.position) < 1f && !child.hasReceivedCake)
                {
                    child.DeliverCake(); // Dostarcz tort dziecku
                    CheckIfAllChildrenAreHappy(); // Sprawd�, czy wszystkie dzieci s� szcz�liwe
                    break;
                }
            }
        }
    }

    // Sprawdzenie, czy wszystkie dzieci s� szcz�liwe
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
            Debug.Log("Wszystkie dzieci s� szcz�liwe! Wygra�e�!");
            GameOver(true);
        }
    }

    // Koniec gry
    private void GameOver(bool isWin)
    {
        enabled = false; // Wy��cz sterowanie graczem
        if (isWin)
        {
            Debug.Log("Wygra�e�!");
        }
        else
        {
            Debug.Log("Przegra�e�!");
        }
    }
}
