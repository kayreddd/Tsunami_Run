using UnityEngine;
using UnityEngine.SceneManagement;

public class WaterRise : MonoBehaviour
{
    [Header("Réglages")]
    [SerializeField] private float riseDuration = 10f;
    [SerializeField] private float riseHeight = 6f;
    [SerializeField] private GameObject gameOverUI;

    private Vector3 startPosition;
    private Vector3 endPosition;
    private float elapsedTime = 0f;
    private bool hasLost = false;
    private bool hasStarted = false;

    void Awake()
    {
        startPosition = transform.position;
        endPosition = startPosition + Vector3.up * riseHeight;
    }

    void OnEnable()
    {
        SceneManager.sceneUnloaded += OnSceneUnloaded;
        ResetWater();
    }

    void OnDisable()
    {
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    private void OnSceneUnloaded(Scene scene)
    {
        // Stopper l'eau quand la scène est quittée
        hasStarted = false;
    }

    void Update()
    {
        if (!hasStarted || hasLost) return;

        elapsedTime += Time.unscaledDeltaTime;
        float t = Mathf.Clamp01(elapsedTime / riseDuration);
        transform.position = Vector3.Lerp(startPosition, endPosition, t);

        if (t >= 1f && !hasLost)
            TriggerGameOver("l’eau a submergé tout le niveau !");
    }

    public void StartWaterRise()
    {
        hasStarted = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasLost)
            TriggerGameOver("le joueur a touché l’eau !");
    }

    private void TriggerGameOver(string reason)
    {
        hasLost = true;
        Debug.Log("Game Over : " + reason);
        if (gameOverUI != null) gameOverUI.SetActive(true);
        PauseGame();
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
    }

    public void RetryLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ResetWater()
    {
        elapsedTime = 0f;
        hasLost = false;
        hasStarted = false;
        transform.position = startPosition;

        if (gameOverUI != null)
            gameOverUI.SetActive(false);
    }
}
