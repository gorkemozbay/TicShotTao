using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // spawner time
    private float timeUntilSpawn;
    private float spawnPeriod;
    private float spawnRateNoise;
    // spawner type
    private int randomIdx;
    private int currentLevel;
    Dictionary<string, int> enemies = new Dictionary<string, int>();
    
    public GameObject[] enemyTypes; // | 0: follower | 1: capturer | 2: shooter |

    // Start is called before the first frame update
    private void Start()
    {
        // Set enemies
        enemies.Add("follower", 0);
        enemies.Add("capturer", 1);
        enemies.Add("shooter", 2);

        // Get difficulty
        currentLevel = PlayerData.currentLevel;
        int difficulty = currentLevel / 10;
        
        // Set spawnRate
        float spawnRate = PlayerData.spawnRates[difficulty];

        // Add noise
        spawnRateNoise = Random.Range(-spawnRate / 2, spawnRate / 5);
        spawnRate += spawnRateNoise;
        spawnPeriod = 1 / spawnRate;

        // Start timer
        timeUntilSpawn = spawnPeriod;

        // Spawn some followers immediatly
        int randomFollowerCount = Random.Range(0,2);
        float randomDelay = Random.Range(0.0f, 3.0f);
        for (int i = 0; i < randomFollowerCount; i++)
        {
            Invoke("spawnFollower", randomDelay);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!FindObjectOfType<Manager>().didWin)
        { 
            timeUntilSpawn -= Time.deltaTime;
            if (timeUntilSpawn < 0f)
            {
                float spawnChange = Random.Range(0f, 1f);
                float randomFloat = Random.Range(0f, 1f);

                // LEVEL 1-2: only follower
                if (currentLevel <= 2) 
                {
                    if (spawnChange <= 0.5f)
                        return;
                    else
                        randomIdx = enemies["follower"];
                }
                // LEVEL 3-5: follower + capturer
                else if (currentLevel <= 5)
                {
                    if (spawnChange <= 0.3f)
                        return;
                    else
                    {
                        if (randomFloat <= 0.25)
                            randomIdx = enemies["capturer"];
                        else
                            randomIdx = enemies["follower"];
                    }
                }
                // LEVEL 6-10: follower + capturer + shooter   
                else if (currentLevel <= 10)
                {
                    if (spawnChange <= 0.2f)
                        return;
                    else
                    {
                        if (randomFloat <= 0.25)
                            randomIdx = enemies["capturer"];
                        else if (randomFloat <= 0.5)
                            randomIdx = enemies["shooter"];
                        else
                            randomIdx = enemies["follower"];
                    }
                }
                Instantiate(enemyTypes[randomIdx], gameObject.transform.position, Quaternion.identity);
                timeUntilSpawn = spawnPeriod;
            }
        }
    }

    void spawnFollower()
    {
        Instantiate(enemyTypes[0], gameObject.transform.position, Quaternion.identity);
    }
}
