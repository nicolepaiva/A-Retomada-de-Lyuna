using UnityEngine;
using TMPro;
using System;
using System.Collections;

public class TypeTextAnimationScript : MonoBehaviour
{
    public Action TypeFinished;
    public float typeDelay = 0.05f;
    public TMPro.TextMeshProUGUI textObject;
    public TMPro.TextMeshProUGUI nomeObject;
    public string nome;
    public string fullText;
    Coroutine coroutine;

    // void Start()
    // {
    //     StartCoroutine(TypeText());
    // }

    public void StartTyping() {
        coroutine = StartCoroutine(TypeText());
    }

    public IEnumerator TypeText() {
        nomeObject.text = nome;
        textObject.text = fullText;
        textObject.maxVisibleCharacters = 0;
        for (int i = 0; i <= textObject.text.Length; i++) {
            textObject.maxVisibleCharacters = i;
            yield return new WaitForSeconds(typeDelay);
        }
        TypeFinished?.Invoke();
    }
    public void Skip() {
        StopCoroutine(coroutine);
        textObject.maxVisibleCharacters = textObject.text.Length;
    }
}
