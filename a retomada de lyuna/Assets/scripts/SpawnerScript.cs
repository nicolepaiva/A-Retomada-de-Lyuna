using System.Runtime.CompilerServices;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnableObject
    {
        public GameObject prefab;
        [Range(0f, 1f)]
        public float spawnChance;
    }

    public GameObject firstObstacle;
    public  SpawnableObject[] objects;

    public float minSpawnRate = 1f;
    public float maxSpawnRate = 2f;

    private void Start()
    {
        for (int i = -5; i <= 25; i += 15) 
        {
            SpawnOne(new Vector3(i, 0, 0));
        }
    }

    private void OnEnable()
    {
        Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Spawn()
    {
        SpawnOne(transform.position);
        Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
    }

    private void SpawnOne(Vector3 pos)
    {
        float spawnChance = Random.value;

        foreach (var obj in objects)
        {
            if (spawnChance < obj.spawnChance)
            {
                GameObject obstacle = Instantiate(obj.prefab);
                obstacle.transform.position += pos;
                break;
            }
            spawnChance -= obj.spawnChance;
        }
    }
    
}
