using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    private static PlayerMoveController instance = null;
    public static PlayerMoveController Instance { get { return instance; } }

    public float walkSpeed = 1f;
    public float moveClickRadius = 1.5f;
    private Vector2 movePos;
    private Vector2 lookDir;

    private Rigidbody2D rigidbody2d;
    public RangedWeapon weapon;
    private void Awake()
    {
        // if the singleton hasn't been initialized yet
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        instance = this;
    }

    void Start()
    {
        movePos = Vector2.zero;
        rigidbody2d = GetComponent<Rigidbody2D>();
        weapon = GetComponentInChildren<RangedWeapon>();
    }

    // Update is called once per frame
    void Update()
    {
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
            weapon.Attack();
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
