using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class barraDeVida : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Image vidaAnimalImage;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool AlterarBarraVida(float vidaAtual){
        if(vidaAtual < 1){
            vidaAnimalImage.fillAmount = vidaAtual;
            return true;
        }else{
            return false;
        }
    } 
}
