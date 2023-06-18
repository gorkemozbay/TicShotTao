using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffOrb : MonoBehaviour
{
    public ParticleSystem pickedUpEffect;

    void Update()
    {
        Collider2D collider = Physics2D.OverlapCircle(this.transform.position, 1.0f);
        if (collider != null)
        {
            if (collider.gameObject.CompareTag("Player")) 
            {
                FindObjectOfType<BuffMenu>().enableBuffScreenWithDelay();
                Instantiate(pickedUpEffect, gameObject.transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}
