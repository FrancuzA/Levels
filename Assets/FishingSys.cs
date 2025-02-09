using UnityEngine;
using UnityEngine.UI;

public class FishingMechanic : MonoBehaviour
{
    public float fillSpeed = 0.5f; // Szybkoœæ wype³niania paska
    public float maxFill = 1f; // Maksymalna wartoœæ paska (ustawiona na 1 dla Image.fillAmount)
    public float spamInterval = 0.1f; // Minimalny czas miêdzy naciskaniami spacji
    public float minTimeBetweenFishing = 5f; // Minimalny czas przed rozpoczêciem nowego po³owu
    public float maxTimeBetweenFishing = 10f; // Maksymalny czas przed rozpoczêciem nowego po³owu
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

        // Wylosuj czas rozpoczêcia pierwszego po³owu
        ScheduleNextFishing();
    }

    void Update()
    {
        // Jeœli nadszed³ czas rozpoczêcia po³owu
        if (Time.time >= nextFishingTime && !isFishing)
        {
            StartFishing();
        }

        // Obs³u¿ mechanikê po³owu, jeœli jest aktywna
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
        Debug.Log("Ryba ugryz³a! Si³uj siê z ni¹, spamuj¹c spacjê!");
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

                // Ogranicz wartoœæ currentFill do maksymalnej wartoœci
                currentFill = Mathf.Min(currentFill, maxFill);

                // Aktualizuj pasek filla
                fillBar.fillAmount = currentFill;

                // Jeœli pasek osi¹gnie maksymaln¹ wartoœæ, zakoñcz po³ów
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
            Debug.Log("Uda³o siê z³apaæ rybê!");
            // Mo¿esz dodaæ efekty, np. animacjê ryby czy punkty
        }
        else
        {
            Debug.Log("Przerwano po³ow!");
        }

        // Wyzeruj licznik i zaplanuj nastêpny po³ów
        currentFill = 0f;
        fillBar.fillAmount = currentFill;
        ScheduleNextFishing();
    }

    public void CancelFishing()
    {
        isFishing = false;
        currentFill = 0f;
        fillBar.fillAmount = currentFill;
        Debug.Log("Po³ow przerwany.");
        ScheduleNextFishing(); // Zaplanuj nastêpny po³ów
    }

    private void ScheduleNextFishing()
    {
        // Wylosuj czas do nastêpnego po³owu
        float timeToNextFishing = Random.Range(minTimeBetweenFishing, maxTimeBetweenFishing);
        nextFishingTime = Time.time + timeToNextFishing;
        Debug.Log($"Nastêpny po³ów rozpocznie siê za {timeToNextFishing:F2} sekund.");
    }
}