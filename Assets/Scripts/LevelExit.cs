using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    public GameObject levelCompleteUI; // le canvas à activer
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Sortie atteinte !");

            // Stoppe le joueur (facultatif)
            collision.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;

            // Active la popup
            levelCompleteUI.SetActive(true);
            Debug.Log("Popup activée ! Canvas actif : " + levelCompleteUI.activeSelf);

            // Met le jeu en pause (facultatif)
            Time.timeScale = 0f;

            // Ex : charger la sc�ne suivante
            //SceneManager.LoadScene("Level_2");

            //FindObjectOfType<SceneFader>().StartCoroutine(
            //FindObjectOfType<SceneFader>().FadeOut("Level2")
            //);
        }
    }
}
