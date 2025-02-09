using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeAndRespawn : MonoBehaviour
{
    public Image fadeScreen; // Przypisz Image (Canvas -> Image) w edytorze
    public GameObject player; // Przypisz gracza w edytorze
    public GameObject grave; // Przypisz nagrobek w edytorze

    private float fadeDuration = 1f; // Czas trwania efektu fade
    private float blackScreenDuration = 2f; // Czas trwania czarnego ekranu
    private bool isFading = false;

    private void Start()
    {
        if (fadeScreen == null)
        {
            Debug.LogError("Brak przypisania fadeScreen!");
        }
        if (player == null)
        {
            Debug.LogError("Brak przypisania gracza!");
        }
        if (grave == null)
        {
            Debug.LogError("Brak przypisania nagrobka!");
        }
    }

    public void TriggerFadeOut()
    {
        if (!isFading)
        {
            StartCoroutine(FadeOut());
        }
    }

    private IEnumerator FadeOut()
    {
        isFading = true;

        float fadeTime = 0f;
        while (fadeTime < fadeDuration)
        {
            fadeTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, fadeTime / fadeDuration);
            fadeScreen.color = new Color(0f, 0f, 0f, alpha);
            yield return null;
        }

        // Wy³¹cz gracza i w³¹cz nagrobek
        player.SetActive(false);
        grave.SetActive(true);

        // Poczekaj na czarnym ekranie
        yield return new WaitForSeconds(blackScreenDuration);

        // Rozpocznij fade in
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        float fadeTime = 0f;
        while (fadeTime < fadeDuration)
        {
            fadeTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, fadeTime / fadeDuration);
            fadeScreen.color = new Color(0f, 0f, 0f, alpha);
            yield return null;
        }

        isFading = false;
    }
}