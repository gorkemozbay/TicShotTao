using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHealth : MonoBehaviour
{
    public ParticleSystem ps;
    public GameObject coin;

    public float hp;
    public int contactDamage;

    public void Start()
    {
        contactDamage = 1;
    }

    public void getDamage(float damage)
    {
        hp -= damage;
    }

    private void Update()
    {     
        if(hp <= 0)
        {
            killEnemy();
        }   
    }

    public void killEnemy(bool dropCoin = true)
    {
        // Instantiate death effect
        ParticleSystem particle = Instantiate(ps, transform.position, Quaternion.identity);
        particle.Play();
        Destroy(particle, 2f);

        // Drop random coins
        if (GameObject.FindGameObjectWithTag("UI").GetComponent<PlayerUI>().canDropCoin && dropCoin)
        {
            int coinDropAmount = Random.Range(1,4);
            for (int i = 0; i < coinDropAmount; i++)
            {
                Vector3 randomVector = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0);
                GameObject currentCoin = Instantiate(coin, transform.position + randomVector, Quaternion.identity);
                Rigidbody2D rb = currentCoin.GetComponent<Rigidbody2D>();
            
                float force = (float)Random.Range(30, 40);
                rb.AddForce(randomVector.normalized * force);            
            }
        }
        // Begone
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.collider.gameObject.GetComponent<PlayerHealth>().damagePlayer(contactDamage);
            Destroy(gameObject);
        }
    }

}
