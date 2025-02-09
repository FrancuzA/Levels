using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeManager : MonoBehaviour
{
    public float fadeDuration = 2f; // Czas trwania efektu fade
    private Image fadeImage; // Referencja do Image odpowiadaj¹cego za fade

    private void Start()
    {
        // Pobierz referencjê do Image
        fadeImage = GetComponent<Image>();
        if (fadeImage == null)
        {
            Debug.LogError("Brak komponentu Image na tym obiekcie!");
        }
    }

    // Metoda do wykonania fade-out
    public void FadeOut()
    {
        StartCoroutine(FadeTo(1f)); // Zmiana przezroczystoœci na 1 (pe³na czern)
    }

    // Metoda do wykonania fade-in
    public void FadeIn()
    {
        StartCoroutine(FadeTo(0f)); // Zmiana przezroczystoœci na 0 (ca³y widoczny)
    }

    // Coroutine do zmiany przezroczystoœci
    private IEnumerator FadeTo(float targetAlpha)
    {
        float startAlpha = fadeImage.color.a; // Aktualna przezroczystoœæ
        float timeElapsed = 0f;

        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.unscaledDeltaTime; // U¿ywamy unscaledDeltaTime, aby nie zale¿a³o od pauzy gry
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, timeElapsed / fadeDuration);
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, newAlpha);
            yield return null;
        }

        // Upewnij siê, ¿e koñcowy stan jest precyzyjny
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, targetAlpha);
    }
}