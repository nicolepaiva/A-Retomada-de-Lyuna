using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransiçãoCreditosScript : MonoBehaviour
{
    [SerializeField] private GameObject _startingSceneTransition;

    private IEnumerator Start() 
    {
        _startingSceneTransition.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        _startingSceneTransition.SetActive(false);
    }
}
