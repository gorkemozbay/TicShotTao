using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject gameManager;
    public Transform target;

    // Player follow
    public float smoothSpeed = 10f;
    public float yOffset = 1f;
    
    // Buff-state
    private bool followPlayer;
    private float zoomInDelay;

    // Portal effect
    private bool startPortalZoom;
    private Vector3 portalPos;

    void Start()
    {
        gameManager = FindObjectOfType<Manager>().gameObject;
        followPlayer = true;
        zoomInDelay = 3;
    }

    void Update()
    {
        followPlayer = !gameManager.GetComponent<Manager>().didWin;
        if (!followPlayer) 
        {
            zoomInDelay -= Time.deltaTime;
            if (zoomInDelay <= 0)
            {
                followPlayer = true;
            }
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (startPortalZoom) 
        {
            transform.position = Vector3.Lerp(transform.position, portalPos, smoothSpeed * Time.deltaTime);
        }
        // If did not win OR in buff pick state, follow player
        else if (target != null && followPlayer)  
        {
            // Adjust based on speed here...
            Vector3 newPos = new Vector3(target.position.x, target.position.y + yOffset, -10f);
            transform.position = Vector3.Lerp(transform.position, newPos, smoothSpeed * Time.deltaTime);
        }
        // IF win, put camera to the center of map
        else if (!followPlayer)
        {
            Vector3 newPos = new Vector3(0, 0, -10);
            transform.position = Vector3.Lerp(transform.position, newPos, smoothSpeed * Time.deltaTime);
        }

    }

    public void startPortalFollow(Vector3 portalPositon)
    {
        portalPos = portalPositon;
        startPortalZoom = true;
    }
}



