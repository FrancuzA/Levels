using UnityEngine;
using UnityEngine.UI;

public class FishingBar : MonoBehaviour
{
    public float fillSpeed = 0.1f; // Szybko�� wype�niania paska
    public float targetValue = 0.7f; // Idealna warto�� (bloczek)
    public float tolerance = 0.1f; // Zakres dopuszczalnej warto�ci
    public Image fillBar; // Referencja do wype�nienia paska
    private bool isFilling = false; // Czy pasek jest wype�niany
    private bool hasCaughtFish = false; // Czy ryba zosta�a z�apana

    void Update()
    {
        if (!hasCaughtFish)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                // Wype�nij pasek, gdy gracz trzyma spacj�
                isFilling = true;
                FillBar();
            }
            else
            {
                // Sprawd�, czy gracz pu�ci� spacj� w odpowiednim momencie
                isFilling = false;
                CheckIfCaughtFish();
            }
        }
    }

    void FillBar()
    {
        // Zwi�ksz warto�� paska
        float currentValue = fillBar.fillAmount + fillSpeed * Time.deltaTime;
        fillBar.fillAmount = Mathf.Clamp01(currentValue); // Ogranicz warto�� do [0, 1]
    }

    void CheckIfCaughtFish()
    {
        // Sprawd�, czy warto�� paska jest w idealnym zakresie
        if (fillBar.fillAmount >= targetValue - tolerance && fillBar.fillAmount <= targetValue + tolerance)
        {
            Debug.Log("Z�apa�e� rybk�!");
            hasCaughtFish = true;
            // Mo�esz tu doda� kod do pokazania animacji ryby
        }
        else
        {
            Debug.Log("Ryba uciek�a!");
            ResetBar();
        }
    }

    void ResetBar()
    {
        // Resetuj pasek po nieudanej pr�bie
        fillBar.fillAmount = 0f;
        hasCaughtFish = false;
    }
}