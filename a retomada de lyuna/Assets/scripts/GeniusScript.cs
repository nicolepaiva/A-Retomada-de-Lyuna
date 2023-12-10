using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Genius : MonoBehaviour
{
    [SerializeField] private Button[] botoes;
    [SerializeField] private Button botaoAux;
    [SerializeField] private KeyCode teclaAtivacao0;
    [SerializeField] private KeyCode teclaAtivacao1;
    [SerializeField] private Image vidaAnimalImage;
    [SerializeField] private barraDeVida barraDeVida;

    private List<int> sequenciaComputador = new List<int>();
    private int indiceJogador = 0;
    public float vidaDoInimigoAtual = 0;
    private float tempoDaUltimaTecla0 = -1f;
    private float tempoDaUltimaTecla1 = -1f;
    public float intervaloMax = 2f;
    public float inicio = 0;


    void Update()
    {
        if (Input.GetKeyDown(teclaAtivacao0)) { //verifica se apertou Z
            tempoDaUltimaTecla0 = Time.time; //guarda quando Z foi pressionado
            Debug.Log("apertou Z");
            Debug.Log($"intervalo desde a última tecla: {tempoDaUltimaTecla0 - tempoDaUltimaTecla1}");
            if (tempoDaUltimaTecla0 - tempoDaUltimaTecla1 <= intervaloMax) { //verifica quanto tempo faz desde que apertou X
                //se intervalo <= intervalo máximo, então executa o double botão
                botoes[2].Select();
                botoes[2].onClick.Invoke();
            } else {
                //se intervalo > intervalo máximo, então executa Z
                botoes[0].Select();
                botoes[0].onClick.Invoke();
            }
        }

        if (Input.GetKeyDown(teclaAtivacao1)) { //verifica se apertou X
            tempoDaUltimaTecla1 = Time.time; //guarda quando X foi pressionado
            Debug.Log("apertou X");
            Debug.Log($"intervalo desde a última tecla: {tempoDaUltimaTecla1 - tempoDaUltimaTecla0}");
            if (tempoDaUltimaTecla1 - tempoDaUltimaTecla0 <= intervaloMax) { //verifica quanto tempo faz desde que apertou Z
                //se intervalo <= intervalo máximo, então executa o double botão
                botoes[2].Select();
                botoes[2].onClick.Invoke();
            } else {
                //se intervalo > intervalo máximo, então executa X
                botoes[1].Select();
                botoes[1].onClick.Invoke();
            }


        }
    }

    public void Start()
    {
       
        JogadaComputador();

    }

    private void JogadaComputador()
    {
        Debug.Log("jogada computador");
        StartCoroutine(MostraSequencia());
    }

    private IEnumerator MostraSequencia()
    {
        sequenciaComputador.Add(Random.Range(0, 3));

        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < sequenciaComputador.Count; i++)
        {
            botoes[sequenciaComputador[i]].Select();
            yield return new WaitForSeconds(0.5f);
            botaoAux.Select();
            yield return new WaitForSeconds(0.2f);
        }                
    }

    private IEnumerator Sleep(float time){
        yield return new WaitForSeconds(time);
        Debug.Log("zzzzzzzzzzzzzz");
        botaoAux.Select();
    }
   
    public void JogadaJogador(int _botaoPressionado) //0 = azul | 1 = amarelo | 2 = laranja | 3 = verde
    { 
        
        StartCoroutine(Sleep(0.3f));
        Debug.Log($"_botaoPressionado: {_botaoPressionado}");
        Debug.Log($"{_botaoPressionado} == {sequenciaComputador[indiceJogador]}?");
        if(_botaoPressionado == sequenciaComputador[indiceJogador])
        {
            indiceJogador++;
            if(indiceJogador >= sequenciaComputador.Count)
            {
                Debug.Log("acertou a sequência");
                indiceJogador = 0;
                
                JogadaComputador();
            }
        }
        else
        {
            Debug.Log("não: gameover");
            vidaDoInimigoAtual = 0;
            sequenciaComputador.Clear();
            JogadaComputador();
        }

    }
   
}