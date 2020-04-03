using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour, IAttack
{
    public LayerMask enemyLayer;

    public float damage = 2f;
    public float attackCooldown = 0.25f;

    private void Update()
    {
        manageCooldown();
    }

    public abstract bool Attack();

    protected abstract void manageCooldown();

}
