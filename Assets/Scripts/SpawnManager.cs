using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public int enemyCount;
    public int spawnNumber = 1;
    public GameObject[] enemyPrefabs;
    public GameObject[] powerupPrefabs;
    private int randomEnemy;

    void Start()
    {
        int randomPowerup = Random.Range(0, powerupPrefabs.Length);
        Instantiate(powerupPrefabs[0], GenerateSpawnPosition(),
        powerupPrefabs[0].transform.rotation);
        SpawnEnemyWaveEasy(spawnNumber); // kacinci spawn oldugu
        
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if(enemyCount == 0 && spawnNumber < 3) //easy enemies until 3rd round
        {
            spawnNumber++; //enemy yok oldukca her defasinda artir
            SpawnEnemyWaveEasy(spawnNumber);
            Instantiate(powerupPrefabs[0], GenerateSpawnPosition(), powerupPrefabs[0].transform.rotation);
           
        }
        else if(spawnNumber >= 3 && enemyCount == 0) //harder enemies
            {
                spawnNumber++;
                int randomPowerup = Random.Range(0, powerupPrefabs.Length);
                Instantiate(powerupPrefabs[randomPowerup], GenerateSpawnPosition(),
                powerupPrefabs[randomPowerup].transform.rotation);
                SpawnEnemyWave(spawnNumber);
            }
        
    

    
    }
    private Vector3 GenerateSpawnPosition()
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
            int randomEnemy = Random.Range(0, 2);
            Instantiate(enemyPrefabs[randomEnemy], GenerateSpawnPosition(), enemyPrefabs[randomEnemy].transform.rotation);
        }
    }

    void SpawnEnemyWaveEasy(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            
            Instantiate(enemyPrefabs[0], GenerateSpawnPosition(), enemyPrefabs[0].transform.rotation);
        }
    }
        

}
