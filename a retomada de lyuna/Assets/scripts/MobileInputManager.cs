using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInputManager : MonoBehaviour
{

    public static MobileInputManager Instance;

    public bool andarFrentePressed = false;
    public bool andarTrasPressed = false;
    public bool pularPressed = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public float GetHorizontalInput()
    {
        float input = 0f;
        if (andarFrentePressed) input += 1f;
        if (andarTrasPressed) input -= 1f;
        return input;
    }
}
