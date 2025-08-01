using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float moveTime = 15f;
    private Rigidbody2D rb;
    private float moveDirection;
    private float moveIterator;
    private bool canMove;

    void Start()
    {
        //Define rigid body
        rb = GetComponent<Rigidbody2D>();
        moveDirection = -1;
        moveIterator = moveTime;
        canMove = true;
    }

    void Update()
    {
        if (moveIterator > 0)
        {
            moveIterator -= Time.deltaTime;
            if (canMove)
                rb.linearVelocity = new Vector2(moveDirection * moveSpeed, rb.linearVelocity.y);
        }
        else if (canMove)
        {
            canMove = false;
            moveIterator = moveTime / 3;
        }
        else
        {
            moveDirection *= -1;
            moveIterator = moveTime;
            canMove = true;
        }
    }
}
