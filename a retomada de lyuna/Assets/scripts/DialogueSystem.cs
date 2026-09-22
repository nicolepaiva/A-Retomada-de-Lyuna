using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum STATE {
    DISABLED,
    WAITING,
    TYPING
}

public class DialogueSystem : MonoBehaviour
{
    public DialogueData dialogueData;
    public Genius geniusScript;

    int currentText = 0;
    bool finished = false;

    TypeTextAnimationScript typeText;

    STATE state;

    void Awake() {
        typeText = FindObjectOfType<TypeTextAnimationScript>();
        typeText.TypeFinished = OnTypeFinished;
        geniusScript = FindObjectOfType<Genius>();
    }

    void Start () {
        state = STATE.DISABLED;
    }

    void Update() {
        if (state == STATE.DISABLED) return;

        switch (state) {
            case STATE.WAITING:
                Waiting();
                break;
            case STATE.TYPING:
                Typing();
                break;
        }
    }

    public void Next() {
        Debug.Log("NEXT");
        typeText.nome = dialogueData.talkScript[currentText].name;
        typeText.fullText = dialogueData.talkScript[currentText++].text;
        if(currentText == dialogueData.talkScript.Count) finished = true;
        typeText.StartTyping();
        StartCoroutine(Sleep(0.2f));
        state = STATE.TYPING;
    }

    void OnTypeFinished() {
        state = STATE.WAITING;
    }

    private IEnumerator Sleep(float time)
    {
        yield return new WaitForSeconds(time);
    }

    void Waiting() {
        if (Input.touchCount > 0) {
            if (Input.GetTouch(0).phase == TouchPhase.Began) {
                if (!finished) {
                    Next();
                } else {
                    state = STATE.DISABLED;
                    currentText = 0;
                    finished = false;
                    Debug.Log("Carregando fase...");
                    StartCoroutine(geniusScript.CarregarFase());
                }
            }
        }
    }

    void Typing() {
        if (Input.touchCount > 0) {
            if (Input.GetTouch(0).phase == TouchPhase.Began) {
                typeText.Skip();
                state = STATE.WAITING;
            }
        }
    }
}
