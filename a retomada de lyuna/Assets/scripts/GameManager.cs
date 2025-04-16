using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject boss;
    public int recordeJogo;
    public int distanciaPercorrida;
    public TextMeshProUGUI tempoFaseTxt;
    public TextMeshProUGUI pontuacaoAtualTxt;
    public TextMeshProUGUI recordeTxt;

    public int distanciaBoss = 500;
    public Image barraProgresso;

    private bool chamouBoss = false;

    [Header("Configurações de velocidade")]
    public float initialGameSpeed = 5f;
    public float gameSpeedIncrease = 0.1f;
    [SerializeField] public float gameSpeed = 0;

    [Header("UI")]
    public GameObject painelGameOver;
    //public TextMeshProUGUI gameOverText;
    //public Button retryButton;


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
        lyuna = FindObjectOfType<LyunaScript>();
        spawner = FindObjectOfType<SpawnerScript>();
        StartGame();
    }

    public void StartGame()
    {
        _startingSceneTransition.SetActive(true);
        spawner.gameObject.SetActive(true);
        gameSpeed = initialGameSpeed;
        DeleteAllChildren(spawner.obstaculosRoot.transform);
        lyuna.gameObject.SetActive(true);
        painelGameOver.SetActive(false);
        deuGameOver = false;
        StartCoroutine(NewGame());
    }
    public void DeleteAllChildren(Transform transform)
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
    private void DisableStartingSceneTransition()
    {
        _startingSceneTransition.SetActive(false);
    }

    public void EndingSceneTransition()
    {
        _endingSceneTransition.SetActive(true);

    }

    public void ApresentarPontuacao()
    {
        tempoFaseTxt.enabled = false;

        pontuacaoAtualTxt.text = distanciaPercorrida + "m";
        if(distanciaPercorrida > recordeJogo)
        {
            recordeJogo = distanciaPercorrida;
        }
        recordeTxt.text = "recorde atual: "+recordeJogo + "m";
        
    }

    public IEnumerator NewGame()
    {
        painelGameOver.SetActive(false);
        tempoFaseTxt.enabled = true;
        yield return new WaitForSeconds(0.8f);
        gameSpeed = initialGameSpeed;
        enabled = true;
        jumpButton.gameObject.SetActive(true);
        DisableStartingSceneTransition();
        andarButton.gameObject.SetActive(false);
        barraProgresso.fillAmount = 0;
        lyuna.objAnimator.SetBool("morrendo", false);
    }

    public IEnumerator GameOver()
    {
        gameSpeed = 0f;
        
        jumpButton.gameObject.SetActive(false);
        spawner.gameObject.SetActive(false);
        
        andarButton.gameObject.SetActive(false);
        deuGameOver = true;
        
        distancia = 0f;
        yield return new WaitForSeconds(3.0f);
        painelGameOver.SetActive(true);
        lyuna.objAnimator.SetBool("morrendo", false);
        lyuna.gameObject.SetActive(false);
        ApresentarPontuacao();
    }

    private void Update()
    {
        if (!deuGameOver) 
        {
            barraProgresso.fillAmount = Mathf.Clamp01(distancia / distanciaBoss);

            distanciaPercorrida = (int)distancia / 2;
            tempoFaseTxt.text = distanciaPercorrida +"m";
            distancia += aumentoDistancia * Time.deltaTime;
            if (distancia < 468)
            {
                gameSpeed += gameSpeedIncrease * Time.deltaTime;
            }
            else if (distancia < distanciaBoss)
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
                tempoFaseTxt.enabled = false;
                andarButton.gameObject.SetActive(true);
                lyuna.objAnimator.SetBool("pegando_flauta", true);
                
            }
        }
    }
}