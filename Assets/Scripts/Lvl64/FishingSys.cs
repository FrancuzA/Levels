using UnityEngine;
using UnityEngine.UI;

public class FishingBar : MonoBehaviour
{
    public float fillSpeed = 0.1f; // Szybkoœæ wype³niania paska
    public float targetValue = 0.7f; // Idealna wartoœæ (bloczek)
    public float tolerance = 0.1f; // Zakres dopuszczalnej wartoœci
    public Image fillBar; // Referencja do wype³nienia paska
    private bool isFilling = false; // Czy pasek jest wype³niany
    private bool hasCaughtFish = false; // Czy ryba zosta³a z³apana

    void Update()
    {
        if (!hasCaughtFish)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                // Wype³nij pasek, gdy gracz trzyma spacjê
                isFilling = true;
                FillBar();
            }
            else
            {
                // SprawdŸ, czy gracz puœci³ spacjê w odpowiednim momencie
                isFilling = false;
                CheckIfCaughtFish();
            }
        }
    }

    void FillBar()
    {
        // Zwiêksz wartoœæ paska
        float currentValue = fillBar.fillAmount + fillSpeed * Time.deltaTime;
        fillBar.fillAmount = Mathf.Clamp01(currentValue); // Ogranicz wartoœæ do [0, 1]
    }

    void CheckIfCaughtFish()
    {
        // SprawdŸ, czy wartoœæ paska jest w idealnym zakresie
        if (fillBar.fillAmount >= targetValue - tolerance && fillBar.fillAmount <= targetValue + tolerance)
        {
            Debug.Log("Z³apa³eœ rybkê!");
            hasCaughtFish = true;
            // Mo¿esz tu dodaæ kod do pokazania animacji ryby
        }
        else
        {
            Debug.Log("Ryba uciek³a!");
            ResetBar();
        }
    }

    void ResetBar()
    {
        // Resetuj pasek po nieudanej próbie
        fillBar.fillAmount = 0f;
        hasCaughtFish = false;
    }
}