using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public SceneFader sceneFader; // pour le fondu
    public string levelToLoad = "Level_1";

    public void PlayGame()
    {
        // Lancer le fade puis charger la scène du Level_1
        if (sceneFader != null)
            sceneFader.FadeToScene(levelToLoad);
        else
            SceneManager.LoadScene(levelToLoad);
    }

    public void QuitGame()
    {
        Debug.Log("Quitter le jeu...");
        Application.Quit();
    }
}