using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogoControler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float tempoLetra;
    [SerializeField] TextMeshProUGUI textDialog;
    private bool finalPagesDialogo;
    private IEnumerator routineDitar;
    private string[] pages;
    private int currentPages;
    void Awake(){
        routineDitar = Ditar();
    }
    void Update(){
        if(Input.GetKeyDown("z")){
            proximaPagina();
        }
    }

    void proximaPagina(){
        if(finalPagesDialogo == true){
            currentPages++;
            if(currentPages >= pages.Length){
                EndDialogo();
                return;
            }
            routineDitar = Ditar();
            StartCoroutine(routineDitar);
        }else{
            finalPagesDialogo = true;
            textDialog.text = pages[currentPages];
            StopCoroutine(routineDitar);
        }
    }
    
        void EndDialogo(){
        StopCoroutine(routineDitar);
        gameObject.SetActive(false);
    }
    public void OpenDialog(string[] pages){
        if(gameObject.activeInHierarchy)
            return;
        gameObject.SetActive(true);
        this.pages = pages;
        currentPages = 0;
        finalPagesDialogo = false;
        routineDitar = Ditar();
        StartCoroutine(routineDitar);
    }

    IEnumerator Ditar(){
        var page = pages[currentPages];
        textDialog.text = "";
        finalPagesDialogo = false;
        foreach (var letter in page){
            textDialog.text += letter;
            yield return new WaitForSeconds(tempoLetra);
        }
        finalPagesDialogo = true;
    }
    
   
}
