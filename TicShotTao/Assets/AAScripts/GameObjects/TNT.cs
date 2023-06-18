using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNT : MonoBehaviour
{

    public GameObject explotionPS;
    private int tnt_hp = 2;
    private int explosionDamage = 10;
    private float explosionArea = 4f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (tnt_hp <= 0)
        {
            Instantiate(explotionPS, transform.position, transform.rotation);
            FindObjectOfType<AudioManager>().Play("Explosion");
            DamageInArea();
            Destroy(gameObject);
        }
    }

    public void getDamage()
    {
        tnt_hp -= 1;
    }

    public void DamageInArea()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionArea);
        foreach(Collider2D col in colliders)
        {
            if (col.gameObject.GetComponent<enemyHealth>() != null)
            {
                col.gameObject.GetComponent<enemyHealth>().getDamage(explosionDamage);
            } 
        }
    }

}
