using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MobileController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public enum ComandoPlayerMovimento { AndarFrente, Pular}
    public ComandoPlayerMovimento comando;
    public void OnPointerDown(PointerEventData eventData)
    {
        if(comando == ComandoPlayerMovimento.AndarFrente)
        {
            MobileInputManager.Instance.andarFrentePressed = true;
        } else if(comando == ComandoPlayerMovimento.Pular)
        {
            MobileInputManager.Instance.pularPressed = true;
        }
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(comando == ComandoPlayerMovimento.AndarFrente)
        {
            MobileInputManager.Instance.andarFrentePressed = false;
        } else if (comando == ComandoPlayerMovimento.Pular)
        {
            MobileInputManager.Instance.pularPressed = false;
        }
    }
}
