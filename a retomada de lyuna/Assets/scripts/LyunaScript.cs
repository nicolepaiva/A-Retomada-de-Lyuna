using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LyunaScript : MonoBehaviour
{
    public GameManager GameManager;
    public Rigidbody2D rigidBody;
    public Animator objAnimator;
    public string faseNova;
    

    [Header("Movimentação")]
    public float velocidade;


    [Header("Pulo")]
    public bool jogadorEstaTocandoNoChao;
    public float alturaPulo;
    private bool podePular;
    public Transform verificadorDeChao;

    //public bool ButtonAndarPressed = false;

    public float tamanhoDoVerificadorDeChao;
    public LayerMask camadaDoChao;


    private void FixedUpdate()
    {
        jogadorEstaTocandoNoChao = Physics2D.OverlapCircle(
            verificadorDeChao.position,
            tamanhoDoVerificadorDeChao,
            camadaDoChao
        );
        if (jogadorEstaTocandoNoChao)
        {
            podePular = true;
            objAnimator.SetBool("pulando", false);
        }
        else
        {
            podePular = false;
            objAnimator.SetBool("pulando", true);
        }
        MovimentoJogador();
        Walk();
    }
    public void Walk()
    {
        float botaoInput = MobileInputManager.Instance.GetHorizontalInput();
        Vector3 movimento = new Vector3(botaoInput, 0f, 0f) * velocidade * Time.deltaTime;
        transform.Translate(movimento);
        if(botaoInput > 0)
        {
            objAnimator.SetBool("andando", true);
            objAnimator.SetBool("idle", false);
        }
    }

    private void MovimentoJogador()
    {
        float movimentoHorizontal = Input.GetAxis("Horizontal");
        float eixoX = movimentoHorizontal * velocidade;

        rigidBody.velocity = new Vector2(eixoX, rigidBody.velocity.y);

        if (jogadorEstaTocandoNoChao)
        {
            if (GameManager.Instance.gameSpeed != 0 || movimentoHorizontal != 0)
            {
                objAnimator.SetBool("idle", false);
                objAnimator.SetBool("andando", true);
            }
            else
            {
                objAnimator.SetBool("idle", true);
                objAnimator.SetBool("andando", false);
            }
        }


        if (GameManager.Instance.gameSpeed == 0)
        {
            velocidade = 12;
        }
        else
        {
            velocidade = 5;
        }

        if (movimentoHorizontal > 0 || GameManager.Instance.gameSpeed > 0)
        {
            transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        }
        else if (movimentoHorizontal < 0 && GameManager.Instance.gameSpeed == 0)
        {
            objAnimator.SetBool("idle", true);
            transform.localScale = new Vector3(-0.4f, 0.4f, 0.4f);
        }
    }
    public void JumpWithButton()
    {
        if (jogadorEstaTocandoNoChao && podePular)
        {
            podePular = false;
            jogadorEstaTocandoNoChao = false;
            
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0f);
            Debug.Log("pulando");
            rigidBody.AddForce(new Vector2(0f, alturaPulo), ForceMode2D.Impulse);
            objAnimator.SetBool("pulando", true);
        }
    }
    private IEnumerator CarregarBatalha()
    {
        GameManager.Instance.EndingSceneTransition();
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(faseNova);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            GameManager.Instance.GameOver();
            
        }
        else if (other.CompareTag("Enemy"))
        {
            StartCoroutine(CarregarBatalha());
        }
       
    }
}