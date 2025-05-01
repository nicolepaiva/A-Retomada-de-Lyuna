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
    public string nomeCenaCreditos; // Nome da cena de créditos
    public string nomeCenaMenu; // Nome da cena do menu principal

    // Método chamado ao clicar no botão "Iniciar Jogo"
    public void Awake()
    {
        loadingScreen.SetActive(false);
    }
    public void IniciarJogo()
    {
        StartCoroutine(CarregarCenaAsync(nomeCenaJogo)); // Substitua pelo nome da cena do jogo
    }

    // Método chamado ao clicar no botão "Créditos"
    public void AbrirCreditos()
    {
        // Aqui você pode carregar uma cena de créditos ou exibir um painel de créditos
        SceneManager.LoadScene(nomeCenaCreditos); // Substitua pelo nome da cena de créditos
    }

    // Método chamado ao clicar no botão "Fechar Aplicativo"
    public void FecharAplicativo()
    {
        Application.Quit();
        Debug.Log("Aplicativo fechado."); // Apenas para testes no editor
    }

    // Coroutine para carregar a cena de forma assíncrona
    private IEnumerator CarregarCenaAsync(string nomeCena)
    {
        // Exibe a tela de carregamento
        loadingScreen.SetActive(true);

        // Inicia o carregamento da cena
        AsyncOperation operacao = SceneManager.LoadSceneAsync(nomeCena);

        // Enquanto a cena não estiver carregada, atualiza a barra de progresso
        while (!operacao.isDone)
        {
            float progresso = Mathf.Clamp01(operacao.progress / 0.9f);
            loadingBar.value = progresso;
            yield return null;
        }
    }
}
