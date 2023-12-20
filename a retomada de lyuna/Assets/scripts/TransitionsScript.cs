using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TransitionsScript : MonoBehaviour
{
    [SerializeField] private GameObject _startingSceneTransition;
    [SerializeField] private GameObject _endingSceneTransition;

    private void Start()
    {
        _startingSceneTransition.SetActive(true);
        Invoke(nameof(DisableStartingSceneTransition), 1.4f);
    }

    private void DisableStartingSceneTransition()
    {
        _startingSceneTransition.SetActive(false);
    }

}