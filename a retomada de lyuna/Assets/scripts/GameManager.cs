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

    private LyunaScript lyuna;
    private SpawnerScript spawner;

    [Header("Configurações de distância")]
    public float distancia = 0;
    public float aumentoDistancia = 1.0f;

    [Header("Transições")]
    [SerializeField] private GameObject _startingSceneTransition;
    [SerializeField] private GameObject _endingSceneTransition;

    public Button btnJump;

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
        btnJump.gameObject.SetActive(true);
        enabled = false;
        Debug.Log("COMEÇOU");
        _startingSceneTransition.SetActive(true);

        lyuna = FindObjectOfType<LyunaScript>();
        spawner = FindObjectOfType<SpawnerScript>();

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
        btnJump.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.8f);
        gameSpeed = initialGameSpeed;
        enabled = true;
        DisableStartingSceneTransition();
    }

    public void GameOver()
    {
        gameSpeed = 0f;
        
        lyuna.gameObject.SetActive(false);
        btnJump.gameObject.SetActive(false);
        spawner.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);

        deuGameOver = true;
        Debug.Log($"deu game over? {deuGameOver}");
    }

    private void Update()
    {
        //Debug.Log("tá na tela de game over?");
        if (!deuGameOver) 
        {
            //Debug.Log("não");
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
            }
        }
        else 
        {
           // Debug.Log("sim");
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