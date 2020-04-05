using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable, IKillable
{
    private static Player instance = null;
    public static Player Instance { get { return instance; } }

    public float maxHealth = 10f;
    public float health;
    public float healthRegen = 1f;
    public int regenTicksPerSecond = 1;

    public float damageReceivedMultiplier = 1f;

    private float timeToRegen = 0f;

    //DEBUG STUFF
    private bool hurt = true;
    //DEBUG STUFF END

    private void Awake()
    {
        // if the singleton hasn't been initialized yet
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        instance = this;
    }

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

        //DEBUG STUFF
        if (hurt)
        {
            if (this.health < 1f)
                hurt = false;
            else
                changeHealth(0.1f, true);
        }
        else
            if (this.health == this.maxHealth)
                hurt = true;
        
        //DEBUG STUFF END
    }

    private void changeHealth(float amount, bool isDamage)
    {
        if (isDamage)
            amount = -amount;
        health = Mathf.Min(this.health + amount, maxHealth);

        // Update health UI
        // Update light
        LightController.Instance.setLightSize(this.health, this.maxHealth);
    }

    public void HealMax()
    {
        changeHealth(maxHealth-health, false);
    }

    public void Damage(float damage)
    {
        damage = Mathf.Abs(damage * damageReceivedMultiplier);
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
