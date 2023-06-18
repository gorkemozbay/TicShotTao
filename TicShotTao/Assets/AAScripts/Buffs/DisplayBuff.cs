using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DisplayBuff : MonoBehaviour
{
    public Buff buff;
    public int cardIdx;
    public TextMeshProUGUI buffName;
    public TextMeshProUGUI buffDescription;
    public Image buffIcon;

    // Buff Levels
    public Image[] buffLevels;
    public Sprite circleClosed;
    public ParticleSystem ps;
    private int buffLevel;


    // Start is called before the first frame update
    void Start()
    {
        // Pick Buff
        buff = FindObjectOfType<BuffMenu>().thisLevelBuffs[cardIdx];

        // Set Card
        buffName.text = buff.buffName;
        buffDescription.text = buff.buffDescription;
        buffIcon.sprite = buff.buffIcon;

        buffLevel = 0;
        // Check for Buff Level
        for (int i = 0; i < PlayerData.playerBuffs.Count; i++)
        {
            if (buff.buffName == PlayerData.playerBuffs[i].buffName) 
            {
                buffLevel = PlayerData.playerBuffs[i].buffLevel;
                break;
            }
        }

        buff.buffLevel = buffLevel;
        for (int i = 0; i < buffLevel; i++) 
        {
            buffLevels[i].GetComponent<Image>().sprite = circleClosed;
        }
    }

    public void ApplyBuff()
    {
        bool playerHasThisBuff = false;

        if (buff.buffName == "FASTER FINGERS")
        {
            PlayerData.fireRate += buff.buffPower;
        }
        else if (buff.buffName == "LIGHTER SHOES")
        {
            PlayerData.playerSpeed += buff.buffPower;
        }
        else if (buff.buffName == "LEADERSHIP")
        {
            PlayerData.playerCaptureSpeed += buff.buffPower;
        }
        else if (buff.buffName == "SHARPER BULLETS")
        {
            PlayerData.playerDamage += buff.buffPower;
        }

        for (int i = 0; i < PlayerData.playerBuffs.Count; i++)
        {
            // IF already have this buff, increase a level.
            if (buff.buffName == PlayerData.playerBuffs[i].buffName)
            {
                PlayerData.playerBuffs[i].buffLevel++;
                playerHasThisBuff = true;
                break;
            }
        }

        if (!playerHasThisBuff) 
        {
            PlayerData.playerBuffs.Add(buff);
        }

        PlayerData.print();
        BuffMenu buffMenu = FindObjectOfType<BuffMenu>();
        buffMenu.disableBuffScreen();
        buffMenu.spawnPortalWithDelay();
    }
}
