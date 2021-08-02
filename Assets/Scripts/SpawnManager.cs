using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public int enemyCount;
    public int spawnNumber = 1;
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;

    void Start()
    {
        SpawnEnemyWave(spawnNumber); // baslangicta 1 tane gelsin
        Instantiate(powerupPrefab, GenerateSpawnPos(), powerupPrefab.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            spawnNumber++; //enemy yok oldukca her defasinda artir
            SpawnEnemyWave(spawnNumber);
            Instantiate(powerupPrefab, GenerateSpawnPos(), powerupPrefab.transform.rotation);
        }
    }
    private Vector3 GenerateSpawnPos()
    {
        float spawnPosX = Random.Range(-10, 10);
        float spawnPosZ = Random.Range(-10, 10);
        Vector3 spawnPos = new Vector3(spawnPosX, 0, spawnPosZ);

        return spawnPos; //return kullanmaliyiz void olmayan metodlarda.
    }
    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPos(), enemyPrefab.transform.rotation);
        }
    }

}
