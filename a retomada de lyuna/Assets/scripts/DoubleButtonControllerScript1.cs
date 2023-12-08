using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class DoubleButtonControllerScript : MonoBehaviour
{

    [SerializeField] private Button botao;
    [SerializeField] private Button botaoAux;
    [SerializeField] private KeyCode teclaAtivacao;

    [SerializeField] private KeyCode teclaAtivacao2;

    // Update is called once per frame
    void Update()
    {

        // Logica de 2 Teclas
        /**
        * 1. Identificar a primeira tecla.
        * 2. Guardar em uma variavel.
        * 3. espera Xs dentro desse tempo, caso senha a tecla esperada emite o click.
        * 4. Caso contrario limpa a variavel.
        */
     


        // Verifica se a tecla foi pressionada
        if (Input.GetKeyDown(teclaAtivacao) && Input.GetKeyDown(teclaAtivacao2)){
            // Seleciona e desseleciona o botão
            Debug.Log($"apertou {teclaAtivacao} e {teclaAtivacao2}");
            if (botao != null) {
                botao.Select();
               botao.onClick.Invoke();
               StartCoroutine("Reset");
            }
        } 
        // else if (Input.GetKeyUp(teclaAtivacao) || Input.GetKeyUp(teclaAtivacao2)){
        //     botaoAux.Select();
        // }
    }

    IEnumerator Reset() {
        // your process
        yield return new WaitForSeconds(1);
        botaoAux.Select();
        // continue process
    } 
}
