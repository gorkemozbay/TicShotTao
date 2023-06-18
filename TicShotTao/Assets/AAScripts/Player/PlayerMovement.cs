using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private float playerSpeed;
    private float originalSpeed;

    public Rigidbody2D rb;
    public Camera cam;

    Vector2 movement;
    Vector2 mousePos;

    // to dash
    public TrailRenderer tr;
    public float dashSpeed;
    
    private bool isDashing;
    [SerializeField]
    private float dashCoolDown;
    private float dashDuration;

    // to kill player
    public GameObject playerDeadPs;

    private void Awake()
    {
        //PlayerData.print();
    }

    private void Start()
    {
        playerSpeed = PlayerData.playerSpeed;
        originalSpeed = playerSpeed;
        dashCoolDown = PlayerData.dashCD;
    }

    // Update is called once per frame
    private void Update()
    {
        // to dash
        dashCoolDown -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) && dashCoolDown <= 0f)
        {
            dashCoolDown = PlayerData.dashCD;
            dashDuration = PlayerData.dashDuration;
            playerSpeed = dashSpeed;
            isDashing = true;
            tr.enabled = true;
        }
        if (isDashing && dashDuration <= 0f)
        {
            playerSpeed = originalSpeed;
            isDashing = false;
            tr.enabled = false;
        }
        else if (isDashing)
        {
            dashDuration -= Time.deltaTime;
        }

        // to move
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        // to rotate
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        // to move
        rb.MovePosition(rb.position + movement * playerSpeed * Time.fixedDeltaTime);

        // to rotate
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90 ;
        rb.rotation = angle;
    }

    public void killPlayer()        
    {
        Instantiate(playerDeadPs, transform.position, transform.rotation);
        gameObject.SetActive(false);
    }
}
