using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animSpawnerScript : MonoBehaviour
{
    [SerializeField] private GameObject animCanaime;
    [SerializeField] private GameObject animMakunaima;
    [SerializeField] private GameObject animDupla;
    public void ExibirAnim (int animEscolha) {
        switch (animEscolha)
        {
            case 0:
                GameObject anim0 = Instantiate(animMakunaima, new Vector3(131.2f, -172.8f, 0f), Quaternion.identity);
                anim0.transform.SetParent (GameObject.FindGameObjectWithTag("Canvas").transform, false);
                break;
            case 1:
                GameObject anim1 = Instantiate(animCanaime, new Vector3(497.6f, -172.8f, 0f), Quaternion.identity);
                anim1.transform.SetParent (GameObject.FindGameObjectWithTag("Canvas").transform, false);
                break;
            case 2:
                GameObject anim2 = Instantiate(animDupla, new Vector3(315.6f, -172.8f, 0f), Quaternion.identity);
                anim2.transform.SetParent (GameObject.FindGameObjectWithTag("Canvas").transform, false);
                break;
        }
    }
}
