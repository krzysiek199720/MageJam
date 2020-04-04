using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupActions
{
    //void functions used in powerups
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
}
