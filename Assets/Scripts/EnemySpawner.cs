using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemyShip enemy;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            SpawnEnemy();
        }
    }

    public void SpawnEnemy()
    {
        int spawnSide = Random.Range(0, 2) == 0 ? -50 : 50;
        Instantiate(enemy, transform.position + new Vector3(spawnSide, 0, 0), Quaternion.Euler(new Vector3(0f, 180f, 0f)));
    }
}
