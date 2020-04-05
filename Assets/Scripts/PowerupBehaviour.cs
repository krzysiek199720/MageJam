using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupBehaviour : MonoBehaviour
{
    [HideInInspector]
    public PowerupController controller;

    private Powerup powerup;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            ActivatePowerup();
            this.gameObject.SetActive(false);
        }
    }

    private void ActivatePowerup()
    {
        controller.ActivatePowerup(powerup);
        controller.powerupPickedup(this);
    }

    public void SetPowerup(Powerup powerup)
    {
        this.powerup = powerup;
        gameObject.name = powerup.type.ToString();
    }
}
