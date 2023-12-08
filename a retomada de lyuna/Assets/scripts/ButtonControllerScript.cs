using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class ButtonControllerScript : MonoBehaviour
{

    [SerializeField] private Button botao;
    [SerializeField] private Button botaoAux;
    [SerializeField] private KeyCode teclaAtivacao;

    [SerializeField] private KeyCode teclaAtivacao2;

    // Update is called once per frame
    void Update()
    {
        // Verifica se a tecla foi pressionada
        if (Input.GetKeyDown(teclaAtivacao) && !Input.GetKeyDown(teclaAtivacao2)){
            // Seleciona e desseleciona o botão
            Debug.Log($"apertou {teclaAtivacao}");
            if (botao != null) {
                botao.Select();
                botao.onClick.Invoke();
            }
        } else if (Input.GetKeyUp(teclaAtivacao)){
            botaoAux.Select();
        }
    }

}
