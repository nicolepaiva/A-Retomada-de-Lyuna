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
        MovimentoJogador();
        PuloJogador();
    }

    private void MovimentoJogador()
    {
        float movimentoHorizontal = Input.GetAxis("Horizontal"); 
        float eixoX = movimentoHorizontal * velocidade;

        rigidBody.velocity = new Vector2(eixoX, rigidBody.velocity.y);

        if (jogadorEstaTocandoNoChao) {
            objAnimator.Play("lyuna andando");
        }
    }

    private void PuloJogador()
    {
        jogadorEstaTocandoNoChao = Physics2D.OverlapCircle(verificadorDeChao.position, tamanhoDoVerificadorDeChao, camadaDoChao);

        if (jogadorEstaTocandoNoChao) {
            if (Input.GetButtonDown("Jump")) {
                rigidBody.AddForce(new Vector2(0f, alturaPulo), ForceMode2D.Impulse);
            }
        } else {
            objAnimator.Play("lyuna pulando");
         }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) {
            
        }
    }
}