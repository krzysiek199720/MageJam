using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable, IKillable
{
    public float enemySpeed = 1f;
    public float stoppingDis = 1f;

    [HideInInspector]
    public string enemyName;
    [HideInInspector]
    public int pointsPerKill;

    public float maxHealth = 10f;
    public float health;

    private Animator anim;
    private Transform target;
    private Rigidbody2D rigidbody2d;

    private Vector2 pushForce;

    void Start()
    {
        health = maxHealth;
        pushForce = Vector2.zero;
        anim = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log(pushForce);
        transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + pushForce, Time.fixedDeltaTime);
        pushForce /= 2f;
        if (pushForce.magnitude < 0.01f)
            pushForce = Vector2.zero;

        if (Vector2.Distance(transform.position, target.position) > stoppingDis)
        {
            anim.SetBool("isWalking", true);
            transform.position = Vector2.MoveTowards(transform.position, target.position, enemySpeed * Time.fixedDeltaTime);            
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

    public void Damage(float damage)
    {
        damage = Mathf.Abs(damage);
        this.health -= damage;

        if (this.health <= 0)
            Kill();
    }

    public void Kill()
    {
        GameManager.confirmKill(this.name, this.pointsPerKill);
        this.gameObject.SetActive(false);
        //Animacja
        // Dzwiek
        // Destroy
        Destroy(this.gameObject);
    }

    public void Knockback(float force, Vector2 direction)
    {
        Debug.Log("now");
        this.pushForce = direction * force;
        Debug.Log(direction * force);
        Debug.Log(this.pushForce);
    }
}
