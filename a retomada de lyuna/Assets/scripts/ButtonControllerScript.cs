using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonControllerScript : MonoBehaviour
{

    [SerializeField] private Button botao0;
    [SerializeField] private Button botao1;
    [SerializeField] private Button doubleBotao;
    [SerializeField] private Button botaoAux;
    [SerializeField] private KeyCode teclaAtivacao0;
    [SerializeField] private KeyCode teclaAtivacao1;

    private float tempoDaUltimaTecla0 = 0f;
    private float tempoDaUltimaTecla1 = 0f;
    public float intervaloMax = 2f;


    void Update()
    {
        if (Input.GetKeyDown(teclaAtivacao0)) { //verifica se apertou Z
            tempoDaUltimaTecla0 = Time.time; //guarda quando Z foi pressionado
            Debug.Log($"tempoDaUltimaTecla0 => {tempoDaUltimaTecla0}");
            if (tempoDaUltimaTecla0 - tempoDaUltimaTecla1 <= intervaloMax) { //verifica quanto tempo faz desde que apertou X
                //se intervalo <= intervalo máximo, então executa o double botão
                doubleBotao.Select();
                doubleBotao.onClick.Invoke();
            } else {
                //se intervalo > intervalo máximo, então executa Z
                botao0.Select();
                botao0.onClick.Invoke();
            }
            StartCoroutine(Sleep(1f));
        }

        if (Input.GetKeyDown(teclaAtivacao1)) { //verifica se apertou X
            tempoDaUltimaTecla1 = Time.time; //guarda quando X foi pressionado
            Debug.Log($"tempoDaUltimaTecla1 => {tempoDaUltimaTecla1}");
            if (tempoDaUltimaTecla1 - tempoDaUltimaTecla0 <= intervaloMax) { //verifica quanto tempo faz desde que apertou Z
                //se intervalo <= intervalo máximo, então executa o double botão
                doubleBotao.Select();
                doubleBotao.onClick.Invoke();
            } else {
                //se intervalo > intervalo máximo, então executa X
                botao1.Select();
                botao1.onClick.Invoke();
            }
            StartCoroutine(Sleep(1f));
        }
    }

    private IEnumerator Sleep(float time){
        yield return new WaitForSeconds(time);
        botaoAux.Select();
    }

}
