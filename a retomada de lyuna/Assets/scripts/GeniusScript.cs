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

    // public float vidaDoInimigo = 0;

    // private void Update(){
    //     if(Input.GetKeyDown(KeyCode.Space)){
    //         JogadaComputador();
    //     }
    // }

    public void Inicia()
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
        if(_botaoPressionado == sequenciaComputador[indiceJogador])
        {
            indiceJogador++;
            if(indiceJogador >= sequenciaComputador.Count)
            {
                botaoAux.Select();
                indiceJogador = 0;
                JogadaComputador();
                // vidaDoInimigo =+ 30;
            }
        }
        else
        {
            Debug.Log("Chama GameOver");
            // vidaDoInimigo = 0;
            // Resetar sequencia
            // sequenciaComputador
        }
    }

}