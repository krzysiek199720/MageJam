using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSettings : MonoBehaviour
{
    public void setSize(float size)
    {
        this.transform.localScale = new Vector3(size, size, size);
    }
}
