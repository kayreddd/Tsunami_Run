using UnityEngine;
using UnityEngine.SceneManagement;

public class WaterRise : MonoBehaviour
{
    [Header("Réglages")]
    [SerializeField] private float riseDuration = 20f; // Temps avant que l'eau atteigne le haut
    [SerializeField] private float riseHeight = 6f;   // Hauteur totale à parcourir
    [SerializeField] private GameObject gameOverUI;    // Popup Game Over

    private Vector3 startPosition;
    private Vector3 endPosition;
    private float elapsedTime = 0f;
    private bool hasLost = false;

    void Start()
    {
        startPosition = transform.position;
        endPosition = new Vector3(startPosition.x, startPosition.y + riseHeight, startPosition.z);
    }

    void Update()
    {
        if (hasLost) return;

        elapsedTime += Time.deltaTime;
        float t = Mathf.Clamp01(elapsedTime / riseDuration);

        // Deplacement progressif vers le haut
        transform.position = Vector3.Lerp(startPosition, endPosition, t);

        // Si l'eau atteint le sommet alors Game Over automatique
        if (t >= 1f && !hasLost)
        {
            hasLost = true;
            Debug.Log(" Game Over : l’eau a submergé tout le niveau !");
            gameOverUI.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasLost)
        {
            hasLost = true;
            Debug.Log(" Game Over : le joueur a touché l’eau !");
            gameOverUI.SetActive(true);
        }
    }

    // Bouton "Relancer"
    public void RetryLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
