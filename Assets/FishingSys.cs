using UnityEngine;
using UnityEngine.UI;
public class FishingMechanic : MonoBehaviour
{
    public float fillSpeed = 0.5f; // Szybko�� wype�niania paska
    public float maxFill = 1f; // Maksymalna warto�� paska
    public float spamInterval = 0.1f; // Minimalny czas mi�dzy naciskaniami spacji
    public Image fillBar; // Referencja do paska filla
    private float currentFill = 0f;
    private bool isFishing = false;
    private float lastPressTime = 0f;
    
    Animator animator;
    void Start()
    {
        if (fillBar == null)
        {
            Debug.LogError("Przypisz pasek filla w edytorze!");
        }
        StartFishing();
    }
    void Update()
    {
        HandleFishing();
        if(fillBar.fillAmount>=1f)
        {
            animator.SetTrigger("RYBA");
        }
    }
    public void StartFishing()
    {
        isFishing = true;
        currentFill = 0f; // Resetuj pasek
        fillBar.fillAmount = currentFill;
    }
    private void HandleFishing()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Fiszing, adding to bar, amount: " + fillSpeed + " current amount " + fillBar.fillAmount);
            fillBar.fillAmount += fillSpeed;
        }
        if (currentFill >= maxFill)
        {
            FinishFishing(true); // Sukces
            animator.SetTrigger("RYBA");
        }
    }

    private void FinishFishing(bool success)
    {
        isFishing = false;
        if (success)
        {
            Debug.Log("Uda�o si� z�apa� ryb�!");
            // Mo�esz doda� efekty, np. animacj� ryby czy punkty
        }
        else
        {
            Debug.Log("Przerwano po�ow!");
        }
    }
    public void CancelFishing()
    {
        isFishing = false;
        currentFill = 0f;
        Debug.Log("Po�ow przerwany.");
    }
}