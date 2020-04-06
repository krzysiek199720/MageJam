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

    private string[] hurtSounds = new string[] { "hurt1", "hurt2", "hurt3", "hurt4" };

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

        
    }

    private void changeHealth(float amount, bool isDamage)
    {
        if (isDamage)
        {
            amount = -amount;
            AudioManager.Instance.Play(hurtSounds[Random.Range(0, hurtSounds.Length)]);
        }
        health = Mathf.Clamp(this.health + amount, 0f, maxHealth);


        // if killed
        if (this.health <= 0f)
            Kill();

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
        changeHealth(damage, true);
    }

    public void Knockback(float force, Vector2 direction)
    {
        //Don tknow if player should be knocked back
    }

    public void Kill()
    {
        Animator anim = GetComponent<Animator>();
        PlayerMoveController.Instance.isDead = true;
        GameManager.Instance.ShowGameOverScreen();
        anim.Play("player_death");
        Debug.Log("You've been killed");
        // Do stuff
    }
}
