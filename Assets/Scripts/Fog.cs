using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog : MonoBehaviour
{
    private static Fog instance = null;
    public static Fog Instance { get { return instance; } }
    private void Awake()
    {
        // if the singleton hasn't been initialized yet
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        instance = this;
    }

    public void EnableFog()
    {
        this.gameObject.SetActive(true);
    }
    public void DisableFog()
    {
        this.gameObject.SetActive(false);
    }
}
