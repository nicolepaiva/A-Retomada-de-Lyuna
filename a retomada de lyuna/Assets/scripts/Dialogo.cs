using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Dialogo
{
    [SerializeField] private TextoDialogo[] frases;
    [SerializeField] private string nomeNpc;
    public TextoDialogo[] GetFrases(){
        return frases;
    }

    public string GetNomeNpc(){
        return nomeNpc;
    }
}
