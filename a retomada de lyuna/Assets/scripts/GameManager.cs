using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [field: Header("Configurações de velocidade")]
    public float initialGameSpeed = 5f;
    public float gameSpeedIncrease = 0.1f;
    [field: SerializeField] public float gameSpeed { get; private set; }

    public TextMeshProUGUI gameOverText;
    public Button retryButton;

    private LyunaScript lyuna;
    private SpawnerScript spawner;


    private void Awake()
    {
        if (Instance == null) {
            Instance = this;
        } else {
            DestroyImmediate(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this) {
            Instance = null;
        }
    }
    private void Start()
    {
        lyuna = FindObjectOfType<LyunaScript>();
        spawner = FindObjectOfType<SpawnerScript>();

        NewGame();
    }

    public void NewGame()
    {
        ObstacleScript[] obstacles = FindObjectsOfType<ObstacleScript>();

        foreach (var obstacle in obstacles) {
            Destroy(obstacle.gameObject);
        }
        
        gameSpeed = initialGameSpeed;
        enabled = true;

        lyuna.gameObject.SetActive(true);
        spawner.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);
    }

    public void GameOver()
    {
        gameSpeed = 0f;
        enabled = false;

        lyuna.gameObject.SetActive(false);
        spawner.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);
    }

    private void Update()
    {
        gameSpeed += gameSpeedIncrease * Time.deltaTime;
    }
}