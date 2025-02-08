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
            if (player != null && player.heldItem == null) // SprawdŸ, czy gracz nie trzyma ju¿ ¿adnego zasobu
            {
                player.PickUpItem(resourcePrefab); // Podaj graczowi zasób
                canSpawn = false; // Wy³¹cz spawner na chwilê
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ResetSpawner(); // Resetuj spawner, gdy gracz opuœci obszar
        }
    }

    public void ResetSpawner()
    {
        canSpawn = true; // Pozwól na ponowne pobranie zasobu
    }

    void UpdateSpawnerSprite()
    {
        if (spawnerSpriteRenderer != null && resourcePrefab != null)
        {
            spawnerSpriteRenderer.sprite = resourcePrefab.GetComponent<SpriteRenderer>().sprite;
        }
    }
}