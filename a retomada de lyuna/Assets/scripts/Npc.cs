using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] DialogoControler dia;
    [TextArea(1,8)]
    [SerializeField] string[] pages;

    

    void Update(){
        
        if(GameManager.Instance.gameSpeed > 0 || GameManager.Instance.distancia >= 500){
            dia?.OpenDialog(pages);
        }
    }

    // Update is called once per frame
}
