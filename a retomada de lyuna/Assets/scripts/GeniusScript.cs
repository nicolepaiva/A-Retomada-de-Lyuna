using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Genius : MonoBehaviour
{
    [SerializeField] private Button[] botoes;
    [SerializeField] private Button botaoAux;

    private List<int> sequenciaComputador = new List<int>();

    private int indiceJogador = 0;

    public bool botaoComecar = false;

    public float vidaDoInimigo = 10;

    private ButtonControllerScript botao0e1;

    private DoubleButtonControllerScript botao2;

    private void Update(){
    }

    public void Start()
    {
        JogadaComputador();
    }

    private void JogadaComputador()
    {
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

    public void JogadaJogador(int _botaoPressionado) //0 = azul | 1 = amarelo | 2 = laranja | 3 = verde
    {      
        Debug.Log($"{_botaoPressionado} == {sequenciaComputador[indiceJogador]}?");  
        if(_botaoPressionado == sequenciaComputador[indiceJogador])
        {
            Debug.Log("sim");
            indiceJogador++;
            if(indiceJogador >= sequenciaComputador.Count)
            {
                Debug.Log("acertou a sequência");
                botaoAux.Select();
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

}