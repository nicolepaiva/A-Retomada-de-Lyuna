using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LyunaScript : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public Animator objAnimator;

    [Header("Movimentação")]
    public float velocidade;

    [Header("Pulo")]
    public bool jogadorEstaTocandoNoChao;
    public float alturaPulo;
    public Transform verificadorDeChao;

    public float tamanhoDoVerificadorDeChao;
    public LayerMask camadaDoChao;

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.gameSpeed == 0){
            velocidade = 12;
        }else{
            velocidade = 5;
        }

        MovimentoJogador();
        PuloJogador();
    }

    private void MovimentoJogador()
    {
        float movimentoHorizontal = Input.GetAxis("Horizontal"); 
        float eixoX = movimentoHorizontal * velocidade;

        rigidBody.velocity = new Vector2(eixoX, rigidBody.velocity.y);

        if (jogadorEstaTocandoNoChao) {
            if (GameManager.Instance.gameSpeed != 0 || movimentoHorizontal != 0) {
                objAnimator.Play("lyuna andando");
            } else {
                objAnimator.Play("lyuna parada");
            }
        }

        if(GameManager.Instance.gameSpeed == 0){
            velocidade = 12;
        }else{
            velocidade = 5;
        }

        if(movimentoHorizontal > 0 || GameManager.Instance.gameSpeed > 0){
            transform.localScale = new Vector3(2.910543f,2.910543f,2.910543f);
        }else if(movimentoHorizontal < 0 && GameManager.Instance.gameSpeed == 0){
            transform.localScale = new Vector3(-2.910543f,2.910543f,2.910543f);
        }
    }

    private void PuloJogador()
    {
        jogadorEstaTocandoNoChao = Physics2D.OverlapCircle(verificadorDeChao.position, tamanhoDoVerificadorDeChao, camadaDoChao);

        if (jogadorEstaTocandoNoChao) {
            if (Input.GetButtonDown("Jump")) {
                Debug.Log("pulou");
                rigidBody.AddForce(new Vector2(0f, alturaPulo), ForceMode2D.Impulse);
            }
        } else {
            objAnimator.Play("lyuna pulando");
         }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("colidiu com obstáculo");
        if (other.CompareTag("Obstacle")) {
            Debug.Log("sim é um obstáculo");
            GameManager.Instance.GameOver();
        }
    }
}