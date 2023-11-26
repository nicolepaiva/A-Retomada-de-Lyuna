using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LyunaScript : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public float jumpStrength;
    public float velocidade;

    private void MovimentarJogador()
    {
        float movimentoHorizontal = Input.GetAxis("Horizontal");
        float pulo = Input.GetAxis("Jump");
        float eixoX = movimentoHorizontal * velocidade;
        float eixoY = pulo * jumpStrength;

        rigidBody.velocity = new Vector2(eixoX, eixoY);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MovimentarJogador();
    }
}