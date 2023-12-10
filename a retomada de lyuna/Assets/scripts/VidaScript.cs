using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VidaScript : MonoBehaviour
{
    public Slider slider;

    public void AlterarVida(float vida) {
        slider.value = vida;
    }
}
