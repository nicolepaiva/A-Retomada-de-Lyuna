using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonControllerScript : MonoBehaviour
{

    [SerializeField] private Button botao;
    [SerializeField] private KeyCode teclaAtivacao;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Verifica se a tecla 'X' foi pressionada
        if (Input.GetKeyDown(teclaAtivacao)){
            // Seleciona o botão
            if (botao != null)
            {
                botao.Select();
                botao.onClick.Invoke();

            }
        }
   
    }
}
