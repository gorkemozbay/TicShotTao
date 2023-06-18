using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_capturer1 : MonoBehaviour
{
    GameObject[] captureZones;
    GameObject currentCaptureZone;
    List<GameObject> uncapturedZones = new List<GameObject>();
    
    private Transform randomUncapturedZone;

    public float enemySpeed = 3f;
    public float stopDistance = 1f;
    public float retreatDistance = 5f;


    // Start is called before the first frame update
    void Start()
    {
        GameObject zone = findUncapturedZone();
        randomUncapturedZone = zone.transform;
        stopDistance = Random.Range(0.1f, 4.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
        float distance = Vector2.Distance(transform.position, randomUncapturedZone.position);

        if (distance > stopDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, randomUncapturedZone.position, enemySpeed * Time.deltaTime);
        }
        else if (distance < stopDistance && distance > retreatDistance)
        {
            transform.position = this.transform.position;
        }
        else if (distance < retreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, randomUncapturedZone.position, -enemySpeed * Time.deltaTime);
        }

    }

    GameObject findUncapturedZone()
    {
        // find all zones
        captureZones = GameObject.FindGameObjectsWithTag("Capture");
        foreach(GameObject captureZone in captureZones)
        {
            currentCaptureZone = captureZone;
            // find uncaptured zones
            if (!(currentCaptureZone.GetComponent<CapturePoint>().isCaptured))
            {
                uncapturedZones.Add(currentCaptureZone);
            }
        }
        // select random
        int idx = Random.Range(0, uncapturedZones.Count);
        return uncapturedZones[idx];
    }

    
}
