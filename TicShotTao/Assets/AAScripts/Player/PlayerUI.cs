using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    // Health
    private int health;
    private int numberOfHearts;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    // Coin
    public TextMeshProUGUI coinText;
    public bool canDropCoin;

    // Timer
    private float currentTimeSec;
    private int currentTimeMin;
    private bool isTimerRunning;
    public TextMeshProUGUI timerText;
    
    // Level
    public TextMeshProUGUI levelText;

    private void Start()
    {
        isTimerRunning = true;
        canDropCoin = true;
        currentTimeSec = 0f;
        currentTimeMin = 2;

        levelText.text = "LEVEL " +  PlayerData.currentLevel.ToString();
    }

    void Update()
    {
        
        // Health
        health = PlayerData.playerCurrentHp;
        numberOfHearts = PlayerData.playerMaxHp;

        for (int i = 0; i < hearts.Length; i++)
        {

            if (health > numberOfHearts)
            {
                health = numberOfHearts;
            }

            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < numberOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }

        // Coin
        coinText.text = PlayerData.playerCoin.ToString();

        // Timer
        if (isTimerRunning && !FindObjectOfType<Manager>().didWin)
            currentTimeSec -= Time.deltaTime;
        if (currentTimeSec <= 0f)
        {
            currentTimeSec = 60f;
            currentTimeMin -= 1;
            if (currentTimeMin == -1)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().damagePlayer(1, true);
                timerText.color = Color.red;
                isTimerRunning = false;
                canDropCoin = false;
                // Destroy All Coins in the map
                GameObject[] coinsToDestroy;
                coinsToDestroy = GameObject.FindGameObjectsWithTag("Coin");
                foreach (GameObject coin in coinsToDestroy)
                { 
                    coin.GetComponent<Coin>().destroyWithTimer();
                    Destroy(coin);
                }
            }
        }
        // Adjust visuals
        if (isTimerRunning && currentTimeSec < 10)
        {
            timerText.text = "0" + currentTimeMin.ToString() + ":0" + ((int)(currentTimeSec)).ToString();
        }
        else if (isTimerRunning)
        {
            timerText.text = "0" + currentTimeMin.ToString() + ":" + ((int)(currentTimeSec)).ToString();
        }
        
    }

}
