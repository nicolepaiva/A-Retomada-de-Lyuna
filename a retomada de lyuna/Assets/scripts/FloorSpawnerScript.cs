using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class FloorSpawnerScript : MonoBehaviour
{

    public GameObject floor;
    public float spawnRate = 2;
    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnRate) {
            timer += Time.deltaTime;
        } else {
            SpawnPipe();
            timer = 0;
        }
    }

    void SpawnPipe() {
        Instantiate(floor, transform.position, transform.rotation);
    }
}
