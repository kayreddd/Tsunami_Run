using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject endGameUI;
    private SceneFader fader;

    private void Start()
    {
        fader = FindFirstObjectByType<SceneFader>();
    }
    public void LoadNextLevel()
    {
        // Reprend le temps normal
        Time.timeScale = 1f;

        // Récupère l’index de la scène actuelle
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // si on est pas au dernier niveau
        if (currentSceneIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            string nextScene = SceneUtility.GetScenePathByBuildIndex(currentSceneIndex + 1);
            StartCoroutine(fader.FadeOutAndLoad(System.IO.Path.GetFileNameWithoutExtension(nextScene)));
        }
        else
        {
            // Dernier niveau et affiche la fin du jeu
            Debug.Log("Fin du jeu !");
            if (endGameUI != null)
            {
                endGameUI.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }

    //public void RestartLevel()
    //{
    //    // Recharge la scène actuelle
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    //    Time.timeScale = 1f;
    //}

    //public void QuitGame()
    //{
    //    Debug.Log("Quit Game");
    //    Application.Quit();
    //}
}