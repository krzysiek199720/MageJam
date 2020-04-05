using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float enemySpeed = 1f;
    public float stoppingDis = 1f;

    private Animator anim;
    private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, target.position) > stoppingDis)
        {
            anim.SetBool("isWalking", true);
            transform.position = Vector2.MoveTowards(transform.position, target.position, enemySpeed * Time.deltaTime);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

        if (target.position.x > transform.position.x)
            transform.localScale = new Vector3(1, 1, 1);
        else if (target.position.x < transform.position.x)
            transform.localScale = new Vector3(-1, 1, 1);

    }
}
