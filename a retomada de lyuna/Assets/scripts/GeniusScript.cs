using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Genius : MonoBehaviour
{
    private bool temEsquerda = false;
    private bool temDireita = false;
    [SerializeField] private Button[] buracos;
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
    public string faseNova;
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

    public IEnumerator CarregarFase()
    {
        caixaDiálogo.SetActive(false);
        _endingSceneTransition.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(faseNova);
    }

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
            buracos[sequenciaComputador[i]].Select();
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
        if (!computadorJogando)
        {
            animSpawner.ExibirAnim(_notaTocada);
            buracos[_notaTocada].onClick.Invoke();
            audioSourceFlauta.clip = sonsFlauta[_notaTocada];
            audioSourceFlauta.Play();

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
                        StartCoroutine(AtivaDialogo());
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

    }

    private IEnumerator AtivaDialogo()
    {
        yield return new WaitForSeconds(0.2f);
        dialogueSystem.Next();
    }

    private IEnumerator Sleep(float time)
    {
        yield return new WaitForSeconds(time);
        botaoAux.Select();
    }

}