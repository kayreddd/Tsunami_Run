using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneFader : MonoBehaviour
{
    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration = 1f;

    void Start()
    {
        // Démarre avec un écran noir
        fadeCanvasGroup.alpha = 1;
        // Lance le fade-in (disparition du noir)
        StartCoroutine(FadeIn());
    }

    public IEnumerator FadeIn()
    {
        float time = 0;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            fadeCanvasGroup.alpha = 1 - (time / fadeDuration);
            yield return null;
        }
        fadeCanvasGroup.alpha = 0;

        // Une fois le fondu terminé, on démarre l’eau
        WaterRise water = FindFirstObjectByType<WaterRise>();
        if (water != null)
        {
            water.StartWaterRise();
        }
    }

    public IEnumerator FadeOut(string sceneToLoad)
    {
        float time = 0;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            fadeCanvasGroup.alpha = time / fadeDuration;
            yield return null;
        }
        fadeCanvasGroup.alpha = 1;
        SceneManager.LoadScene(sceneToLoad);
    }

    public void FadeToNextLevel(string sceneName)
    {
        // Stoppe l'eau avant de changer de scène
        WaterRise water = FindFirstObjectByType<WaterRise>();
        if (water != null)
        {
            water.StopWater();
        }

        StartCoroutine(FadeOut(sceneName));
    }

    // --- Méthode utilisée par le menu principal ---
    public void FadeToScene(string sceneName)
    {
        WaterRise water = FindFirstObjectByType<WaterRise>();
        if (water != null)
        {
            water.StopWater();
        }

        StartCoroutine(FadeOut(sceneName));
    }

    // --- Méthode utilisée par le LevelManager ---
    public IEnumerator FadeOutAndLoad(string sceneName)
    {
        // Identique à FadeOut(), gardée pour compatibilité
        yield return StartCoroutine(FadeOut(sceneName));
    }
}