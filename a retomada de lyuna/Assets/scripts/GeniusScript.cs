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

    private List<int> sequenciaComputador = new List<int>();

    private int indiceJogador = 0;
    public float vidaDoInimigo = 10;
    private float tempoDaUltimaTecla0 = -1f;
    private float tempoDaUltimaTecla1 = -1f;
    public float intervaloMax = 0.4f;
    private float ultimoTempo0 = -1f;
    private float ultimoTempo1 = -1f;

    void Update()
    {
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

    private void OnEnable()
    {
        Invoke(nameof(AtivaBotao), intervaloMax); //testa o tempo entre as teclas apertadas pra decidir qual vai ser selecionada
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    public void Start()
    {
        JogadaComputador();
    }

    private void JogadaComputador()
    {
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
                vidaDoInimigo += 10;
                JogadaComputador();
            }
        }
        else
        {
            Debug.Log("não: gameover");
            vidaDoInimigo = 0;
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
        Invoke(nameof(AtivaBotao), intervaloMax);
    }

    private IEnumerator Sleep(float time){
        yield return new WaitForSeconds(time);
        botaoAux.Select();
    }
}