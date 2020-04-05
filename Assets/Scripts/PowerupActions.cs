﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupActions
{
    RangedWeapon rangedWeapon = RangedWeapon.Instance;
    LightController lightController = LightController.Instance;
    Player player = Player.Instance;

    public void EmptyFunc()
    {
        ;
    }

    public void DisableFog()
    {
        Fog.Instance.DisableFog();
        LightController.Instance.DisableLight();
    }
    public void EnableFog()
    {
        Fog.Instance.EnableFog();
        LightController.Instance.EnableLight();
    }
    public void DoubleDamage()
    {
        rangedWeapon.damageMultiplier = 2f;
    }
    public void NormalDamage()
    {
        rangedWeapon.damageMultiplier = 1f;
    }

    public void UnlimitedAmmo()
    {
        rangedWeapon.magazineSize = int.MaxValue;
    }
    public void NormalAmmo()
    {
        rangedWeapon.magazineSize = rangedWeapon.magazineMaxSize;
    }

    public void VisionRangePlus()
    {
        lightController.sizeMultiplier = 2f;
    }
    public void VisionRangeNormal()
    {
        lightController.sizeMultiplier = 1f;
    }

    public void Heal()
    {
        player.HealMax();
    }

    public void Endurance()
    {
        player.damageReceivedMultiplier = 0.5f;
    }
    public void NormalEndurance()
    {
        player.damageReceivedMultiplier = 1f;
    }
}
