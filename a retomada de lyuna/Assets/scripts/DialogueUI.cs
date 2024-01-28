 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    TextMeshProUGUI nameText;

    void Awake() {
        nameText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    public void SetName (string name) {
        nameText.text = name;
    }
}
