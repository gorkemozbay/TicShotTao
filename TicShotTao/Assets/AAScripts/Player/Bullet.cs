using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float lifeTime = 2f;
    private float bulletDamage;
    float startTime;

    public ParticleSystem ps;
    

    private void Start()
    {
        bulletDamage = PlayerData.playerDamage;
        startTime = Time.time;
    }

    private void Update()
    {
        if (Time.time - startTime > lifeTime)
        {
            killBullet();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin") || collision.CompareTag("Bullet"))
        {
            return;
        }

        if (collision.CompareTag("Enemy") || collision.CompareTag("EnemyCapturer"))
        {
            collision.GetComponent<enemyHealth>().getDamage(bulletDamage);
        }

        killBullet();

    }

    private void killBullet()
    {
        ParticleSystem particle = Instantiate(ps, gameObject.transform.position, Quaternion.identity);
        particle.Play();
        Destroy(particle.gameObject, 2f);
        Destroy(gameObject);
    }
}
