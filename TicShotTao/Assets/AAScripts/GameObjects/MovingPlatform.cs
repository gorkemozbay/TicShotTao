using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public IDictionary<int, Transform> targets;

    public int targetIdx;
    private Transform target;

    public float platformSpeed;

    private void Start()
    {
        // Set Target
        targets = new Dictionary<int, Transform>() {
        { 0, pointA },
        { 1, pointB }
        };

        target = targets[targetIdx];
    }

    // Update is called once per frame
    private void Update()
    {
        // To move
        float distanceToTarget = Vector2.Distance(transform.position, target.position);

        if (distanceToTarget < 0.1f)
        {
            targetIdx += 1;
            targetIdx = targetIdx % 2;
            target = targets[targetIdx];
        }

        transform.position = Vector2.MoveTowards(transform.position, target.position, platformSpeed * Time.deltaTime);
    }
}
