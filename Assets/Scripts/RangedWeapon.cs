using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RangedWeapon : Weapon
{
    public int pierceAdd = 0;
    public float projectileSpeedMultiplier = 1f;
    public float projectileDistanceToAdd = 1f;

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

        GameObject projectileGO = Instantiate(projectilePrefab, projectileSpawn.position, projectileSpawn.rotation);
        projectileGO.transform.localScale = new Vector3(
            this.transform.localScale.x * projectileGO.transform.localScale.x
            , this.transform.localScale.y * projectileGO.transform.localScale.y
            , this.transform.localScale.z * projectileGO.transform.localScale.z
            );
        Projectile projectile = projectileGO.GetComponent<Projectile>();

        Vector2 moveMentVector = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;

        projectile.setMovementVector(moveMentVector);
        projectile.addStats(damage, knockbackForce, pierceAdd, projectileSpeedMultiplier, projectileDistanceToAdd);
        projectile.enemyLayer = enemyLayer;
        projectile.ignoreLayer = ignoreLayer;

        return true;
    }
}
