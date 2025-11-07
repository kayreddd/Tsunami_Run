using UnityEngine;
using UnityEngine.SceneManagement;

public class WaterRise : MonoBehaviour
{
    [Header("Réglages")]
    [SerializeField] private float riseDuration = 10f; // Temps avant que l'eau atteigne le haut
    [SerializeField] private float riseHeight = 6f;   // Hauteur totale à parcourir
    [SerializeField] private GameObject gameOverUI;    // Popup Game Over

    private Vector3 startPosition;
    private Vector3 endPosition;
    private float elapsedTime = 0f;
    private bool hasLost = false;
    private bool hasStarted = false;
    private bool levelFinished = false; // ← Nouvelle variable pour stopper l'eau

    void Start()
    {
        startPosition = transform.position;
        endPosition = new Vector3(startPosition.x, startPosition.y + riseHeight, startPosition.z);
    }

    void Update()
    {
        // L’eau monte seulement si le niveau a commencé, n’est pas perdu et pas fini
        if (!hasStarted || hasLost || levelFinished) return;

        elapsedTime += Time.deltaTime;
        float t = Mathf.Clamp01(elapsedTime / riseDuration);

        transform.position = Vector3.Lerp(startPosition, endPosition, t);

        // Si l'eau atteint le sommet alors Game Over automatique
        if (t >= 1f && !hasLost)
        {
            hasLost = true;
            Debug.Log("Game Over : l’eau a submergé tout le niveau !");
            if (gameOverUI != null)
                gameOverUI.SetActive(true);
            PauseGame();
        }
    }

    public void StartWaterRise()
    {
        hasStarted = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasLost)
        {
            hasLost = true;
            Debug.Log("Game Over : le joueur a touché l’eau !");
            if (gameOverUI != null)
                gameOverUI.SetActive(true);
            PauseGame();
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            var controller = player.GetComponent<PlayerController>();
            if (controller != null) controller.enabled = false;

            var rb = player.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }

        Debug.Log("Jeu mis en pause après Game Over");
    }

    // Bouton "Relancer"
    public void RetryLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Appelle cette fonction quand tu finis le niveau
    public void FinishLevel()
    {
        levelFinished = true;
        Debug.Log("Niveau terminé : l’eau s’arrête !");
    }

    public void StopWater()
    {
        hasStarted = false;
        levelFinished = true;
        Debug.Log("Eau stoppée définitivement !");
    }
}
