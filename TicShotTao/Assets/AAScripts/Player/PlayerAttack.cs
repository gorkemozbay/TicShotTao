using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform attackPoint;
    public GameObject bulletPrefab;

    public float bulletSpeed = 20f;
    public float timetoShot;

    private void Start()
    {
        timetoShot = 1 / PlayerData.fireRate;
    }

    private void Update()
    {
        timetoShot -= Time.deltaTime;
        if (Input.GetMouseButton(0) && timetoShot < 0f)
        {
            Shoot();
        }
        
    }

    private void Shoot()
    {
        // Create bullet and speed it up
        GameObject bullet = Instantiate(bulletPrefab, attackPoint.position, attackPoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = attackPoint.up * bulletSpeed;
        // Shot sound
        FindObjectOfType<AudioManager>().Play("PlayerShot");
        // Fire rate timing
        timetoShot = 1 / PlayerData.fireRate;
    }

    
}
