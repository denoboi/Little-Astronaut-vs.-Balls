using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public GameObject enemyPrefab;
    
    void Start()
    {
       Instantiate(enemyPrefab, EnemySpawnPos(), enemyPrefab.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private Vector3 EnemySpawnPos()
    {
        float spawnPosX = Random.Range(-10, 10);
        float spawnPosZ = Random.Range(-10, 10);
        Vector3 spawnPos = new Vector3(spawnPosX, 0, spawnPosZ);

        return spawnPos; //return kullanmaliyiz void olmayan metodlarda.
    }
}
