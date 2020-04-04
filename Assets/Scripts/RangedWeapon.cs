using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RangedWeapon : Weapon
{
    public int pierceAdd = 0;
    public float projectileSpeedMultiplier = 1f;
    public float projectileDistanceToAdd = 1f;

    public int numberOfBullets = 5;

    public float minimumShootAngle = -30f;
    public float maximumShootAngle = 30f;

    public Transform projectileSpawn;

    public GameObject projectilePrefab;

    public LayerMask ignoreLayer;

    // maybe well have a list of upgrades or something?
    public float cooldownLeft {get; protected set; }

    void Start()
    {

        if (projectilePrefab == null)
        {
            Debug.LogError("No projectile prefab");
        }

        if (projectileSpawn == null)
        {
            Debug.LogError("No projectile spawn point set");
            projectileSpawn = this.transform;
        }
    }

    protected override void manageCooldown()
    {
        cooldownLeft = Mathf.Max(cooldownLeft - Time.deltaTime, 0);
    }

    public override bool Attack()
    {
        if (cooldownLeft > 0)
            return false;


        //Do the attack
        cooldownLeft = attackCooldown;
        Vector2 moveMentVector = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;

        for(int i = 0; i < numberOfBullets; ++i)
        {
            GameObject projectileGO = Instantiate(projectilePrefab, projectileSpawn.position, projectileSpawn.rotation);
            projectileGO.transform.localScale = new Vector3(
                this.transform.localScale.x * projectileGO.transform.localScale.x
                , this.transform.localScale.y * projectileGO.transform.localScale.y
                , this.transform.localScale.z * projectileGO.transform.localScale.z
                );
            Projectile projectile = projectileGO.GetComponent<Projectile>();


            projectile.setMovementVector(rotatedVector2(moveMentVector, Random.Range(minimumShootAngle, maximumShootAngle)));
            projectile.addStats(damage, knockbackForce, pierceAdd, projectileSpeedMultiplier, projectileDistanceToAdd);
            projectile.enemyLayer = enemyLayer;
            projectile.ignoreLayer = ignoreLayer;
        }

        return true;
    }

    protected Vector2 rotatedVector2(Vector2 v, float degrees)
    {
        Vector2 res = new Vector2();

        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

        res.x = (cos * v.x) - (sin * v.y);
        res.y = (sin * v.x) + (cos * v.y);
        return res;
    }
}
