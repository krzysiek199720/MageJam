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
    private float health;

    public float damage = 1f;
    public float damageInterval = 1f;
    private float currentDamageInterval = 0f;

    private Animator anim;
    private Transform target;
    private Rigidbody2D rigidbody2d;
    private List<Collider2D> colliders = new List<Collider2D>();
    private Collider2D playerCollider;

    private Vector2 pushForce;

    void Start()
    {
        health = maxHealth;
        pushForce = Vector2.zero;
        anim = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
        target = playerGO.GetComponent<Transform>();
        playerCollider = playerGO.GetComponent<Collider2D>();
        rigidbody2d.GetAttachedColliders(colliders);
    }

    // Update is called once per physics tick
    void FixedUpdate()
    {
        currentDamageInterval += Time.fixedDeltaTime;
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


        //do collisions

        foreach(Collider2D col in this.colliders)
        {
            if (col.IsTouching(playerCollider))
            {
                if (this.damageInterval < this.currentDamageInterval)
                {
                    Player.Instance.Damage(this.damage);
                    currentDamageInterval = 0f;
                }
            }
        }
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
        AudioManager.Instance.Play("splash");
        // Destroy
        Destroy(this.gameObject);
    }

    public void Knockback(float force, Vector2 direction)
    {
        this.pushForce = direction * force;
        Debug.Log(direction * force);
        Debug.Log(this.pushForce);
    }
}
