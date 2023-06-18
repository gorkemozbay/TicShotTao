using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public CapturePoint[] cps;
    public GameObject[] winDots;
    public LevelLoader levelLoader;

    public bool didWin = false;
    private float afterWinTimeWait;
    public int whoWinned = 0;
    private int[] gameMap = {0,0,0,0,0,0,0,0,0};

    public float checkDistance;

    // To spawns buff-pick orb
    private GameObject player;
    public GameObject buffOrb;
    private Vector3 orbPos;

    private void Start()
    {
        afterWinTimeWait = 1;
        levelLoader = FindObjectOfType<LevelLoader>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < gameMap.Length; i++)
        {
            gameMap[i] = cps[i].whoCaptured();
        }
        whoWinned = checkForWin();
        if (whoWinned == 1)
        {
            PlayerData.isImmune = true;
            Invoke("spawnBuffOrb", 3.5f);
            // Destroy All Enemies in the map
            GameObject[] enemiesToDestroy_capturer = GameObject.FindGameObjectsWithTag("EnemyCapturer");
            GameObject[] enemiesToDestroy_others = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject[] enemiesToDestroy_all = new GameObject[enemiesToDestroy_capturer.Length + enemiesToDestroy_others.Length];
            enemiesToDestroy_capturer.CopyTo(enemiesToDestroy_all, 0);
            enemiesToDestroy_others.CopyTo(enemiesToDestroy_all, enemiesToDestroy_capturer.Length);
            foreach (GameObject enemy in enemiesToDestroy_all)
            {
                enemy.GetComponent<enemyHealth>().killEnemy(false);
            }
        }
        else if (whoWinned == -1)
        {
            PlayerData.isImmune = true;
            Invoke("loadLoseScreen", 2f);
        }
    }

    // returns 0 if no win, 1 for player win, -1 for enemy win
    public int checkForWin()
    {
        if (!didWin)
        {
            // rows
            if (Mathf.Abs(gameMap[0] + gameMap[1] + gameMap[2]) == 3)
            {
                winDots[0].GetComponent<WinCircle>().SetGoalPos("h");
                didWin = true;
                return gameMap[0] == 1 ? 1 : -1;
            }
            else if (Mathf.Abs(gameMap[3] + gameMap[4] + gameMap[5]) == 3)
            {
                winDots[1].GetComponent<WinCircle>().SetGoalPos("h");
                didWin = true;
                return gameMap[3] == 1 ? 1 : -1;
            }
            else if (Mathf.Abs(gameMap[6] + gameMap[7] + gameMap[8]) == 3)
            {
                winDots[2].GetComponent<WinCircle>().SetGoalPos("h");
                didWin = true;
                return gameMap[6] == 1 ? 1 : -1;
            }
            // columns
            else if (Mathf.Abs(gameMap[0] + gameMap[3] + gameMap[6]) == 3)
            {
                winDots[3].GetComponent<WinCircle>().SetGoalPos("v");
                didWin = true;
                return gameMap[0] == 1 ? 1 : -1;
            }
            else if (Mathf.Abs(gameMap[1] + gameMap[4] + gameMap[7]) == 3)
            {
                winDots[4].GetComponent<WinCircle>().SetGoalPos("v");
                didWin = true;
                return gameMap[1] == 1 ? 1 : -1;
            }
            else if (Mathf.Abs(gameMap[2] + gameMap[5] + gameMap[8]) == 3)
            {
                winDots[5].GetComponent<WinCircle>().SetGoalPos("v");
                didWin = true;
                return gameMap[2] == 1 ? 1 : -1;
            }
            // diogonals
            else if (Mathf.Abs(gameMap[0] + gameMap[4] + gameMap[8]) == 3)
            {
                winDots[6].GetComponent<WinCircle>().SetGoalPos("d");
                didWin = true;
                return gameMap[0] == 1 ? 1 : -1;
            }
            else if (Mathf.Abs(gameMap[2] + gameMap[4] + gameMap[6]) == 3)
            {
                winDots[7].GetComponent<WinCircle>().SetGoalPos("d");
                didWin = true;
                return gameMap[2] == 1 ? 1 : -1;
            }
        }
        // no win
        return 0;

    }

    void loadWinScreen()
    {
        levelLoader.loadWinScreen();
    }

    void loadLoseScreen()
    {
        levelLoader.loadLostScreen();
    }

    void spawnBuffOrb() 
    {
        while (true)
        {
            Vector3 playerPos = player.transform.position;
            float randomX = Random.Range(-25f, 25f);
            float randomY = Random.Range(-30f, 30f);
            orbPos = new Vector3(randomX, randomY, 0f);
            if (Vector3.Distance(orbPos, playerPos) >= checkDistance)
                break;
        }
        Instantiate(buffOrb, orbPos, Quaternion.identity);
    }
    
    
}

