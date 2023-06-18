using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_shooter : MonoBehaviour
{
    public int contactDamage = 1;
    public float shotCoolDown;
    public float shotCoolDownStart = 3f;
    public float bulletSpeed = 10f;

    public float enemySpeed = 2f;
    public float stopDistance = 15f;
    public float retreatDistance = 5f;

    public Transform player;
    public GameObject bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        shotCoolDown = shotCoolDownStart;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        shotCoolDown -= Time.deltaTime;

        if (distance > stopDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, enemySpeed * Time.deltaTime);
        } 
        else if (distance < stopDistance && distance > retreatDistance)
        {
            transform.position = this.transform.position;
        } 
        else if (distance < retreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -enemySpeed * Time.deltaTime);
        }

        if (shotCoolDown <= 0f)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb =  bullet.GetComponent<Rigidbody2D>();
            
            Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
            Vector2 shotDir = playerPos - rb.position;

            rb.velocity = shotDir.normalized * bulletSpeed;

            shotCoolDown = shotCoolDownStart;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            damagePlayer(contactDamage);
            gameObject.GetComponent<enemyHealth>().killEnemy();
            
        }
    }
    public void damagePlayer(int damage)
    {
        PlayerData.playerCurrentHp -= damage;
    }

    
}
