using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DialogueSystem2 : MonoBehaviour
{
    public DialogueData dialogueData;
    public GameObject caixaDiálogo;
    public string faseNova;
    [SerializeField] private GameObject _startingSceneTransition;
    [SerializeField] private GameObject _endingSceneTransition;
    int currentText = 0;
    bool finished = false;

    TypeTextAnimationScript typeText;

    STATE state;

    void Awake() {
        typeText = FindObjectOfType<TypeTextAnimationScript>();
        typeText.TypeFinished = OnTypeFinished;
    }

    public IEnumerator Start () {
        state = STATE.DISABLED;
        _startingSceneTransition.SetActive(true);
        yield return new WaitForSeconds(5f);
        _startingSceneTransition.SetActive(false);
        caixaDiálogo.SetActive(true);
        Next();
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
                StartCoroutine(CarregarFase());
            }
        }
    }

    void Typing() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            typeText.Skip();
            state = STATE.WAITING;
        }
    }

    public IEnumerator CarregarFase()
    {
        caixaDiálogo.SetActive(false);
        _endingSceneTransition.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(faseNova);
    }
}
