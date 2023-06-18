using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    private GameObject player;
    private bool isPlayerAlive;

    private void Awake()
    {
        PlayerData.isImmune = false;
        isPlayerAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerData.playerCurrentHp <= 0 && isPlayerAlive)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            Invoke("loadLostScreen", 1f);
            isPlayerAlive = false;
            player.GetComponent<PlayerMovement>().killPlayer();    
        } 
    }

    public void loadWinScreen()
    {
        SceneManager.LoadScene(1);
    }

    public void loadLostScreen()
    {
        SceneManager.LoadScene(2);
    }
}
