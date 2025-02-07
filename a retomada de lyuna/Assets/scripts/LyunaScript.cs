using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LyunaScript : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public Animator objAnimator;
    public string faseNova;
    

    [Header("Movimentação")]
    public float velocidade;


    [Header("Pulo")]
    public bool jogadorEstaTocandoNoChao;
    public float alturaPulo;
    public Transform verificadorDeChao;

    public bool ButtonAndarPressed = false;

    public float tamanhoDoVerificadorDeChao;
    public LayerMask camadaDoChao;

    // Update is called once per frame
    void Update()
    {
        MovimentoJogador();
        andarBtn();

        jogadorEstaTocandoNoChao = Physics2D.OverlapCircle(verificadorDeChao.position, tamanhoDoVerificadorDeChao, camadaDoChao);
        //PuloJogador();
    }

    public void andarBtn()
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
            objAnimator.SetBool("pulando", false);
        } else
        {
            objAnimator.SetBool("pulando", true);
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

    private void PuloJogador()
    {
        jogadorEstaTocandoNoChao = Physics2D.OverlapCircle(verificadorDeChao.position, tamanhoDoVerificadorDeChao, camadaDoChao);

        if (jogadorEstaTocandoNoChao)
        {
            if (Input.GetButtonDown("Jump"))
            {
                Debug.Log("pulou");
                rigidBody.AddForce(new Vector2(0f, alturaPulo), ForceMode2D.Impulse);
            }
        }
        else
        {
            objAnimator.Play("lyuna pulando");
        }

    }

    public void JumpWithButton()
    {
        if (jogadorEstaTocandoNoChao)
        {
            rigidBody.AddForce(new Vector2(0f, alturaPulo), ForceMode2D.Impulse);
            objAnimator.SetBool("pulando", true);
        }
    }

    private IEnumerator CarregarBatalha()
    {
        Debug.Log("carregando batalha");
        GameManager.Instance.EndingSceneTransition();
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(faseNova);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("colidiu");
        if (other.CompareTag("Obstacle"))
        {
            Debug.Log("sim é um obstáculo");
            GameManager.Instance.GameOver();
            
        }
        else if (other.CompareTag("Enemy"))
        {
            Debug.Log(other.transform.localScale);
            Debug.Log("sim é um inimigo");
            StartCoroutine(CarregarBatalha());
        }
       
    }
}