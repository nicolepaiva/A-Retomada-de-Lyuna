using UnityEngine;
using TMPro;
using System.Collections;

public class TypeTextAnimationScript : MonoBehaviour
{
    public float typeDelay = 0.05f;
    public TMPro.TextMeshProUGUI textObject;
    public string fullText;

    void Start()
    {
        StartCoroutine(TypeText());
    }

    public IEnumerator TypeText() {
        textObject.text = fullText;
        textObject.maxVisibleCharacters = 0;
        for (int i = 0; i <= textObject.text.Length; i++) {
            textObject.maxVisibleCharacters = i;
            yield return new WaitForSeconds(typeDelay);
        }
    }
}
