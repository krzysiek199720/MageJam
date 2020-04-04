using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSettings : MonoBehaviour
{
    public float changeTime = 1.5f;
    private float startSize;
    private float endSize;
    private float time;
    public void setSize(float size)
    {
        this.startSize = this.transform.localScale.x;
        this.endSize = size;
        this.time = 0f;
    }
    public void setSizeInstant(float size)
    {
        this.transform.localScale = new Vector3(size, size, size);
    }

    private void Update()
    {
        this.time += Time.deltaTime;
        if (this.time > this.changeTime)
            return;

        float lerpSize = Mathf.Lerp(this.startSize, this.endSize, this.time / this.changeTime);
        this.transform.localScale = new Vector3(lerpSize, lerpSize, lerpSize);
    }
}
