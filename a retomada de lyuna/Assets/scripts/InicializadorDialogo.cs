using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InicializadorDialogo : MonoBehaviour
{
    [SerializeField] GerenciadorDeDialogo gerenciador;
    [SerializeField] GerenciadorDeDialogo ativar;
    [SerializeField] Dialogo dialogo;

   void Start(){
   }

    // Update is called once per frame
   
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("colidiu");
        if (other.CompareTag("Inicializa")) {
            Inicializa();
        }
    }
    public void Inicializa(){
        if(gerenciador == null){
            return;
        }else{
            gerenciador.Inicializa(dialogo);

        }
    }

    
}
