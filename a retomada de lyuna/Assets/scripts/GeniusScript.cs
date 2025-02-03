using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Genius : MonoBehaviour
{
    private bool temEsquerda = false;
    private bool temDireita = false;
    [SerializeField] private Button[] botoes;
    [SerializeField] private Button botaoAux;
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;
    private Touch touchLeft;
    private Touch touchRight;
    private Touch theTouch;

    private float screenWidth;

    public GameObject caixaDiálogo;
    public VidaScript barra;
    public float vidaDoInimigo = 0;
    // public string faseNova;
    public List<int> sequenciaComputador = new List<int>();
    [SerializeField] private GameObject _startingSceneTransition;
    [SerializeField] private GameObject _endingSceneTransition;
    [SerializeField] private Animator objAnimator;

    private int indiceJogador = 0;
    private float tempoDaUltimaTecla01 = -1f;
    [SerializeField] private float intervaloMin = 0.3f;
    private bool computadorJogando = false;

    [Header("Efeitos Sonoros")]
    public AudioSource audioSourceFlauta;
    public AudioClip[] sonsFlauta;

    DialogueSystem dialogueSystem;

    void Awake()
    {
        dialogueSystem = FindObjectOfType<DialogueSystem>();
    }

    void Update()
    {
        if (!computadorJogando) {
            if (Input.touchCount > 0) {
                for (int i = 0; i < Input.touchCount; i++) {
                    theTouch = Input.GetTouch(i);
                    if (theTouch.position.x < screenWidth/2) {
                        touchLeft = theTouch;
                        temEsquerda = true;
                    } else {
                        touchRight = theTouch;
                        temDireita = true;
                    }
                }
            
                if (temEsquerda && temDireita && ((touchLeft.phase == TouchPhase.Stationary && touchRight.phase == TouchPhase.Ended) || (touchLeft.phase == TouchPhase.Ended && touchRight.phase == TouchPhase.Stationary) || (touchLeft.phase == TouchPhase.Ended && touchRight.phase == TouchPhase.Ended))) { //verifica se apertou dos dois lados ao mesmo tempo
                    tempoDaUltimaTecla01 = Time.time;
                    Debug.Log("touchLeft: " + touchLeft.phase + ", touchRight: " + touchRight.phase);
                    Debug.Log("vc apertou dois botões");
                    botoes[2].Select();
                    botoes[2].onClick.Invoke();
                    audioSourceFlauta.clip = sonsFlauta[2];
                    audioSourceFlauta.Play();
                    touchLeft.phase = TouchPhase.Canceled;
                    touchRight.phase = TouchPhase.Canceled;
                    Debug.Log("CANCELADOS? touchLeft: " + touchLeft.phase + ", touchRight: " + touchRight.phase);
                } else if (touchLeft.phase == TouchPhase.Ended && Time.time - tempoDaUltimaTecla01 > intervaloMin) { //verifica se apertou do lado esquerd0
                    Debug.Log("touchLeft: " + touchLeft.phase + ", touchRight: " + touchRight.phase);
                    Debug.Log("vc apertou esquerda");
                    botoes[0].Select();
                    botoes[0].onClick.Invoke();
                    audioSourceFlauta.clip = sonsFlauta[0];
                    audioSourceFlauta.Play();
                    touchLeft.phase = TouchPhase.Canceled;
                    Debug.Log("CANCELADOS? touchLeft: " + touchLeft.phase + ", touchRight: " + touchRight.phase);
                } else if (touchRight.phase == TouchPhase.Ended && Time.time - tempoDaUltimaTecla01 > intervaloMin) { //verifica se apertou do lado direito
                    Debug.Log("touchLeft: " + touchLeft.phase + ", touchRight: " + touchRight.phase);
                    Debug.Log("vc apertou direita");
                    botoes[1].Select();
                    botoes[1].onClick.Invoke();
                    audioSourceFlauta.clip = sonsFlauta[1];
                    audioSourceFlauta.Play();
                    touchRight.phase = TouchPhase.Canceled;
                    Debug.Log("CANCELADOS? touchLeft: " + touchLeft.phase + ", touchRight: " + touchRight.phase);
                } 
            }

            temEsquerda = false;
            temDireita = false;        
        }
    }
    // public IEnumerator CarregarFase()
    // {
    //     caixaDiálogo.SetActive(false);
    //     objAnimator.Play("animParadaMansa");
    //     _endingSceneTransition.SetActive(true);
    //     yield return new WaitForSeconds(1.5f);
    //     SceneManager.LoadScene(faseNova);
    // }

    public IEnumerator Start()
    {
        screenWidth = Screen.width;
        objAnimator.Play("animParada");
        _startingSceneTransition.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        _startingSceneTransition.SetActive(false);
        objAnimator.Play("animBraba");
        JogadaComputador();
    }

    private void JogadaComputador()
    {
        computadorJogando = true;
        Debug.Log("jogada computador");
        StartCoroutine(MostraSequencia());
    }

    private IEnumerator MostraSequencia()
    {
        sequenciaComputador.Add(Random.Range(0, 3));

        yield return new WaitForSeconds(2f);
        for (int i = 0; i < sequenciaComputador.Count; i++)
        {
            botoes[sequenciaComputador[i]].Select();
            audioSourceFlauta.clip = sonsFlauta[sequenciaComputador[i]];
            audioSourceFlauta.Play();
            yield return new WaitForSeconds(0.5f);
            botaoAux.Select();
            yield return new WaitForSeconds(0.2f);
        }
        computadorJogando = false;                
    }

    public void JogadaJogador(int _notaTocada) //0 = vermelho | 1 = azul | 2 = vermelho + azul
    {     
        StartCoroutine(Sleep(0.2f));
        Debug.Log($"_botaoPressionado: {_notaTocada}"); 
        if(_notaTocada == sequenciaComputador[indiceJogador])
        {
            indiceJogador++;
            if(indiceJogador >= sequenciaComputador.Count)
            {
                Debug.Log("acertou a sequência");
                indiceJogador = 0;
                vidaDoInimigo += 20;
                barra.AlterarVida(vidaDoInimigo);
                if(vidaDoInimigo >= 100){
                    objAnimator.Play("animMansa");
                    caixaDiálogo.SetActive(true);
                    dialogueSystem.Next();
                    computadorJogando = true;
                    leftButton.gameObject.SetActive(false);
                    rightButton.gameObject.SetActive(false);
                } else {
                    JogadaComputador();
                }
            }
        }
        else
        {
            Debug.Log("não: gameover");
            indiceJogador = 0;
            vidaDoInimigo = 0;
            barra.AlterarVida(vidaDoInimigo);
            sequenciaComputador.Clear();
            JogadaComputador();
        }
    }

    public void DesativarBotao () {
        Debug.Log("desativar botao");
        botaoAux.Select();
    }
    private IEnumerator Sleep(float time)
    {
        yield return new WaitForSeconds(time);
        botaoAux.Select();
    }

}