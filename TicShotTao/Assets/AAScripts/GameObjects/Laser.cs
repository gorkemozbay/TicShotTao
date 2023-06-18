using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public LineRenderer lr;
    public GameObject laser;
    public float distance;

    public bool isLookingRight;
    private Vector3 laserDirection;

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.queriesStartInColliders = false;
        if (isLookingRight)
            laserDirection = Vector3.right;
        else
            laserDirection = Vector3.left;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(laser.transform.position, laserDirection, distance);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Player"))
            {
                lr.SetPosition(1, hitInfo.point);
                hitInfo.collider.gameObject.GetComponent<PlayerHealth>().damagePlayer(1);
            }
            else if (hitInfo.collider.CompareTag("Enemy") || hitInfo.collider.CompareTag("EnemyCapturer"))
            {
                lr.SetPosition(1, laser.transform.position + laserDirection * distance);
            }
            else
            {
                lr.SetPosition(1, hitInfo.point);
            }
        }
        else
        {
            lr.SetPosition(1, laser.transform.position + laserDirection * distance);
        }

        // start of laser
        lr.SetPosition(0, laser.transform.position + laserDirection);
    }
}
