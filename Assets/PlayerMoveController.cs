using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    [SerializeField]
    public float walkSpeed = 1f;
    private Vector2 move;
    private Vector2 lookDir;
    private bool fire;

    private Rigidbody2D rigidbody;
    void Start()
    {
        move = Vector2.zero;
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        fire = false;
        //INPUTS
        lookDir = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButton(1)){
            //need to move
            move = lookDir;
        }
        if (Input.GetMouseButtonDown(0))
            fire = true;
        lookAt(lookDir);
    }
    private void FixedUpdate()
    {
        rigidbody.MovePosition(Vector2.MoveTowards(rigidbody.position, move, walkSpeed * Time.fixedDeltaTime));
    }

    private void lookAt(Vector2 dir)
    {
        transform.right = dir - rigidbody.position;
    }


}
