using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    // Player Stats HP
    public static int playerMaxHp = 4;
    public static int playerCurrentHp = 4;
    public static float immuneTime = 1f;
    public static bool isImmune = false;

    // Player Stats Attack
    public static float playerDamage = 2.5f;
    public static float fireRate = 3f;

    // Player Stats Movement
    public static float playerSpeed = 10;

    // Player Stats Capture
    //public static float playerCaptureSpeed = 8f;
    public static float playerCaptureSpeed = 80f;

    // Player Stats Coin/Inventory
    public static int playerCoin = 0;

    // Player Stats Dash
    public static float dashCD = 1f;
    public static float dashDuration = 0.4f;
   
    // Level Stats
    public static int currentLevel = 1;

    // Level Difficulty Stats
    public static float[] spawnRates = {0.1f, 0.2f, 0.3f, 0.5f, 0.8f, 1.3f, 2.1f, 3.4f, 5.5f, 8.9f};

    // Levels
    public static List<int> startingLevels = new List<int>() { 4, 5};
    public static List<int> levels = new List<int>(){6, 7, 8, 9, 10, 11, 12};

    // Buffs 
    public static List<Buff> playerBuffs = new List<Buff>(); 

    public static void print()
    {
        Debug.Log("Player Has " + playerBuffs.Count + " Buffs");
        for (int i = 0; i < playerBuffs.Count; i++)
        {
            Debug.Log("Player Buff " + (i+1) + ": " + playerBuffs[i]);
        }
    }
}
