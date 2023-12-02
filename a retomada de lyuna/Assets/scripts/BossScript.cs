using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    void Update()
    {
        transform.position += (Vector3.left * GameManager.Instance.gameSpeed) * Time.deltaTime;
    }
}
