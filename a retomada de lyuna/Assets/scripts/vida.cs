using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vida : MonoBehaviour
{
    public float vidaDoInimigoAtual;
    [SerializeField] private barraDeVida barraDeVida;

    // Start is called before the first frame update
    void Start()
    {
        vidaDoInimigoAtual = 0;
        if (barraDeVida.AlterarBarraVida(vidaDoInimigoAtual) == true)
        {
            vidaDoInimigoAtual += 0.1f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CurarAnimal(0.1f);
        }
    }
    private void CurarAnimal(float x)
    {
        vidaDoInimigoAtual = 0.1f;
        if (barraDeVida.AlterarBarraVida(vidaDoInimigoAtual) == true)
        {
            vidaDoInimigoAtual += 0.1f;
        }
    }


}
