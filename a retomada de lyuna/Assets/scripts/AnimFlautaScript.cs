using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimFlautaScript : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
