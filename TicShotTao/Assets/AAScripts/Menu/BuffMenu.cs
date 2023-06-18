using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffMenu : MonoBehaviour
{
    private Vector3 portalSpawnPos;
    public float checkDistance;
    public GameObject portal;

    public List<Buff> allBuffs;
    public List<Buff> thisLevelBuffs = new List<Buff>();
    public GameObject buffUI;

    public void Awake()
    {
        for (int i = 0; i < 3; i++)
        {
            int randomIdx = Random.Range(0, allBuffs.Count);
            if (allBuffs.Count != 0)
            {
                Buff currentBuff = allBuffs[randomIdx];
                allBuffs.Remove(currentBuff);
                thisLevelBuffs.Add(currentBuff);
            }  
        }
        buffUI.SetActive(false);
    }
    
    public void enableBuffScreen() 
    {
        buffUI.SetActive(true);
    }

    public void enableBuffScreenWithDelay() 
    {
        Invoke("enableBuffScreen", 0.5f);
    }

    public void disableBuffScreen()
    {
        buffUI.SetActive(false);
    }

    public void spawnPortal() 
    {
        int randomPos = Random.Range(0, 4);
        while (true)
        {
            switch (randomPos)
            {
                case 0:
                    portalSpawnPos = new Vector3(8, 8, 0);
                    break;
                case 1:
                    portalSpawnPos = new Vector3(-8, 8, 0);
                    break;
                case 2:
                    portalSpawnPos = new Vector3(8, -8, 0);
                    break;
                case 3:
                    portalSpawnPos = new Vector3(-8, -8, 0);
                    break;
            }

            Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
            if (Vector3.Distance(portalSpawnPos, playerPos) >= checkDistance)
                break;
        }
        Instantiate(portal, portalSpawnPos, Quaternion.identity);
    }

    public void spawnPortalWithDelay() 
    {
        Invoke("spawnPortal", 0.5f);
    }
}

