using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPrincipal : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject loadingScreen; // Tela de carregamento
    public Slider loadingBar; // Barra de progresso do carregamento

    public string nomeCenaJogo; // Nome da cena do jogo
    public string nomeCenaCreditos; // Nome da cena de cr�ditos
    public string nomeCenaMenu; // Nome da cena do menu principal

    // M�todo chamado ao clicar no bot�o "Iniciar Jogo"
    public void Awake()
    {
        loadingScreen.SetActive(false);
    }
    public void IniciarJogo()
    {
        StartCoroutine(CarregarCenaAsync(nomeCenaJogo)); // Substitua pelo nome da cena do jogo
    }

    // M�todo chamado ao clicar no bot�o "Cr�ditos"
    public void AbrirCreditos()
    {
        // Aqui voc� pode carregar uma cena de cr�ditos ou exibir um painel de cr�ditos
        SceneManager.LoadScene(nomeCenaCreditos); // Substitua pelo nome da cena de cr�ditos
    }

    // M�todo chamado ao clicar no bot�o "Fechar Aplicativo"
    public void FecharAplicativo()
    {
        Application.Quit();
        Debug.Log("Aplicativo fechado."); // Apenas para testes no editor
    }

    // Coroutine para carregar a cena de forma ass�ncrona
    private IEnumerator CarregarCenaAsync(string nomeCena)
    {
        // Exibe a tela de carregamento
        loadingScreen.SetActive(true);

        // Inicia o carregamento da cena
        AsyncOperation operacao = SceneManager.LoadSceneAsync(nomeCena);

        // Enquanto a cena n�o estiver carregada, atualiza a barra de progresso
        while (!operacao.isDone)
        {
            float progresso = Mathf.Clamp01(operacao.progress / 0.9f);
            loadingBar.value = progresso;
            yield return null;
        }
    }
}
