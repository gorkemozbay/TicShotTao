using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_follower : MonoBehaviour
{
    public float enemySpeed = 3f;
    public float stopDistance;
    public float retreatDistance = 5f;

    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerData.playerCurrentHp == 0)
            return;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        stopDistance = 1f;
    }   

    // Update is called once per frame
    void Update()
    {
        if (PlayerData.playerCurrentHp == 0)
            return;

        float distance = Vector2.Distance(transform.position, player.position);

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
    }
    
}
