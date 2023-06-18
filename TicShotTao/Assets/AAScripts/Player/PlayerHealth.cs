using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private float immuneTime;

    public void LateUpdate()
    {
        if (PlayerData.isImmune && !FindObjectOfType<Manager>().didWin)
        {
            immuneTime -= Time.deltaTime;
            if (immuneTime <= 0f)
                PlayerData.isImmune = false;
        }
    }

    public void damagePlayer(int damage, bool pierceImmunity = false)
    {
        if (!PlayerData.isImmune || pierceImmunity)
        {
            gameObject.GetComponent<SimpleFlash>().Flash();
            FindObjectOfType<AudioManager>().Play("PlayerHit");
            PlayerData.playerCurrentHp -= damage;
            PlayerData.isImmune = true;
            immuneTime = PlayerData.immuneTime;
        }
    }
}
