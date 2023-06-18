using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chainsaw : MonoBehaviour
{
    public Transform[] points;
    public IDictionary<int, Transform> targets = new Dictionary<int, Transform>();

    public int targetIdx;
    private Transform target;

    public float moveSpeed;
    public float rotationSpeed;
    private float rotationAngle;

    // Start is called before the first frame update
    void Start()
    {
        // Set Rotation
        rotationAngle = 0f;

        // Set Target
        for (int i = 0; i < points.Length; i++)
        {
            targets.Add(i, points[i]);
        }

        target = targets[targetIdx];
    }

    // Update is called once per frame
    void Update()
    {
        // To rotate
        rotationAngle += rotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(Vector3.forward * rotationAngle);

        // To move
        float distanceToTarget = Vector2.Distance(transform.position, target.position);

        if (distanceToTarget < 0.1f)
        {
            targetIdx += 1;
            targetIdx = targetIdx % points.Length;
            target = targets[targetIdx];
        }
           
        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.gameObject.GetComponent<PlayerHealth>().damagePlayer(1);
        }
    }
}
