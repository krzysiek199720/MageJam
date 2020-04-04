using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable, IKillable
{
    public float maxHealth = 10f;
    public float health;
    public float healthRegen = 1f;
    public int regenTicksPerSecond = 1;

    private float timeToRegen = 0f;

    private void Start()
    {
        health = maxHealth;
    }
    private void Update()
    {
        timeToRegen -= Time.deltaTime;
        if(timeToRegen < 0f)
        {
            timeToRegen = 1f / regenTicksPerSecond;
            changeHealth(healthRegen, false);
        }
    }

    private void changeHealth(float amount, bool isDamage)
    {
        if (isDamage)
            amount = -amount;
        health = Mathf.Min(this.health + amount, maxHealth);

        // Update health UI
        // Update light
    }
    public void Damage(float damage)
    {
        damage = Mathf.Abs(damage);
        changeHealth(Mathf.Max(this.health - damage, 0f), true);
                
        // if killed
        if (this.health <= 0f)
            Kill();
    }

    public void Knockback(float force, Vector2 direction)
    {
        //Don tknow if player should be knocked back
    }

    public void Kill()
    {
        Debug.Log("You've been killed");
        // Do stuff
    }
}
