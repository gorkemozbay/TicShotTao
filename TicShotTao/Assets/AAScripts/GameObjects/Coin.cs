using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    public ParticleSystem ps;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerData.playerCoin += 1;
            FindObjectOfType<AudioManager>().Play("PickedCoin");
            Destroy(gameObject);
        }
    }

    public void destroyWithTimer() 
    {
        ParticleSystem particle = Instantiate(ps, gameObject.transform.position, Quaternion.identity);
        particle.Play();
        Destroy(particle.gameObject, 2f);
    }
}
