using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingLaser : MonoBehaviour
{
    public LineRenderer lr;
    
    public float rotateSpeed;
    public float distance;
    

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.queriesStartInColliders = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, distance);

        if(hitInfo.collider != null)
        {
            lr.SetPosition(1, hitInfo.point);
            // set color
            // check and damage player
        }
        else
        {
           lr.SetPosition(1, transform.position + transform.right * distance);
        }

        lr.SetPosition(0, gameObject.transform.GetChild(1).transform.position);
        
    }
}
 