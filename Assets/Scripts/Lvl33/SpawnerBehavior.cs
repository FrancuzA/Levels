using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    public GameObject resourcePrefab; // Prefab zasobu
    public bool canSpawn = true;
    public SpriteRenderer spawnerSpriteRenderer;

    void Start()
    {
        UpdateSpawnerSprite();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && canSpawn)
        {
            PlayerControllerKids player = other.GetComponent<PlayerControllerKids>();
            if (player != null && player.heldItem == null) // Sprawd�, czy gracz nie trzyma ju� �adnego zasobu
            {
                player.PickUpItem(resourcePrefab); // Podaj graczowi zas�b
                canSpawn = false; // Wy��cz spawner na chwil�
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ResetSpawner(); // Resetuj spawner, gdy gracz opu�ci obszar
        }
    }

    public void ResetSpawner()
    {
        canSpawn = true; // Pozw�l na ponowne pobranie zasobu
    }

    void UpdateSpawnerSprite()
    {
        if (spawnerSpriteRenderer != null && resourcePrefab != null)
        {
            spawnerSpriteRenderer.sprite = resourcePrefab.GetComponent<SpriteRenderer>().sprite;
        }
    }
}