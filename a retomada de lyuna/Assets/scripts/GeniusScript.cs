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
    public GameObject flautaFrente;
    public GameObject barraVidaObjeto;
    public VidaScript barra;
    public float vidaDoInimigo = 0;
    // public string faseNova;
    public List<int> sequenciaComputador = new List<int>();
    [SerializeField] private GameObject _startingSceneTransition;
    [SerializeField] private GameObject _endingSceneTransition;
    [SerializeField] private Animator objAnimator;
    public animSpawnerScript animSpawner;

    private int indiceJogador = 0;
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
                        if (touchLeft.phase == TouchPhase.Began) {
                            temEsquerda = true;
                        }
                    } else {
                        touchRight = theTouch;
                        if (touchRight.phase == TouchPhase.Began) {
                            temDireita = true;
                        }
                    }
                }
            
                if (temEsquerda && temDireita && ((touchLeft.phase == TouchPhase.Stationary && touchRight.phase == TouchPhase.Ended) || (touchLeft.phase == TouchPhase.Ended && touchRight.phase == TouchPhase.Stationary) || (touchLeft.phase == TouchPhase.Ended && touchRight.phase == TouchPhase.Ended))) { //verifica se apertou dos dois lados ao mesmo tempo
                    // Debug.Log("touchLeft: " + touchLeft.phase + ", touchRight: " + touchRight.phase);
                    // Debug.Log("vc apertou dois botões");
                    temEsquerda = false;
                    temDireita = false;
                    animSpawner.ExibirAnim(2);
                    botoes[2].onClick.Invoke();
                    audioSourceFlauta.clip = sonsFlauta[2];
                    audioSourceFlauta.Play();
                } else if (temEsquerda && touchLeft.phase == TouchPhase.Ended) { //verifica se apertou do lado esquerdo
                    // Debug.Log("touchLeft: " + touchLeft.phase + ", touchRight: " + touchRight.phase);
                    // Debug.Log("vc apertou esquerda");
                    temEsquerda = false;
                    temDireita = false;
                    animSpawner.ExibirAnim(0);
                    botoes[0].onClick.Invoke();
                    audioSourceFlauta.clip = sonsFlauta[0];
                    audioSourceFlauta.Play();
                } else if (temDireita && touchRight.phase == TouchPhase.Ended) { //verifica se apertou do lado direito
                    // Debug.Log("touchLeft: " + touchLeft.phase + ", touchRight: " + touchRight.phase);
                    // Debug.Log("vc apertou direita");
                    temEsquerda = false;
                    temDireita = false;
                    animSpawner.ExibirAnim(1);;
                    botoes[1].onClick.Invoke();
                    audioSourceFlauta.clip = sonsFlauta[1];
                    audioSourceFlauta.Play();
                } 
            }     
        }
    }
    // public IEnumerator CarregarFase()
    // {
    //     caixaDiálogo.SetActive(false);
    //     _endingSceneTransition.SetActive(true);
    //     yield return new WaitForSeconds(1.5f);
    //     SceneManager.LoadScene(faseNova);
    // }

    public IEnumerator Start()
    {
        Debug.Log($"computadorJogando = {computadorJogando}");
        screenWidth = Screen.width;
        _startingSceneTransition.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        _startingSceneTransition.SetActive(false);
        objAnimator.Play("animHarpiaVooBraba");
        JogadaComputador();
    }

    private void JogadaComputador()
    {
        computadorJogando = true;
        Debug.Log($"computadorJogando = {computadorJogando}");
        // Debug.Log("jogada computador");
        StartCoroutine(MostraSequencia());
    }

    private IEnumerator MostraSequencia()
    {
        sequenciaComputador.Add(Random.Range(0, 3));

        string exibirSequencia = "sequência: ";
        foreach (var x in sequenciaComputador) {
            exibirSequencia += x.ToString() + " - ";
        }
        Debug.Log(exibirSequencia);

        yield return new WaitForSeconds(2f);
        for (int i = 0; i < sequenciaComputador.Count; i++)
        {
            botoes[sequenciaComputador[i]].Select();
            // switch (sequenciaComputador[i])
            // {
            // case 0:
            //     Instantiate(animMakunaima);
            //     break;
            // case 1:
            //     Instantiate(animCanaime);
            //     break;
            // case 2:
            //     break;
            // }
            Debug.Log($"computadorJogando = {computadorJogando}");
            audioSourceFlauta.clip = sonsFlauta[sequenciaComputador[i]];
            audioSourceFlauta.Play();
            yield return new WaitForSeconds(0.5f);
            botaoAux.Select();
            yield return new WaitForSeconds(0.2f);
        }
        computadorJogando = false;
        Debug.Log($"computadorJogando = {computadorJogando}");                
    }

    public void JogadaJogador(int _notaTocada) //0 = vermelho | 1 = azul | 2 = vermelho + azul
    {

        StartCoroutine(Sleep(0.2f));
        // Debug.Log($"_botaoPressionado: {_notaTocada}"); 
        if(_notaTocada == sequenciaComputador[indiceJogador])
        {
            indiceJogador++;
            if(indiceJogador >= sequenciaComputador.Count)
            {
                // Debug.Log("acertou a sequência");
                indiceJogador = 0;
                vidaDoInimigo += 20;
                barra.AlterarVida(vidaDoInimigo);
                if(vidaDoInimigo >= 100){
                    objAnimator.Play("animHarpiaIdleMansa");
                    computadorJogando = true;
                    Debug.Log($"computadorJogando = {computadorJogando}");
                    flautaFrente.SetActive(false);
                    caixaDiálogo.SetActive(true);
                    dialogueSystem.Next();
                    leftButton.gameObject.SetActive(false);
                    rightButton.gameObject.SetActive(false);
                } else {
                    JogadaComputador();
                }
            }
        }
        else
        {
            // Debug.Log("não: gameover");
            indiceJogador = 0;
            vidaDoInimigo = 0;
            barra.AlterarVida(vidaDoInimigo);
            sequenciaComputador.Clear();
            JogadaComputador();
        }
    }

    private IEnumerator Sleep(float time)
    {
        yield return new WaitForSeconds(time);
        botaoAux.Select();
    }

}