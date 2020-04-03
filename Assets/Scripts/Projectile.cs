using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 1f;
    public int enemiesToPierce = 1;
    public float speed = 8f;
    public float maxDistance = 5f;

    [HideInInspector]
    public LayerMask enemyLayer { get; set; }

    [HideInInspector]
    public LayerMask ignoreLayer { get; set; }

    public Vector2 movementVector { get; protected set; }
    public Collider2D hitCollider { get; protected set; }

    private Rigidbody2D rb2d;

    private Vector2 startPos;

    protected void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        startPos = rb2d.position;
    }

    void FixedUpdate()
    {
        Debug.Log(rb2d.position);
        if ((rb2d.position - startPos).magnitude > maxDistance)
            DestroyProjectile();
        rb2d.position = rb2d.position + movementVector * speed * Time.fixedDeltaTime;
    }


    public void addStats(float damage, int pierce = 0, float speed = 1f, float distance = 0f)
    {
        this.damage += damage;
        this.enemiesToPierce += pierce;
        this.maxDistance += distance;

        this.speed *= speed;
    }

    public void setMovementVector(Vector2 v)
    {
        // thought i would want some validation, but cant think of any rn
        movementVector = v.normalized;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (((1 << collision.gameObject.layer) & enemyLayer) == 0)
        {
            //This is not enemy collider
            if (((1 << collision.gameObject.layer) & ignoreLayer) == 0) {
                // destroy as it should be ignored
                DestroyProjectile();
            }
            return;
        }

        //Do enemy collision thingy
        IDamageable enemy = collision.GetComponent<IDamageable>();
        if (enemy == null)
        {
            Debug.LogError("Could not find IDamagable on enemy gameObject");
            return;
        }
        enemy.Damage(damage);

        if (--enemiesToPierce == 0)
        {
            //destroy
            DestroyProjectile();
        }
        

    }

    private void DestroyProjectile()
    {
        Destroy(this.gameObject);
    }
}
