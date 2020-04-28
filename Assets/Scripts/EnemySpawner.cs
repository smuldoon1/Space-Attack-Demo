using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Spawn[] spawns;

    private void Start()
    {
        foreach (Spawn spawn in spawns)
        {
            StartCoroutine(SpawnEnemies(spawn.enemy, spawn.spawnRate, spawn.spawnFluctuation));
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            SpawnEnemy(spawns[0].enemy);
        }
    }

    public void SpawnEnemy(EnemyShip enemy)
    {
        int spawnSide = Random.Range(0, 2) == 0 ? -50 : 50;
        Instantiate(enemy, transform.position + new Vector3(spawnSide, 0, 0), Quaternion.Euler(new Vector3(0f, 180f, 0f)));
    }

    public IEnumerator SpawnEnemies(EnemyShip enemy, float rate, float fluctuation)
    {
        while (true)
        {
            yield return new WaitForSeconds(rate * Random.Range(1 - fluctuation > 0 ? 1 - fluctuation : 0, 1 + fluctuation));
            SpawnEnemy(enemy);
        }
    }
}

[System.Serializable]
public struct Spawn
{
    public EnemyShip enemy;
    public float spawnRate;
    public float spawnFluctuation;
}
