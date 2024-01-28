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
    DialogueUI dialogueUI;

    STATE state;

    void Awake() {
        typeText = FindObjectOfType<TypeTextAnimationScript>();
        typeText.TypeFinished = OnTypeFinished;
        geniusScript = FindObjectOfType<Genius>();
        dialogueUI = FindObjectOfType<DialogueUI>();
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
        typeText.nome = dialogueData.talkScript[currentText].name;
        typeText.fullText = dialogueData.talkScript[currentText++].text;
        if(currentText == dialogueData.talkScript.Count) finished = true;
        typeText.StartTyping();
        state = STATE.TYPING;
    }

    void OnTypeFinished() {
        state = STATE.WAITING;
    }

    void Waiting() {
        if (Input.GetKeyDown(KeyCode.Space)) {
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

    void Typing() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            typeText.Skip();
            state = STATE.WAITING;
        }
    }
}
