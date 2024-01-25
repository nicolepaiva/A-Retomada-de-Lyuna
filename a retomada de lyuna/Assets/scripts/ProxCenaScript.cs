using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProxCenaScript : MonoBehaviour
{
    public string faseNova;

    private IEnumerator CarregarFase()
    {
        yield return new WaitForSeconds(52f);
        Debug.Log("carregando fase");
        SceneManager.LoadScene(faseNova);
    }

    void Start()
    {
        StartCoroutine(CarregarFase());
    }
}
