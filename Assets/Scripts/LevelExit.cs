using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] private GameObject levelCompleteUI; // popup � activer
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Sortie atteinte !");

            // Stoppe le joueur (facultatif)
            collision.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;

            // Active la popup
            levelCompleteUI.SetActive(true);

            // Met le jeu en pause (facultatif)
            Time.timeScale = 0f;

            // Ex : charger la sc�ne suivante
            //SceneManager.LoadScene("Level_2");
        }
    }
}
