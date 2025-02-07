using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject boss;

    private bool chamouBoss = false;

    [Header("Configurações de velocidade")]
    public float initialGameSpeed = 5f;
    public float gameSpeedIncrease = 0.1f;
    [SerializeField] public float gameSpeed = 0;

    [Header("UI")]
    public TextMeshProUGUI gameOverText;
    public Button retryButton;
    public Button jumpButton;
    public Button andarButton;

    private LyunaScript lyuna;
    private SpawnerScript spawner;

    [Header("Configurações de distância")]
    public float distancia = 0;
    public float aumentoDistancia = 1.0f;

    [Header("Transições")]
    [SerializeField] private GameObject _startingSceneTransition;
    [SerializeField] private GameObject _endingSceneTransition;

    private bool deuGameOver = false;
    
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
    private void Start()
    {
        enabled = false;
        Debug.Log("COMEÇOU");
        _startingSceneTransition.SetActive(true);

        lyuna = FindObjectOfType<LyunaScript>();
        spawner = FindObjectOfType<SpawnerScript>();

        jumpButton.gameObject.SetActive(true);
        andarButton.gameObject.SetActive(false);
        StartCoroutine(NewGame());
    }

    private void DisableStartingSceneTransition()
    {
        _startingSceneTransition.SetActive(false);
    }

    public void EndingSceneTransition()
    {
        _endingSceneTransition.SetActive(true);

    }

    public IEnumerator NewGame()
    {
        gameOverText.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.8f);
        gameSpeed = initialGameSpeed;
        enabled = true;
        jumpButton.gameObject.SetActive(true);
        DisableStartingSceneTransition();
        andarButton.gameObject.SetActive(false);
    }

    public void GameOver()
    {
        gameSpeed = 0f;
        jumpButton.gameObject.SetActive(false);
        lyuna.gameObject.SetActive(false);
        spawner.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);
        andarButton.gameObject.SetActive(false);
        deuGameOver = true;
        Debug.Log($"deu game over? {deuGameOver}");
    }

    private void Update()
    {
        Debug.Log("tá na tela de game over?");
        if (!deuGameOver) 
        {
            Debug.Log("não");
            distancia += aumentoDistancia * Time.deltaTime;
            if (distancia < 468)
            {
                gameSpeed += gameSpeedIncrease * Time.deltaTime;
            }
            else if (distancia < 500)
            {
                if (!chamouBoss)
                {
                    
                    Instantiate(boss);
                    chamouBoss = true;
                    
                }
                spawner.enabled = false;
            }
            else
            {
                gameSpeed = 0;
                aumentoDistancia = 0;
                jumpButton.gameObject.SetActive(false);
                andarButton.gameObject.SetActive(true);
            }
        }
        else 
        {
            Debug.Log("sim");
            if (Input.GetKeyDown(KeyCode.Space)) {
                Debug.Log("apertou espaço");
                retryButton.Select();
            }
            if (Input.GetKeyUp(KeyCode.Space)) {
                retryButton.onClick.Invoke();
            }
        } 
    }
}