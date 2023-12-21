using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Genius : MonoBehaviour
{
    [SerializeField] private Button[] botoes;
    [SerializeField] private Button botaoAux;
    [SerializeField] private KeyCode teclaAtivacao0;
    [SerializeField] private KeyCode teclaAtivacao1;
    public VidaScript barra;
    public float vidaDoInimigo = 0;
    public string faseNova;
    public List<int> sequenciaComputador = new List<int>();
    [SerializeField] private GameObject _startingSceneTransition;
    [SerializeField] private GameObject _endingSceneTransition;
    [SerializeField] private Animator objAnimator;

    private int indiceJogador = 0;
    private float tempoDaUltimaTecla0 = -1f;
    private float tempoDaUltimaTecla1 = -1f;
    public float intervaloMax = 0.4f;
    private float ultimoTempo0 = -1f;
    private float ultimoTempo1 = -1f;
    private bool computadorJogando = false;

    void Update()
    {
        if (!computadorJogando) {
            if (Input.GetKeyDown(teclaAtivacao0)) { //verifica se apertou Z
                tempoDaUltimaTecla0 = Time.time; //guarda quando Z foi pressionado
                Debug.Log("apertou Z");
                Debug.Log($"intervalo desde a última tecla: {tempoDaUltimaTecla0 - tempoDaUltimaTecla1}");
            }

            if (Input.GetKeyDown(teclaAtivacao1)) { //verifica se apertou X
                tempoDaUltimaTecla1 = Time.time; //guarda quando X foi pressionado
                Debug.Log("apertou X");
                Debug.Log($"intervalo desde a última tecla: {tempoDaUltimaTecla1 - tempoDaUltimaTecla0}");
            }            
        }
        
    }
     private IEnumerator CarregarFase()
    {
        yield return new WaitForSeconds(1.5f);
        _endingSceneTransition.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(faseNova);
    }

    private void OnEnable()
    {
        InvokeRepeating(nameof(AtivaBotao), 0, intervaloMax); //testa o tempo entre as teclas apertadas pra decidir qual vai ser selecionada
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(AtivaBotao));
    }

    public IEnumerator Start()
    {
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
        OnDisable();
        StartCoroutine(MostraSequencia());
    }

    private IEnumerator MostraSequencia()
    {
        sequenciaComputador.Add(Random.Range(0, 3));

        yield return new WaitForSeconds(2f);
        for (int i = 0; i < sequenciaComputador.Count; i++)
        {
            botoes[sequenciaComputador[i]].Select();
            yield return new WaitForSeconds(0.5f);
            botaoAux.Select();
            yield return new WaitForSeconds(0.2f);
        }
        OnEnable();
        computadorJogando = false;                
    }

    public void JogadaJogador(int _botaoPressionado) //0 = azul | 1 = amarelo | 2 = laranja | 3 = verde
    {   
        StartCoroutine(Sleep(0.2f));
        Debug.Log($"_botaoPressionado: {_botaoPressionado}"); 
        if(_botaoPressionado == sequenciaComputador[indiceJogador])
        {
            indiceJogador++;
            if(indiceJogador >= sequenciaComputador.Count)
            {
                Debug.Log("acertou a sequência");
                indiceJogador = 0;
                vidaDoInimigo += 20;
                barra.AlterarVida(vidaDoInimigo);
                JogadaComputador();
                if(vidaDoInimigo >= 100){
                    objAnimator.Play("animParada");
                    StartCoroutine(CarregarFase());
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

    private void AtivaBotao()
    {       
        if (tempoDaUltimaTecla0 != ultimoTempo0 || tempoDaUltimaTecla1 != ultimoTempo1) { //verifica se algum mudou desde a última vez
            Debug.Log("comparando tempos...");
            ultimoTempo0 = tempoDaUltimaTecla0;
            ultimoTempo1 = tempoDaUltimaTecla1;
            if (tempoDaUltimaTecla0 - tempoDaUltimaTecla1 > intervaloMax) {
                botoes[0].Select();
                botoes[0].onClick.Invoke();
            } else if (tempoDaUltimaTecla1 - tempoDaUltimaTecla0 > intervaloMax) {
                botoes[1].Select();
                botoes[1].onClick.Invoke();
            } else {
                botoes[2].Select();
                botoes[2].onClick.Invoke();
            }
        }
    }

    private IEnumerator Sleep(float time)
    {
        yield return new WaitForSeconds(time);
        botaoAux.Select();
    }

}