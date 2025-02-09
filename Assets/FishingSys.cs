using UnityEngine;
using UnityEngine.UI;

public class FishingMechanic : MonoBehaviour
{
    public float fillSpeed = 0.5f; // Szybko�� wype�niania paska
    public float maxFill = 1f; // Maksymalna warto�� paska (ustawiona na 1 dla Image.fillAmount)
    public float spamInterval = 0.1f; // Minimalny czas mi�dzy naciskaniami spacji
    public float minTimeBetweenFishing = 5f; // Minimalny czas przed rozpocz�ciem nowego po�owu
    public float maxTimeBetweenFishing = 10f; // Maksymalny czas przed rozpocz�ciem nowego po�owu
    public Image fillBar; // Referencja do paska filla

    private float currentFill = 0f;
    private bool isFishing = false;
    private float lastPressTime = 0f;
    private float nextFishingTime = 0f;

    void Start()
    {
        if (fillBar == null)
        {
            Debug.LogError("Przypisz pasek filla w edytorze!");
        }

        // Wylosuj czas rozpocz�cia pierwszego po�owu
        ScheduleNextFishing();
    }

    void Update()
    {
        // Je�li nadszed� czas rozpocz�cia po�owu
        if (Time.time >= nextFishingTime && !isFishing)
        {
            StartFishing();
        }

        // Obs�u� mechanik� po�owu, je�li jest aktywna
        if (isFishing)
        {
            HandleFishing();
        }
    }

    public void StartFishing()
    {
        isFishing = true;
        currentFill = 0f; // Resetuj pasek
        fillBar.fillAmount = currentFill; // Ustaw pasek na 0
        Debug.Log("Ryba ugryz�a! Si�uj si� z ni�, spamuj�c spacj�!");
    }

    private void HandleFishing()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            float currentTime = Time.time;
            if (currentTime - lastPressTime >= spamInterval)
            {
                lastPressTime = currentTime;
                currentFill += fillSpeed;

                // Ogranicz warto�� currentFill do maksymalnej warto�ci
                currentFill = Mathf.Min(currentFill, maxFill);

                // Aktualizuj pasek filla
                fillBar.fillAmount = currentFill;

                // Je�li pasek osi�gnie maksymaln� warto��, zako�cz po��w
                if (currentFill >= maxFill)
                {
                    FinishFishing(true); // Sukces
                }
            }
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

        // Wyzeruj licznik i zaplanuj nast�pny po��w
        currentFill = 0f;
        fillBar.fillAmount = currentFill;
        ScheduleNextFishing();
    }

    public void CancelFishing()
    {
        isFishing = false;
        currentFill = 0f;
        fillBar.fillAmount = currentFill;
        Debug.Log("Po�ow przerwany.");
        ScheduleNextFishing(); // Zaplanuj nast�pny po��w
    }

    private void ScheduleNextFishing()
    {
        // Wylosuj czas do nast�pnego po�owu
        float timeToNextFishing = Random.Range(minTimeBetweenFishing, maxTimeBetweenFishing);
        nextFishingTime = Time.time + timeToNextFishing;
        Debug.Log($"Nast�pny po��w rozpocznie si� za {timeToNextFishing:F2} sekund.");
    }
}