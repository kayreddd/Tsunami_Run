using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameManager : MonoBehaviour
{
    public GameObject endGameUI;

    // Appelé quand le joueur atteint la sortie du dernier niveau
    public void ShowEndGameUI()
    {
        endGameUI.SetActive(true);
        Time.timeScale = 0f; // met le jeu en pause
    }

    public void ReplayGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1); // relance le jeu depuis Level1
    }

    public void QuitGame()
    {
        Debug.Log("Quitter le jeu...");
        Application.Quit(); // ne fermera pas l’éditeur, mais fonctionne dans une build
    }
}
