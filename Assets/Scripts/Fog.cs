using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog : MonoBehaviour
{
    private static Fog instance = null;
    public static Fog Instance { get { return instance; } }

    private SpriteRenderer sp;
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
        sp = GetComponent<SpriteRenderer>();
    }

    public void EnableFog()
    {
        this.sp.color = new Color(0, 0, 0, 255);
        this.gameObject.layer = LayerMask.NameToLayer("Black");
        //this.gameObject.SetActive(true);
    }
    public void DisableFog()
    {
        this.sp.color = new Color(0, 0, 0, 0);
        this.gameObject.layer = LayerMask.NameToLayer("Mask");
        //this.gameObject.SetActive(false);
    }
}
