using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonControllerScript : MonoBehaviour
{

    [SerializeField] private Button botao;
    [SerializeField] private Button doubleBotao;
    [SerializeField] private Button botaoAux;
    [SerializeField] private KeyCode teclaAtivacao;

    [SerializeField] private KeyCode teclaAtivacao2;

    private List<int> botoesPressionados = new List<int>();

    private float tempoDaUltimaTecla = 0f;
    public float intervaloDeEspera = 2f;


    // Update is called once per frame
    void Update()
    {

        // Se Apertou Botão x ou z Guarda quando foi apertado
        // se apertou Botão x ou z e o tempo da ultima teclar for menor que o intevalo
            // =>{SIM} deve emiti double
            // =>{Não} deve emiti single

            /*** PRIMEIRA VEZ ***/
        // Se Apertou botao x ou z e o tempo de intervalo foi execido deve emit single


        if(Input.GetKeyDown(teclaAtivacao)) {
            // Guarda quando foi precionado
            tempoDaUltimaTecla = Time.time;

            // tempoDaUltimaTecla = 10
            // 10 - tempoDaUltimaTecla
            Debug.Log($"tempoDaUltimaTecla => {tempoDaUltimaTecla}");
            Debug.Log($"Time.time => {Time.time}");

        }
        
        if( Time.time > (intervaloDeEspera + tempoDaUltimaTecla)) {
            if(botao != null) {
                Debug.Log($"FOI single = {teclaAtivacao}");
                tempoDaUltimaTecla = 0f;
                // botao.Select();
                // botao.onClick.Invoke();
                
            }
        }

        if(Input.GetKeyDown(teclaAtivacao2)) {
            float intervalo = Time.time - tempoDaUltimaTecla;
            
            if(intervalo < intervaloDeEspera) {
                Debug.Log("FOI Double");
                if(doubleBotao != null) {
                    // doubleBotao.Select();
                    // doubleBotao.onClick.Invoke();
                    // StartCoroutine("Reset");
                    // botaoAux.Select();
                }
            }
             
        }

        // if(Time.time - tempoDaUltimaTecla > intervaloDeEspera) {
        //     tempoDaUltimaTecla = 0f;
        // }
    }

}
