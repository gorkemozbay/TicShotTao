using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float lifeTime = 2f;
    public int bulletDamage = 1;
    float startTime;

    public ParticleSystem ps;


    private void Start()
    {
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
        if (collision.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().damagePlayer(1);
            killBullet();
        }
        
    }

    private void killBullet()
    {
        ParticleSystem particle = Instantiate(ps, gameObject.transform.position, Quaternion.identity);
        particle.Play();
        Destroy(particle.gameObject, 2f);
        Destroy(gameObject);
    }

}
