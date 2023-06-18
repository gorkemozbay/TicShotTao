using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCircle : MonoBehaviour
{
    public bool startMove;
    public float moveSpeed;
    private Vector3 goalPos;

    // Start is called before the first frame update
    void Start()
    {
        startMove = false;
        moveSpeed = 40f;
    }

    // Update is called once per frame
    void Update()
    {
        if (startMove)
        {
            OnWin();
        }
    }

    void OnWin()
    {
        transform.position = Vector2.MoveTowards(transform.position, goalPos, moveSpeed * Time.deltaTime);
    }

    public void SetGoalPos(string movementPath)
    {
        if(movementPath == "h") // horizontal
        {
            goalPos = new Vector3(-transform.position.x, transform.position.y, 0);
        }
        else if(movementPath == "v") // vertical
        {
            goalPos = new Vector3(transform.position.x, -transform.position.y, 0);
        }
        else if (movementPath == "d") // diagonal
        {
            goalPos = new Vector3(-transform.position.x, -transform.position.y, 0);
        }
        
        startMove = true;
    }
}
