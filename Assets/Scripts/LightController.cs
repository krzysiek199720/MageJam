using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public float walkSpeed = 2f;
    private Rigidbody2D rigidbody2d;

    private Vector2 moveDir;
    private LightSettings lightSettings;
    void Start()
    {
        moveDir = Vector2.zero;
        rigidbody2d = GetComponent<Rigidbody2D>();
        lightSettings = GetComponentInChildren<LightSettings>(); //not the best
    }

    void Update()
    {
        Vector2 move = Vector2.zero;
        //INPUTS
        if (Input.GetKey(KeyCode.W)) move.y = 1;
        if (Input.GetKey(KeyCode.S)) move.y = -1;
        if (Input.GetKey(KeyCode.A)) move.x = -1;
        if (Input.GetKey(KeyCode.D)) move.x = 1;

        moveDir = move.normalized;
    }
    private void FixedUpdate()
    {
        rigidbody2d.MovePosition(Vector2.MoveTowards(rigidbody2d.position, rigidbody2d.position + moveDir, walkSpeed * Time.fixedDeltaTime));
    }

    public void setLightSize(float lightSize)
    {
        lightSettings.setSize(lightSize);
    }
}
