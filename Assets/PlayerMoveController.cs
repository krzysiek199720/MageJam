using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    public float walkSpeed = 1f;
    public float moveClickRadius = 1.5f;
    private Vector2 movePos;
    private Vector2 lookDir;
    private bool doFire = false;
    private bool doMove = false;

    private Rigidbody2D rigidbody2d;
    void Start()
    {
        movePos = Vector2.zero;
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        doFire = false;
        doMove = false;
        //INPUTS
        lookDir = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButton(1))// right mouse button
        {
            if (Vector2.Distance(rigidbody2d.position, lookDir) > moveClickRadius)
                movePos = lookDir;
            else
                movePos = rigidbody2d.position;
        }
        else
            movePos = rigidbody2d.position;

        if (Input.GetMouseButton(0))
            doFire = true;
        lookAt(lookDir);
        
    }
    private void FixedUpdate()
    {
        rigidbody2d.MovePosition(Vector2.MoveTowards(rigidbody2d.position, movePos, walkSpeed * Time.fixedDeltaTime));

    }

    private void lookAt(Vector2 dir)
    {
        transform.right = dir - rigidbody2d.position;
    }

}
