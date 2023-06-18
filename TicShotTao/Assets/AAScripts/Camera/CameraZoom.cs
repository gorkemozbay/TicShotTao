using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    Camera cam;
    public GameObject gameManager;
    
    // Camera zoom as player moves
    public bool isMoving;

    // End Effect
    public float zoomOutSize;
    public bool startZoomOut;
    
    // End Effect-2
    private float zoomInDelay;
    
    // End Effect-3
    public bool startPortalZoom;

    // Start Effect
    public float zoomSize;
    public float zoomSpeed;
    public float zoomDelay;

    private void Awake()
    {
        cam = Camera.main;
        gameManager = FindObjectOfType<Manager>().gameObject;
        startZoomOut = false;
        startPortalZoom = false;
        zoomInDelay = 3;
    }

    private void Update()
    {
        zoomDelay -= Time.deltaTime;
        startZoomOut = gameManager.GetComponent<Manager>().didWin;
        if (startZoomOut)
        {
            zoomInDelay -= Time.deltaTime;
            if (zoomInDelay <= 0)
            {
                startZoomOut = false;
            } 
        }
    }

    private void LateUpdate()
    {
        // Portal zoom
        if (startPortalZoom) 
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 1, zoomSpeed * 2.5f * Time.deltaTime);
            cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, -10f);
        }
        // Game end zoom out
        else if (startZoomOut && cam.orthographicSize < zoomOutSize)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, zoomOutSize, zoomSpeed * Time.deltaTime);
        }
        // Game start zoom in
        else if (zoomDelay <= 0f && cam.orthographicSize > zoomSize)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, zoomSize, zoomSpeed * Time.deltaTime);
        }
 
    }

   
}
