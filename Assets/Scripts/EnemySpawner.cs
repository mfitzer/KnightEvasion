using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public GameObject enemyPrefab;
    public List<Transform> spawnPoints;
    public int startingEnemiesToSpawn = 3;
    int enemiesToSpawn;
    float waveTimer = 0; //Tracks how long since a wave was spawned
    public float waveLength = 20f;
    int waveNumber = 1;
    int enemies = 0;

    // Use this for initialization
    void Start () {
        enemiesToSpawn = startingEnemiesToSpawn;
	}

    //Spawns a wave of enemies
    public void spawnWave()
    {
        List<int> usedSpawnPoints = new List<int>();
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            int spawnIndex = Random.Range(0, spawnPoints.Count); //Random spawnpoint index
            while (usedSpawnPoints.Contains(spawnIndex)) //Stops enemies from being spawned in the same spot
            {
                spawnIndex = Random.Range(0, spawnPoints.Count);
            }
            usedSpawnPoints.Add(spawnIndex);
            GameObject enemy = Instantiate(enemyPrefab, spawnPoints[spawnIndex].position, Quaternion.identity); //Spawn enemy
            enemies++;
        }
        if (enemiesToSpawn + 2 <= spawnPoints.Count)
        {
            enemiesToSpawn += 2;
        }
        else
        {
            enemiesToSpawn = spawnPoints.Count;
        }
    }

    //Spawns a wave of enemies if one is due
    void checkWaveTimer()
    {
        waveTimer += Time.deltaTime;
        if (waveTimer >= waveLength)
        {
            waveTimer = 0;
            waveNumber++;
            spawnWave();
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (GameManager.gameState == GameManager.GAMESTATE.PLAYING)
        {
            checkWaveTimer();
        }
        else
        {
            enemiesToSpawn = startingEnemiesToSpawn; //Reset spawning
        }
    }
}
